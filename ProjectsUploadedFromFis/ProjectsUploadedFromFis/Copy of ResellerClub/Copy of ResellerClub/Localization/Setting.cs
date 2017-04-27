using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.Web;

namespace ResellerClub.Localization
{
    public class Setting
    {
        public DomainPlans  domSetting;
        public Plans  whLinuxSetting;
        public Plans whWindowsSetting;
        public Plans ehSetting;
        public Plans standWebSiteSetting;
        public Plans ecomWebSiteSetting;
        public Plans sslSiteSetting;
        public string CurS;

        private const string whLinuxSettingRootPath = "config/webhosting/linux";
        private const string whWindowsSettingRootPath = "config/webhosting/windows";
        private const string ehSettingRootPath = "config/emailhosting";
        private const string domainSettingRootPath = "config/domain";
        private const string standWebSiteSettingRootPath = "config/websitedesign/standard";
        private const string ecomWebSiteSettingRootPath = "config/websitedesign/ecommerece";
        private const string sslSiteSettingRootPath = "config/ssl";

        internal Setting(XmlDocument doc)
        {
            domSetting = new  DomainPlans(doc,domainSettingRootPath);
            whLinuxSetting = new Plans(doc, whLinuxSettingRootPath);
            whWindowsSetting = new Plans(doc, whWindowsSettingRootPath);
            ehSetting = new Plans(doc, ehSettingRootPath);
            standWebSiteSetting = new Plans(doc, standWebSiteSettingRootPath);
            ecomWebSiteSetting  = new Plans(doc, ecomWebSiteSettingRootPath);
            sslSiteSetting = new Plans(doc, sslSiteSettingRootPath);
            LoadSettings(doc);
        }


        private void LoadSettings(XmlDocument doc)
        {
            CurS = doc.SelectSingleNode("config/currencysymbol").FirstChild.Value;
        }


    }
}
