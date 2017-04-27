using System;
using System.Data;
using System.Configuration;

using System.Web;

namespace ResellerClub.Common
{
    public enum WebService { LinuxHosting, WindowsHosting, EComWebSite, WebSiteBuilder, StandardWebSite, EmailHosting, Domain,SSL_SGC,SSL_SSL123,SSL_FSSL,SSL_WILD,NONE };

    public static class WebServiceExtension
    {
        public static string Value(this WebService webService)
        {
            switch (webService)
            {
                case WebService.LinuxHosting:
                    return "linux_hosting_plan";
                case WebService.WindowsHosting:
                    return "windows_hosting_plan";
                case WebService.EmailHosting:
                    return "email_plan";
                case WebService.EComWebSite:
                    return "ecommerce_bifm_plan";
                case WebService.StandardWebSite:
                    return "standard_bifm_plan";
                case WebService.WebSiteBuilder:
                    return "websitebuilder_plan";
                case WebService.SSL_SSL123:
                    return "ssl";
                case WebService.SSL_FSSL:
                    return "fssl";
                case WebService.SSL_SGC:
                    return "sgc";
                case WebService.SSL_WILD:
                    return "wild";
                
            }
            return "";
        }

        public static WebService  Parse(this string webService)
        {
            switch (webService)
            {
                case "linux_hosting_plan":
                    return WebService.LinuxHosting;
                //case WebService.WindowsHosting:
                //    return "windows_hosting_plan";
                //case WebService.EmailHosting:
                //    return "email_plan";
                //case WebService.EComWebSite:
                //    return "ecommerce_bifm_plan";
                //case WebService.StandardWebSite:
                //    return "standard_bifm_plan";
                //case WebService.WebSiteBuilder:
                //    return "websitebuilder_plan";
                //case WebService.SSL_SSL123:
                //    return "ssl";
                //case WebService.SSL_FSSL:
                //    return "fssl";
                //case WebService.SSL_SGC:
                //    return "sgc";
                //case WebService.SSL_WILD:
                //    return "wild";

            }
            return WebService.NONE;
        }


    }
}
