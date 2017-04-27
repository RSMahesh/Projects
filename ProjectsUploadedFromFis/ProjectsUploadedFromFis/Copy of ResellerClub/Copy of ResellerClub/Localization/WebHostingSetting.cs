using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Localization
{
   public class WebHostingSetting
    {
        public int startingFrom;
        private readonly Dictionary<int, Plan> plans;

        public WebHostingSetting(XmlDocument doc)
        {
            plans = new Dictionary<int, Plan>();
            LoadSettings(doc);
        }

        private void LoadSettings(XmlDocument doc)
        {

            XmlNodeList xmlNL = doc.SelectNodes("config/webhosting/plans/plan");

            //===Load Plans=====================
            foreach (XmlNode node in xmlNL)
            {
                int strartingFrom = 0;
                if (node.Attributes["startingprice"] != null && node.Attributes["startingprice"].Value != "")
                    strartingFrom = int.Parse(node.Attributes["startingprice"].Value);
                Plan plan = new Plan(strartingFrom);

                foreach (XmlNode child in node.ChildNodes)
                {
                    plan.AddItem(int.Parse(child.Attributes["year"].Value), int.Parse(child.Attributes["price"].Value));
                }
                plans.Add(int.Parse(node.Attributes["id"].Value), plan);
            }

        }

        public Plan Plan1
        {
            get
            {
                return plans[1];
            }
        }
        public Plan Plan2
        {
            get
            {
                return plans[2];
            }
        }
        public Plan Plan3
        {
            get
            {
                return plans[3];
            }
        }
        public Plan Plan4
        {
            get
            {
                return plans[4];
            }
        }
    }
}
