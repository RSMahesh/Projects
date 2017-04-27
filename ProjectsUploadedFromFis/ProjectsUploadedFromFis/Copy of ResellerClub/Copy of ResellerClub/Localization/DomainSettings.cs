using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Localization
{
    public class DomainSettings
    {
        private  readonly Dictionary<int, Plan> plans;
        private  readonly Dictionary<string, int> allTlds;
        private  readonly List<string> recommendTlds;
        private  readonly List<string> sideBarTLDs;
        private  readonly List<string> nameServers;


        public DomainSettings(XmlDocument doc)
        {
            plans = new Dictionary<int, Plan>();
            allTlds = new Dictionary<string, int>();
            recommendTlds = new List<string>();
            sideBarTLDs = new List<string>();
            nameServers = new List<string>();
            LoadSettings(doc);
        }

        private void LoadSettings(XmlDocument doc)
        {
        
            XmlNodeList xmlNL = doc.SelectNodes("config/domain/plans/plan");
            
            //===Load Plans=====================
            foreach (XmlNode node in xmlNL)
            {
                int strartingFrom =0;
              //  Dictionary<int, int> plan = new Dictionary<int, int>();
                if (node.Attributes["startingfrom"] != null && node.Attributes["startingfrom"].Value != "")
                    strartingFrom = int.Parse(node.Attributes["startingfrom"].Value);

                Plan plan = new Plan(strartingFrom);
                foreach (XmlNode child in node.ChildNodes)
                {
                    plan.AddItem(int.Parse(child.Attributes["year"].Value), int.Parse(child.Attributes["price"].Value));
                }
                plans.Add(int.Parse(node.Attributes["id"].Value), plan);
            }

            //=========Load tlds(ALL,recommend,sidebar) and link =============
            xmlNL = doc.SelectNodes("config/domain/tlds/tld");

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
            xmlNL = doc.SelectNodes("config/domain/namesevers/nameserver");

            foreach (XmlNode node in xmlNL)
            {
                nameServers.Add(node.InnerText);
            }
        }

        public  List<string> GetAllTlds()
        {
            return new List<string>(allTlds.Keys);
        }

        public  List<string> GetRecommendTLDs()
        {
            return recommendTlds;
        }

        public  List<string> GetSideBarTLDs()
        {
            return sideBarTLDs;
        }

        public  List<string> GetDefaultNameServers()
        {
            return nameServers;
        }

        public  Plan  GetPlan(string tld)
        {
            return plans[allTlds[tld]];
        }

    }
}
