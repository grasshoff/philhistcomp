using System;
using System.Collections.Generic;
using System.Text;

namespace Msal.Rl {

    public struct RlStats {
        public double m_mean;
        public int m_count;

        public RlStats(double mean, int count)
        {
            m_mean = mean;
            m_count = count;
        }


        public void Add(double value, int n)
        {
            if (Double.IsNaN(m_mean)) {
                m_mean = value / n;
                m_count = n;
            } else {
                var count = m_count;
                count += n;
                value -= m_mean;
                m_mean +=  n * value / count;
                m_count = count;
            }
        }

        public void Add(double value)
        {
            Add(value, 1);
        }

        public bool IsDefined
        {
        	get { return m_count > 0; }
        }
    }
}
