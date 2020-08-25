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

    public class RlTree<StateId_t, Action_t> {

        public readonly RlNode<StateId_t, Action_t> m_root;
        public int m_nodesCount = 0;

        public RlTree() {
            m_root = new RlNode<StateId_t, Action_t>(this);
            m_nodesCount++;
        }

        public RlNode<StateId_t, Action_t> AddState(RlModel<StateId_t, Action_t> model, int actorId, ref RlNode<StateId_t, Action_t> currentNode) {
            if (currentNode == null) {
                currentNode = m_root;
            }
            if (currentNode.AddChild(model, actorId, out currentNode)) {
                m_nodesCount++;
            }
            return currentNode;
        }

        public XmlDocument asXML(RlModel<StateId_t, Action_t> model) {
            var result = new XmlDocument();
            var element = result.CreateElement("root");
            result.AppendChild(element);
            m_root.asXML(model, element);
            return result;
        }
        
    }
}
