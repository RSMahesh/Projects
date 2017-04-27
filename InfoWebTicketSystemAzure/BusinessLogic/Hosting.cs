using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;
using ResellerClub.Common;
using ResellerClub.Interface;

namespace ResellerClub.BusinessLogic
{
    public class Hosting : BaseBRL,IHosting
    {
        private Dictionary<string, object> AddToServer(string domainName, string custromerId, string months, string planId,string serviceUrl, InvoiceOption invoiceOption, out string response)
        {
            Dictionary<string, object> rtnVal = new Dictionary<string, object>();
            response = "";
            try
            {
                string addUrl = "&domain-name=" + domainName + "&customer-id=" + custromerId + "&months=" + months.ToString() + "&plan-id=" + planId + "&invoice-option=" + invoiceOption.ToString();
                response = PostUrl(GetInitalUrl(serviceUrl) + addUrl);
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

        public IInvoiceInfoMessage Add(string domainName, string custromerId, string months, string productName, string planName, InvoiceOption invoiceOption)
        {
            var invi = new ResellerClub.Messages.InvoiceInfoMessage();
            string result = "";
            try
            {
                string planId = GetPlanId(productName, planName);
                string serviceUrl = GetServiceUrl(productName);
                var response = AddToServer(domainName, custromerId, months, planId, serviceUrl, invoiceOption, out result);
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
    
    }

      
}
