using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Specialized;
using ResellerClub.Common;
using ResellerClub.Interface.Messages;


namespace ResellerClub.WebUI
{
    public abstract class Plan
    {
        static Plan()
        {
            Load();
        }

        public static void Load()
        {
            LoadPlans();
            LoadTopLevelDomain();
        }

        private static void LoadPlans()
        {
            var subPlanProductMapping = new Dictionary<Guid, IPlanMessage>();
            List<ResellerClub.Interface.Messages.IPlanMessage> planList;
            var plan = ApiObjectFactory.GetObject<ResellerClub.Interface.IPlan>();
            planList = plan.GetPlans();
            //   _LinuxHostingPlan = planList.FindAll(x => x.ProductName == "linux_hosting_plan");
            //  _WindowsHostingPlan = planList.FindAll(x => x.ProductName == "windows_hosting_plan");
            Cache.Set(Constant.SingleDomainHostingLinuxUs, planList.FindAll(x => x.ProductName == Constant.SingleDomainHostingLinuxUs));
            Cache.Set(Constant.SingleDomainHostingWindowsUs, planList.FindAll(x => x.ProductName == Constant.SingleDomainHostingWindowsUs));
            Cache.Set(Constant.EmailHosting, planList.FindAll(x => x.ProductName == Constant.EmailHosting));
            Cache.Set(Constant.DomainRegistration, planList.FindAll(x => x.ProductName == Constant.DomainRegistration));
            Cache.Set(Constant.StandardWebSite, planList.FindAll(x => x.ProductName == Constant.StandardWebSite));
            Cache.Set(Constant.EComWebSite, planList.FindAll(x => x.ProductName == Constant.EComWebSite));
            Cache.Set(Constant.WebSiteBuilder, planList.FindAll(x => x.ProductName == Constant.WebSiteBuilder));

            Cache.Set(Constant.SSL_FSSL, planList.FindAll(x => x.ProductName == Constant.SSL_FSSL));
            Cache.Set(Constant.SSL_SGC , planList.FindAll(x => x.ProductName == Constant.SSL_SGC));
            Cache.Set(Constant.SSL_SSL123, planList.FindAll(x => x.ProductName == Constant.SSL_SSL123));
            Cache.Set(Constant.SSL_WILD, planList.FindAll(x => x.ProductName == Constant.SSL_WILD));

            Cache.Set("AllPlan", planList);

            foreach (var p in planList)
            {
                subPlanProductMapping[p.SubPlanID] = p;
            }

            Cache.Set("SubPlanProductMapping", subPlanProductMapping);
        }

        private static void LoadTopLevelDomain()
        {
            var domain = ApiObjectFactory.GetObject<ResellerClub.Interface.IDomain>();
            Cache.Set(Constant.TopLevelDomain, domain.GetTopLevelDomian());
        }

        public static string GetProductName(Guid subPlanId)
        {
            var subPlanProductMapping = (Dictionary<Guid, IPlanMessage>)Cache.Get("SubPlanProductMapping");
            return subPlanProductMapping[subPlanId].ProductName;
        }

        public static List<IPlanMessage> GetProductPalns(string productName)
        {
            var list = (List<IPlanMessage>)Cache.Get(productName);
            return FilterByCurrency(list);

            // return (List<IPlanMessage>)Cache.Get(productName);
        }

        public static IPlanMessage GetPlanBySubPlanId(Guid subPlanId)
        {
            var subPlanProductMapping = (Dictionary<Guid, IPlanMessage>)Cache.Get("SubPlanProductMapping");
            return subPlanProductMapping[subPlanId];
        }

        public static string GetPlanCurrency(Guid planId)
        {
            List<IPlanMessage> planList = (List<IPlanMessage>)Cache.Get("AllPlan");
            return planList.Find(x => x.PlanID == planId).CurrencyName;
        }

        public static List<ResellerClub.Interface.Messages.IPlanMessage> GetPlansBySequence(string productName, int sequence)
        {
            var list = (List<ResellerClub.Interface.Messages.IPlanMessage>)Cache.Get(productName);
            var filterList = FilterByCurrency(list);
            return filterList.FindAll(x => x.PlanSequence == sequence);
        }


        public static decimal GetPlanStartingPrice(string productName, int sequence)
        {
            var list = (List<ResellerClub.Interface.Messages.IPlanMessage>)Cache.Get(productName);
            var filterList = FilterByCurrency(list);
           var _list = filterList.FindAll(x => x.PlanSequence == sequence);
           var min = _list.Min(x => x.Price);
           return (int) min/12;
        }


        public static decimal GetPlanStartingPrice(string productName)
        {
            var list = (List<ResellerClub.Interface.Messages.IPlanMessage>)Cache.Get(productName);
            var filterList = FilterByCurrency(list);
            var min = filterList.Min(x => x.Price);
            return (int)min / 12;
        }

        public static List<IPlanMessage> GetTopLevelDomainPlan(string topLevelDomain)
        {
            var tld = TopLevelDomainList.Find(x => x.Name == topLevelDomain);
            var lis = (List<ResellerClub.Interface.Messages.IPlanMessage>)Cache.Get(Constant.DomainRegistration);
            var filterList = FilterByCurrency(lis);
            return filterList.FindAll(x => x.PlanID == tld.PlanID);
        }

        public static List<IPlanMessage> GetAllPlan()
        {
            return (List<IPlanMessage>)Cache.Get("AllPlan");
        }

        public static List<IPlanMessage> FilterByCurrency(List<IPlanMessage> planMsgList)
        {
            var currency = GetCurrency();
            var filterList = planMsgList.FindAll(x => x.CurrencyName.ToUpperInvariant() == currency.ToUpperInvariant());
            return filterList;
        }

        #region Properties
        public static List<ResellerClub.Interface.Messages.IPlanMessage> LinuxHostingPlan
        {
            get
            {
                var list = (List<ResellerClub.Interface.Messages.IPlanMessage>)Cache.Get(Constant.SingleDomainHostingLinuxUs);
                return FilterByCurrency(list);
            }
        }

        public static List<IPlanMessage> WindowsHostingPlan
        {
            get
            {
                var list = (List<IPlanMessage>)Cache.Get(Constant.SingleDomainHostingWindowsUs);
                return FilterByCurrency(list);
            }
        }

        public static List<IPlanMessage> StandardWebSite
        {
            get
            {
                var list = (List<IPlanMessage>)Cache.Get(Constant.StandardWebSite);
                return FilterByCurrency(list);
            }
        }


        public static List<IPlanMessage> EcomWebSite
        {
            get
            {
                var list = (List<IPlanMessage>)Cache.Get(Constant.EComWebSite);
                return FilterByCurrency(list);
            }
        }


        public static List<ResellerClub.Interface.Messages.ITopLevelDomianMessage> TopLevelDomainList
        {
            get
            {
                var list = (List<ResellerClub.Interface.Messages.ITopLevelDomianMessage>)Cache.Get(Constant.TopLevelDomain);
                var currency = GetCurrency();
                var filterList = list.FindAll(x => GetPlanCurrency(x.PlanID).ToUpperInvariant() == currency.ToUpperInvariant());
                return filterList;
            }
        }

        public static string GetCurrency()
        {
            SessionManager SessionM = new SessionManager();
            var country = "india";
            var currency = "";
            if (SessionM["country"] == null)
            {
                country =(string) SessionM["country"];
            }

            switch (country)
            {
                case "usa":
                    currency = "usd";
                    break;
                case "india":
                    currency = "rupee";
                    break;
            }
            return currency;
        }

        public static string GetCurrencySymbol()
        {
            var currency = GetCurrency();
            var currencySymbol = "Rs";
            switch (currency.ToUpperInvariant())
            {
                case "USD":
                    currencySymbol = "$";
                    break;
                case "RUPEE":
                    currencySymbol = "Rs";
                    break;
            }
            return currencySymbol;
        }

        #endregion
    }
}
