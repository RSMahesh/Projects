using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Localization
{
    class WebHostingPlan
    {
        public int startingFrom;
        public Plan plan1;
        public Plan plan2;
        public Plan plan3;
        public Plan plan4;


        public WebHostingPlan(string country, XmlDocument doc)
        {
            LoadSettings(doc);
        }

        private void LoadSettings(XmlDocument doc)
        {

            XmlNodeList xmlNL = doc.SelectNodes("config/webhosting/plans/plan");

            //===Load Plans=====================
            foreach (XmlNode node in xmlNL)
            {
                Plan plan = new Plan();
                foreach (XmlNode child in node.ChildNodes)
                {
                    plan.AddItem(int.Parse(child.Attributes["year"].Value), int.Parse(child.Attributes["price"].Value));
                }
                plans.Add(int.Parse(node.Attributes["id"].Value), plan);
            }

            //=========Load tlds(ALL,recommend,sidebar) and link =============
            xmlNL = doc.SelectNodes("config/tlds/tld");

            foreach (XmlNode node in xmlNL)
            {
                allTlds.Add(node.InnerText, int.Parse(node.Attributes["planid"].Value));
                if (node.Attributes["recommend"] != null && bool.Parse(node.Attributes["recommend"].Value))
                {
                    recommendTlds.Add(node.InnerText);
                }

                if (node.Attributes["sidebar"] != null && bool.Parse(node.Attributes["sidebar"].Value))
                {
                    sideBarTLDs.Add(node.InnerText);
                }
            }

            //===Load NmaeServers=====================
            xmlNL = doc.SelectNodes("config/namesevers/nameserver");

            foreach (XmlNode node in xmlNL)
            {
                nameServers.Add(node.InnerText);
            }
        }
    }
}
