using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace ResellerClub.Localization
{
    public class DomainPlans : Plans
    {

        private readonly Dictionary<string, int> allTlds;
        private readonly List<string> recommendTlds;
        private readonly List<string> sideBarTLDs;
        private readonly List<string> nameServers;

        public DomainPlans(XmlDocument doc, string rootPath)
            : base(doc, rootPath)
        {
            allTlds = new Dictionary<string, int>();
            recommendTlds = new List<string>();
            sideBarTLDs = new List<string>();
            nameServers = new List<string>();
            LoadSettings(doc, rootPath);
        }

        private void LoadSettings(XmlDocument doc, string rootPath)
        {

            XmlNodeList xmlNL = doc.SelectNodes(rootPath + "/plans/plan");

            //=========Load tlds(ALL,recommend,sidebar) and link =============
            xmlNL = doc.SelectNodes(rootPath + "/tlds/tld");

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
            xmlNL = doc.SelectNodes(rootPath + "/namesevers/nameserver");

            foreach (XmlNode node in xmlNL)
            {
                nameServers.Add(node.InnerText);
            }
        }
        public List<string> GetAllTlds()
        {
            return new List<string>(allTlds.Keys);
        }

        public List<string> GetRecommendTLDs()
        {
            return recommendTlds;
        }

        public List<string> GetSideBarTLDs()
        {
            return sideBarTLDs;
        }

        public List<string> GetDefaultNameServers()
        {
            return nameServers;
        }

        public Plan GetPlan(string tld)
        {
            return plans[allTlds[tld]];
        }


        public Plan GetDomainPlan(string domain)
        {
            List<string> dplan = new List<string>();
            string tld = domain.Substring(domain.IndexOf('.'));
            return plans[allTlds[tld]];
        }

        public int GetDomainPlanId(string domain)
        {
            string tld = domain.Substring(domain.IndexOf('.'));
            List<PlanItem> pitems = plans[allTlds[tld]].planItems;
            return int.Parse( plans[allTlds[tld]].PlanId);
        }
    }
}
