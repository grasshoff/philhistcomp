using System;
using System.Collections.Generic;
using System.Text;
#if NETFX_CORE
using Windows.Data.Xml.Dom;
using XmlNode = Windows.Data.Xml.Dom.IXmlNode;
#else
using System.Xml;
#endif

namespace Msal.Rl {

    public class RlNode<StateId_t, Action_t> {

        public RlStats m_stats = new RlStats();
        public RlAction<StateId_t, Action_t>[] m_actions;
        public Dictionary<StateId_t,RlNode<StateId_t, Action_t>> m_childs = new Dictionary<StateId_t, RlNode<StateId_t, Action_t>>();

        public RlNode(RlTree<StateId_t, Action_t> tree) {
            m_actions = new RlAction<StateId_t, Action_t>[0];
        }

        public RlNode(RlModel<StateId_t, Action_t> model, int actorId) {
            Action_t[] actions;
            model.rlModelGetActions(actorId, out actions);
            m_actions = new RlAction<StateId_t, Action_t>[actions.Length];
            for (int index = 0; index < actions.Length; ++index) {
                m_actions[index] = new RlAction<StateId_t, Action_t>(actions[index]);
            }
        }

        public bool AddChild(RlModel<StateId_t, Action_t> model, int actorId, out RlNode<StateId_t, Action_t> result)
        {
            StateId_t stateId = model.rlModelStateGetId();
            if (m_childs.TryGetValue(stateId, out result)) {
                return false;
            }
            result = new RlNode<StateId_t, Action_t>(model, actorId);
            m_childs.Add(stateId, result);
            return true;
        }

        public XmlElement asXML(RlModel<StateId_t, Action_t> model, XmlElement parent) {
            var nodeElement = parent.OwnerDocument.CreateElement("child");
            parent.AppendChild(nodeElement);
            if (m_stats.IsDefined) {
                nodeElement.SetAttribute("count", m_stats.m_count.ToString());
                nodeElement.SetAttribute("mean", m_stats.m_mean.ToString("F3"));
            }

            // render the actions
            var actionsElement = nodeElement.OwnerDocument.CreateElement("actions");
            nodeElement.AppendChild(actionsElement);
            foreach (var action in m_actions) {
                action.toXML(model, actionsElement);
            }

            // render the children
            foreach (var key in m_childs.Keys) {
                var childElement = m_childs[key].asXML(model, nodeElement);
                childElement.SetAttribute("id", key.ToString());
            }

            return nodeElement;
        }
    }
}
