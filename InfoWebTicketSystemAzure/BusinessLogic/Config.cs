using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;

namespace API
{
    internal abstract class Config
    {

        private static readonly Dictionary<int, Dictionary<int, int>> plans;
        private static readonly Dictionary<string, int> allTlds;
        private static readonly List<string> recommendTlds;
        private static readonly List<string> sideBarTLDs;
        private static readonly List<string> nameServers;
      
        static Config()
        {
            plans = new Dictionary<int, Dictionary<int, int>>();
            allTlds = new Dictionary<string, int>();
            recommendTlds = new List<string>();
            sideBarTLDs = new List<string>();
            nameServers = new List<string>();
            LoadSettings();
        }

        private static void LoadSettings()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("/XMLs/Config.xml"));

            XmlNodeList xmlNL = doc.SelectNodes("config/plans/plan");

            foreach (XmlNode node in xmlNL)
            {
                Dictionary<int, int> plan = new Dictionary<int, int>();
                foreach (XmlNode child in node.ChildNodes)
                {
                    plan.Add(int.Parse(child.Attributes["year"].Value), int.Parse(child.Attributes["price"].Value));
                }
                plans.Add(int.Parse(node.Attributes["id"].Value), plan);
            }

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

            xmlNL = doc.SelectNodes("config/namesevers/nameserver");

            foreach (XmlNode node in xmlNL)
            {
                nameServers.Add(node.InnerText);
            }
        }

        public static List<string> GetAllTlds()
        {
            return new List<string>(allTlds.Keys);
        }

        public static List<string> GetRecommendTLDs()
        {
            return recommendTlds;
        }

        public static List<string> GetSideBarTLDs()
        {
            return sideBarTLDs;
        }

        public static List<string> GetDefaultNameServers()
        {
            return nameServers;
        }

        public static Dictionary<int, int> GetPlan(string tld)
        {
            return plans[allTlds[tld]];
        }

    }
}
