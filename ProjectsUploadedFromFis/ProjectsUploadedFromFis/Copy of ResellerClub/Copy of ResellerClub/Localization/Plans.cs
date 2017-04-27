using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace ResellerClub.Localization
{
    public class Plans
    {
        protected Dictionary<int, Plan> plans;

        public Plans(XmlDocument doc, string rootPath)
        {
            plans = new Dictionary<int, Plan>();
            LoadSettings(doc, rootPath);
        }

        private void LoadSettings(XmlDocument doc, string rootPath)
        {

            //XmlNodeList xmlNL = doc.SelectNodes("config/webhosting/plans/plan");
            XmlNodeList xmlNL = doc.SelectNodes(rootPath + "/plans/plan");
            //===Load Plans=====================
            foreach (XmlNode node in xmlNL)
            {
                int strartingFrom = 0;
                string  planId = "";
                if (node.Attributes["startingprice"] != null && node.Attributes["startingprice"].Value != "")
                    strartingFrom = int.Parse(node.Attributes["startingprice"].Value);

                if (node.Attributes["id"] != null && node.Attributes["id"].Value != "")
                    planId = node.Attributes["id"].Value;

                Plan plan = new Plan(strartingFrom, planId);

                foreach (XmlNode child in node.ChildNodes)
                {
                    plan.AddItem(int.Parse(child.Attributes["year"].Value), int.Parse(child.Attributes["price"].Value),
                     (Duration)   Enum.Parse(typeof(Duration),child.Attributes["priceduration"] == null?"month" : child.Attributes["priceduration"].Value));
                }
                plans.Add(int.Parse(node.Attributes["id"].Value), plan);
            }

        }

        public Plan GetPlanFromPlanId(int planId)
        {
           return plans[planId];
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
