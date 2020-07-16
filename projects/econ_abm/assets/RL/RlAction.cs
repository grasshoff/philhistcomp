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

    public struct RlAction<StateId_t, Action_t> {
        public RlStats m_stats;
        public Action_t m_action;

        public RlAction(Action_t action) {
            m_stats = new RlStats();
            m_action = action;
        }

        public void toXML(RlModel<StateId_t, Action_t> model, XmlElement parent) {
            var element = parent.OwnerDocument.CreateElement("action");
            parent.AppendChild(element);
            if (m_stats.IsDefined) {
                element.SetAttribute("count", m_stats.m_count.ToString());
                element.SetAttribute("mean", m_stats.m_mean.ToString("F3"));
            } else {
                element.SetAttribute("count", "0");
                element.SetAttribute("mean", "-NAN-");
            }
            element.InnerText = model.rlModelActionToString(m_action);
        }
    }
}
