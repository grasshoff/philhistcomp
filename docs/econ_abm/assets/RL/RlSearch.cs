using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
#if NETFX_CORE
using Windows.Data.Xml.Dom;
using XmlNode = Windows.Data.Xml.Dom.IXmlNode;
#else
using System.Xml;

namespace Msal.Rl
{

    public struct RlSearchParams {
        public int maxGames;
        public int maxGameLength;
        public int maxTime;
        public int expandThreshold;
        public double biasTermConstant;
        public string searchModelTag;
        
        public RlSearchParams(int games, int length, int time, int threshold, double bias, string tag) {
            maxGames = games;
            maxGameLength = length;
            maxTime = time;
            expandThreshold = threshold;
            biasTermConstant = bias;
            searchModelTag = tag;
        }
    }

    public class RlSearch<StateId_t, Action_t>
    {
        internal int m_estimateGames;
        internal int m_evaluateGames;
        internal float m_avgEstimateGameLength;
        internal float m_avgEvaluateGameLength;
        internal float m_avgTreeSize;

        protected Thread m_searchThread;
        protected RlModel<StateId_t, Action_t> m_model;
        protected RlTree<StateId_t, Action_t>[] m_searchTrees;
        protected RlSearchParams m_searchParams;
        protected int m_games;
        protected long m_time;
        protected bool m_running;

        public RlSearch(RlModel<StateId_t, Action_t> model)
        {
            m_model = model;
            m_running = false;
            m_estimateGames = 0;
            m_evaluateGames = 0;
            m_avgEstimateGameLength = 0.0f;
            m_avgEvaluateGameLength = 0.0f;
            m_avgTreeSize = 0;
        }
        
        public bool IsRunning {
            get { return m_running; }
        }

        public float progress {
            get { return ((float)m_games) / m_searchParams.maxGames; }
        }

        public int games {
            get { return m_games; }
        }

        public long time {
            get { return m_time; }
        }

        public int EstimatedGames {
            get { return m_estimateGames; }
        }

        public int EstimatedGamesLength {
            get { return (int)m_avgEstimateGameLength; }
        }

        public int EvaluateGames {
            get { return m_evaluateGames; }
        }

        public int EvaluateGamesLength {
            get { return (int)m_avgEvaluateGameLength; }
        }

        public int SearchTreeSize {
            get {
                if (m_searchTrees == null) return 0;
                int actorToAct = m_model.rlModelGetActorToAct();
                if (m_searchTrees.Length <= actorToAct) return 0;
                if (m_searchTrees[actorToAct] == null) return 0;
                return m_searchTrees[actorToAct].m_nodesCount;
            }
        }

        // Thread based search
        public void Search(RlSearchParams searchParams) 
        {
            m_searchParams = searchParams;
            m_searchThread = new Thread(Start);

            int actorsCount = m_model.rlModelGetActorsCount();
            m_searchTrees = new RlTree<StateId_t, Action_t>[actorsCount];
            for (int index = 0; index < m_searchTrees.Length; ++index) {
                m_searchTrees[index] = new RlTree<StateId_t, Action_t>();
            }
            
            // Start the search thread
            m_running = true;
            m_searchThread.Start();
        }

        protected void Start() {
            //Console.WriteLine("Search thread started ...");
            m_games = 0;
            m_time = 0;
            m_estimateGames = 0;
            m_evaluateGames = 0;
            m_avgEstimateGameLength = 0.0f;
            m_avgEvaluateGameLength = 0.0f;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var elapsedMilliseconds = watch.ElapsedMilliseconds;
            RlEpisod<StateId_t, Action_t> episod = new RlEpisod<StateId_t, Action_t>(this, m_model, m_searchTrees, m_searchParams);

            for (; m_games < m_searchParams.maxGames; ++m_games) {
                episod.Generate();
                episod.UpdateTrees();
                // Stop the search if the maxTime had expired
                if (m_searchParams.maxTime > 0 && (m_games % 100) == 0 && watch.ElapsedMilliseconds >= m_searchParams.maxTime) {
                    break;
                }
            }
            
            watch.Stop();
            m_time = watch.ElapsedMilliseconds;
            m_running = false;
            //Console.WriteLine("Search thread done!");
        }

        // Coroutine based search
        public void Search(RlSearchParams searchParams, MonoBehaviour context) 
        {
            m_searchParams = searchParams;
            m_searchThread = null;

            int actorsCount = m_model.rlModelGetActorsCount();
            m_searchTrees = new RlTree<StateId_t, Action_t>[actorsCount];
            for (int index = 0; index < m_searchTrees.Length; ++index) {
                m_searchTrees[index] = new RlTree<StateId_t, Action_t>();
            }
            
            // Start the search thread
            m_running = true;
            context.StartCoroutine(StartCo());
        }

        public IEnumerator StartCo() {
            //Console.WriteLine("Search thread started ...");
            m_games = 0;
            m_time = 0;
            m_estimateGames = 0;
            m_evaluateGames = 0;
            m_avgEstimateGameLength = 0.0f;
            m_avgEvaluateGameLength = 0.0f;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var elapsedMilliseconds = watch.ElapsedMilliseconds;
            RlEpisod<StateId_t, Action_t> episod = new RlEpisod<StateId_t, Action_t>(this, m_model, m_searchTrees, m_searchParams);

            for (; m_games < m_searchParams.maxGames; ++m_games) {
                episod.Generate();
                episod.UpdateTrees();
                // Stop the search if the maxTime had expired
                if (m_searchParams.maxTime > 0 && (m_games % 100) == 0 && watch.ElapsedMilliseconds >= m_searchParams.maxTime) {
                    break;
                }
                if ((watch.ElapsedMilliseconds - elapsedMilliseconds) >= 50) {
                    yield return null;
                    elapsedMilliseconds = watch.ElapsedMilliseconds;
                }
            }
            
            watch.Stop();
            m_time = watch.ElapsedMilliseconds;
            m_running = false;
            //Console.WriteLine("Search thread done!");
        }

        public void FindBestAction(out Action_t result, out int bestActionsCount, out double bestValue)
        {
            double value;
            var actorId = m_model.rlModelGetActorToAct();
            bestValue = Double.NaN;
            bestActionsCount = 0;
            Action_t[] bestActions = new Action_t[1000];

            if (m_searchThread != null) {
                m_searchThread.Join();
            }
            m_model.rlModelActionGetNone(out result);
/*
            var document = m_searchTrees[actorId].asXML(m_model);
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Unicode);
            writer.Formatting = Formatting.Indented;
            document.WriteContentTo(writer);
            writer.Flush();
            stream.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            String formattedXML = reader.ReadToEnd();
            File.AppendAllText("log.txt", "\n\n" + formattedXML);
*/
            // For each child into the root node of the search tree
            foreach (var child in m_searchTrees[actorId].m_root.m_childs)
            {   // For each action into the child node
                foreach (var action in child.Value.m_actions)
                {
                    if (!action.m_stats.IsDefined) continue;
                    value = action.m_stats.m_mean;
                    if (Double.IsNaN(bestValue) || bestValue < value)
                    {
                        bestValue = value;
                        bestActionsCount = 0;
                        bestActions[bestActionsCount++] = action.m_action;
                    } 
                    else if (Math.Abs(bestValue-value) < 0.0001f) {
                        bestActions[bestActionsCount++] = action.m_action;
                    }
                }
            }

            if (bestActionsCount > 0) {
                result = bestActions[RlUtils.random.Next(bestActionsCount)];
            }
        }
    }

}

