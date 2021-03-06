﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;

namespace Localization
{
    internal abstract class Settings
    {
        public DomainSettings domainSettings;
        public WebHostingPlan webHostingPlan;
        public EmailHostingPlan emailHostingPlan;

        public  Settings(string country,XmlDocument doc)
        {

            LoadSettings();
        }


        private void LoadSettings()
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

      
    }
}
