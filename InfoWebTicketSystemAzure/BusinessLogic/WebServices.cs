using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Common;
using ResellerClub.Interface;
using ResellerClub.Common.Logging;


namespace ResellerClub.BusinessLogic
{
    public class WebServices : BaseBRL, IWebServices
    {
        public WebServices()
        {
        }

        public WebServices(string authUser, string authPassword)
            : base(authUser, authPassword) { }

        private new   string   GetPlanId(string productName, string planName)
        {
            Dictionary<string, object> response = GetPlanDetail();
            Dictionary<string, object> hosting = (Dictionary<string, object>)response["hosting"];

            string serviceId = "";

            foreach (string key in hosting.Keys)
            {
                var dic = (Dictionary<string, object>)hosting[key];

                if (dic["category"].ToString() == productName && dic["plan_name"].ToString().ToUpperInvariant() == planName.ToUpperInvariant())
                {
                    serviceId = key;
                    break;
                }
            }

            return serviceId;
        }

        private string GetServiceUrl(string product)
        {
            string url = "";
            switch (product)
            {
                case Constant.SingleDomainHostingLinuxUs:
                    url = "singledomainhosting/linux/us/add.json";
                    break;
                case Constant.SingleDomainHostingWindowsUs:
                    url = "singledomainhosting/windows/us/add.json";
                    break;
                case Constant.EmailHosting:
                    url = "singledomainhosting/linux/us/add.json";
                    break;
       
            }

            return url;
        }

        private Dictionary<string, object> Add(string domainName, string custromerId, string months, string planId, InvoiceOption invoiceOption, out string response)
        {
            Dictionary<string, object> rtnVal = new Dictionary<string, object>();
            response = "";
            try
            {
                string addUrl = "&domain-name=" + domainName + "&customer-id=" + custromerId + "&months=" + months.ToString() + "&plan-id=" + planId + "&invoice-option=" + invoiceOption.ToString();
                response = PostUrl(GetInitalUrl("webservices/add.json") + addUrl);
                //response = PostUrl(GetInitalUrl("singledomainhosting/linux/us/add.json") + addUrl);
                rtnVal = ParseJsonResponse(response);
            }
            catch (ServerException ex)
            {
                response = lastServerException;
            }
            catch (Exception ex)
            {
                LogException(ex, "");
            }
            return rtnVal;
        }

        public ResellerClub.Interface.Messages.IInvoiceInfoMessage Register(string domainName, string custromerId, string months, string productName, string planName, InvoiceOption invoiceOption)
        {
            var invi = new ResellerClub.Messages.InvoiceInfoMessage();

            string result = "";
            try
            {
                string planId = GetPlanId(productName,planName);
                string serviceUrl = "";
                //var response = (Dictionary<string, object>)Add(domainName, custromerId, months, planId, invoiceOption, out result).First().Value();
                var response = Add(domainName, custromerId, months, planId, invoiceOption, out result);
                invi.Response = result;
                response = (Dictionary<string, object>)response.First().Value;
                invi.Domain = domainName;
                invi.Description = response["actiontypedesc"].ToString();
                invi.InvioceNumber = response["invoiceid"].ToString();
                invi.Response = result;
                invi.Status = Constant.OrderItemStatusProcessed;
            }
            catch (Exception ex)
            {
                LogException(ex, "");
                invi.Status = Constant.OrderItemStatusFailed;
            }

            return invi;
        }

        public static Dictionary<int, int> GetWebServicePlan(WebService webService)
        {
            Dictionary<int, int> rateList = new Dictionary<int, int>();
            rateList.Add(1, 990);
            rateList.Add(2, 590);
            rateList.Add(3, 490);
            rateList.Add(5, 190);

            return rateList;
        }

    }
}
