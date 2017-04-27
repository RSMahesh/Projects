using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResellerClub.Common
{
    public static class Constant
    {
        /* Product Name 
         multidomainhosting
         multidomainhostinglinuxin
         multidomainhostinglinuxuk
        
         multidomainwindowshosting
         multidomainwindowshostingin
         multidomainwindowshostinguk
        
         resellerhosting
         resellerhostinglinuxin
         resellerhostinglinuxuk
         resellerwindowshosting
         resellerwindowshostingin
         resellerwindowshostinguk
         singledomainhostinglinuxin
         singledomainhostinglinuxuk
         singledomainhostinglinuxus
         singledomainhostingwindowsin
         singledomainhostingwindowsuk
         singledomainhostingwindowsus
         thawtecert
         */

        //Hosting
        public const string SingleDomainHostingLinuxUs = "singledomainhostinglinuxus";
        public const string SingleDomainHostingWindowsUs = "singledomainhostingwindowsus";
        public const string MultiDomainHostingLinuxUs = "multidomainhosting";
        public const string MultiDomainHostingWindowsUs = "multidomainwindowshosting";
        //Web Services
        public const string EComWebSite = "ecommerce_bifm_plan";
        public const string WebSiteBuilder = "websitebuilder_plan";
        public const string StandardWebSite = "standard_bifm_plan";
        public const string EmailHosting = "email_plan";
        //Domain
        public const string DomainRegistration = "domain_registration";
        // Dgital Cretificate
        public const string SSL_SGC = "sgc";
        public const string SSL_SSL123 = "ssl";
        public const string SSL_FSSL = "fssl";
        public const string SSL_WILD = "wild";
        public const string splitChar = "|;|";
        public const string TopLevelDomain = "TopLevelDomain";
        //Order Status
        public const int OrderStatusSaved = 1;
        public const int OrderStatusSentToPaymentProcessor = 2;
        public const int OrderStatusPaymentVerified = 3;
        public const int OrderStatusUnProcessed = 4;
        public const int OrderStatusProcessed = 5;
        public const int OrderStatusPaymentAwaited = 101;
        public const int OrderStatusUpdatedTranscationNumber = 102;

        public const int OrderStatusPaymentInvalid = -2;
        public const int GenericError = -1;
        //Order Item Status
        public const int OrderItemStatusSaved = 1;
        public const int OrderItemStatusProcessed = 3;
        public const int OrderItemStatusFailed = -1;
        public const string DefualtItemDescription = "Under Process";

        //Payment Mode 
        public const string PaymentModePayPal = "PayPal";
        public const string PaymentModeIMPS = "IMPS";
        public const string PaymentModeNetBanking = "NetBanking";
        public const string PaymentModeOffLine = "Cash/Check";
       

        public static string GetItemDescription(string domainName, string product,string planDisplayName)
        {
            string description = "";
            switch (product)
            {
                case DomainRegistration:
                    description = domainName + " Rgistration";
                    break;
                case SingleDomainHostingLinuxUs:
                    description = domainName +" " + planDisplayName + " Linux Hosting";
                    break;
                case SingleDomainHostingWindowsUs:
                    description = domainName + " " + planDisplayName +  " Windows Hosting";
                    break;
                case EmailHosting:
                    description = domainName +" " + planDisplayName + " Email Hosting";
                    break;
                case StandardWebSite:
                    description = domainName + " Standard WebSite Design";
                    break;
                case EComWebSite:
                    description = domainName + " Ecommerece WebSite";
                    break;
                case SSL_SSL123:
                    description = domainName + " SSL123 Certificate";
                    break;
                case SSL_FSSL:
                    description = domainName + " Web Server Certificate";
                    break;
                case SSL_SGC:
                    description = domainName + " SGC SuperCert";
                    break;
                case SSL_WILD:
                    description = domainName + " Wildcard Server Certificate";
                    break;

                case WebSiteBuilder:
                    description = domainName + " " + planDisplayName + " WebSite Builder";
                    break;

            }

            return description;
        }

    }
}
