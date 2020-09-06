using System;
using System.Collections.Generic;
using System.Text;

namespace Msal.Rl {

    public class RlEpisod<StateId_t, Action_t>
    {
        public struct RlRecord {
            public RlNode<StateId_t, Action_t> m_node;
            public int m_action;
            public double m_reward;

            public RlRecord(RlNode<StateId_t, Action_t> node, int action, double reward)
            {
                m_node = node;
                m_action = action;
                m_reward = reward;
            }
        };

        public double m_firstPlayUrgency = 10000.0;
        public RlSearch<StateId_t, Action_t> m_search;
        public RlModel<StateId_t, Action_t> m_model;
        public RlModel<StateId_t, Action_t> m_searchModel;
        public RlTree<StateId_t, Action_t>[] m_searchTrees;
        public RlSearchParams m_searchParams;
        public RlRecord[,] m_records;
        public int[] m_recordsCount;


        public RlEpisod(RlSearch<StateId_t, Action_t> search, RlModel<StateId_t, Action_t> model, RlTree<StateId_t, Action_t>[] searchTrees, RlSearchParams searchParams)
        {
            m_search = search;
            m_model = model;
            m_searchModel = m_model.rlModelClone();
            m_searchTrees = searchTrees;
            m_records = new RlRecord[m_model.rlModelGetActorsCount(), searchParams.maxGameLength + 10];
            m_recordsCount = new int[m_model.rlModelGetActorsCount()];
            m_searchParams = searchParams;
            //m_searchParams.biasTermConstant = Math.Sqrt(Math.Log(maxGames));
        }

        public void Generate() {
            int actorId;

            // Copy the original model into the search model
            m_searchModel.rlModelCopy(m_model);
            //m_searchModel = m_model.rlModelClone();
            m_searchModel.rlModelInitES();

            // Track whether the actor is in playout mode
            bool[] playouts = new bool[m_model.rlModelGetActorsCount()];

            // Track the number of records for each actor
            Array.Clear(m_recordsCount, 0, m_recordsCount.Length);

            // Track the current node of each actor into the tree
            RlNode<StateId_t, Action_t>[] currentNode = new RlNode<StateId_t, Action_t>[m_model.rlModelGetActorsCount()];
            for (actorId = 0; actorId < currentNode.Length; ++actorId) {
                currentNode[actorId] = m_searchTrees[actorId].m_root;
                m_records[actorId, m_recordsCount[actorId]++] = new RlRecord(currentNode[actorId], -1, 0.0);
            }

            // Play a game
            int gameLength = 0;
            while (!m_searchModel.rlModelStateIsTerminal(m_model, m_searchParams.maxGameLength))
            {
                int actionIndex = -1;
                Action_t action;
                actorId = m_searchModel.rlModelGetActorToAct();

                // Find an action to be applied to the actor
                if (playouts[actorId]) {
                    m_searchModel.rlModelRandomAction(actorId, out action);
                } else {
                    m_searchTrees[actorId].AddState(m_searchModel, actorId, ref currentNode[actorId]);
                    actionIndex = SelectAction(currentNode[actorId]);
                    if (actionIndex >= 0) {
                        action = currentNode[actorId].m_actions[actionIndex].m_action;
                        playouts[actorId] = currentNode[actorId].m_actions[actionIndex].m_stats.m_count < m_searchParams.expandThreshold;
                    } else {
                        m_searchModel.rlModelActionGetNone(out action);
                        playouts[actorId] = true;
                    }
                }

                // If there is no valid action for the actor, then the search is over
                if (m_searchModel.rlModelActionIsNone(ref action)) break;
                // Apply the action
                double reward = m_searchModel.rlModelApplyAction(actorId, action);
                // Save the action into the episode history
                m_records[actorId, m_recordsCount[actorId]++] = new RlRecord(currentNode[actorId], actionIndex, reward);
                gameLength++;
            }

            double[] rewards = new double[m_searchModel.rlModelGetActorsCount()]; ;
            if (m_searchModel.rlModelStateIsTerminal(m_model, m_searchParams.maxGameLength)) {
                m_searchModel.rlModelEstimate(m_model, ref rewards, m_searchParams.searchModelTag);
                m_search.m_avgEstimateGameLength += (gameLength - m_search.m_avgEstimateGameLength) / ++m_search.m_estimateGames;
            } else {
                m_searchModel.rlModelEvaluate(actorId, ref rewards);
                m_search.m_avgEvaluateGameLength += (gameLength - m_search.m_avgEvaluateGameLength) / ++m_search.m_evaluateGames;
            }

            // Add a final record to the episode to reflect the final rewards
            for (actorId = 0; actorId < rewards.Length; ++actorId) {
                m_records[actorId, m_recordsCount[actorId]++] = new RlRecord(null, -1, rewards[actorId]);
            }
        }


        protected int SelectAction(RlNode<StateId_t, Action_t> node)
        {
            if (node == null || node.m_actions.Length == 0) {
                return -1;
            }
            if (node.m_stats.m_count == 0) {
                return RlUtils.random.Next(node.m_actions.Length);
            }
            double countLog = Math.Log(node.m_stats.m_count);
            double bestUpperBound = 0.0;
            int bestAction = -1;
            for (int index = 0; index < node.m_actions.Length; ++index) {
                var bound = getBoundLP(countLog, ref node.m_actions[index].m_stats);
                if (bestAction < 0 || bound > bestUpperBound) {
                    bestAction = index;
                    bestUpperBound = bound;
                }
            }
            return bestAction;
        }

        protected double getBoundLP(double countLog, ref RlStats stats) {
            var value = getValueEstimate(ref stats);
            //if (m_searchParams.biasTermConstant == 0.0) return value;
            value += m_searchParams.biasTermConstant * Math.Sqrt(countLog / (stats.m_count + 1));
            return value;
        }

        protected double getValueEstimate(ref RlStats stats) {
            return stats.m_count > 0 ? stats.m_mean : m_firstPlayUrgency;
        }


        public void UpdateTrees()
        {
            double gamma = 0.999;
            
            for (int actorId = 0; actorId < m_searchModel.rlModelGetActorsCount(); ++actorId)
            {
                double reward = 0.0;
                for (int index = m_recordsCount[actorId] - 1; index >= 0; --index) {
                    var actionReward = m_records[actorId, index].m_reward;
                    reward += Double.IsNaN(actionReward) ? 0.0 : actionReward;
                    //reward += m_records[actorId, index].m_reward;
                    if (m_records[actorId, index].m_node != null) {
                        if (index > 0 && Object.ReferenceEquals(m_records[actorId, index-1].m_node, m_records[actorId, index].m_node)) continue;
                        m_records[actorId, index].m_node.m_stats.Add(reward);
                        int actionIndex = m_records[actorId, index].m_action;
                        if (actionIndex >= 0) {
                            m_records[actorId, index].m_node.m_actions[actionIndex].m_stats.Add(reward);
                        }
                    }
                    reward *= gamma;
                }
            }
        }

    }


}
