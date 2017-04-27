using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using ResellerClub.Interface;
using ResellerClub.Common;
using ResellerClub.Interface.Messages;
using DAL = ResellerClub.DataAccess;
using ResellerClub.DataAccess;
using ResellerClub.Common.Logging;

namespace ResellerClub.BusinessLogic
{
    public class Domain : BaseBRL, IDomain
    {
        public Domain()
        { }

        public Domain(string authUser, string authPassword)
            : base(authUser, authPassword) { }

        public List<IDomainInfoMessage> SearchDomain(string domain, List<ITopLevelDomianMessage> tlds)
        {
            try
            {

                ResellerClub.Common.WebService ws = new ResellerClub.Common.WebService();
                string domainSerachurl = "";
                string searchResult;
                domainSerachurl += "&domain-name=" + domain;

                foreach (var tld in tlds)
                {
                    domainSerachurl += "&tlds=" + tld.Name.Substring(1);//remove .
                }

                searchResult = PostUrl(GetInitalUrl("domains/available.json") + domainSerachurl);
               // domainSerachurl = "https://test.httpapi.com/api/domains/available.xml?auth-userid=89290&api-key=BjV4VpBuqJKAhOb52y65LbwinJW1NZtw&domain-name=domain1&domain-name=domain2&tlds=com&tlds=net";
                //searchResult = PostUrl(domainSerachurl);

                return ParseDomainSearchResult(ParseJsonResponse(searchResult));
            }
            catch (ServerException ex)
            {
                LogException(ex, "");
                throw;
            }

        }

        private List<IDomainInfoMessage> ParseDomainSearchResult(Dictionary<string, object> dic)
        {
            List<IDomainInfoMessage> domains = new List<IDomainInfoMessage>();
            foreach (string key in dic.Keys)
            {
                Dictionary<string, object> innerDic = (Dictionary<string, object>)dic[key];
                if (innerDic["status"].ToString().ToUpperInvariant() == "AVAILABLE")
                    domains.Add(new Messages.DomainInfoMessage(key));
            }

            return domains;
        }

        public ResellerClub.Interface.Messages.IInvoiceInfoMessage Register(string domain, int years,
           ResellerClub.Interface.Messages.ICustomerInfoMessage customer, InvoiceOption invoice)
        {
            var invi = new ResellerClub.Messages.InvoiceInfoMessage();
            invi.Domain = domain;

            try
            {
                string domainResgister = "&domain-name=" + domain + "&years=" + years.ToString();
                List<string> nameServers = GetNameServer();
                //TO DO nameServers.Join<string>(

                for (int i = 0; i < nameServers.Count; i++)
                {
                    domainResgister += "&ns=" + nameServers[i];
                }

                domainResgister += "&customer-id=" + customer.CustomerID + "&reg-contact-id=" + customer.RegContactID;
                domainResgister += "&admin-contact-id=" + customer.AdminContactID + "&tech-contact-id=" + customer.TechContactID;
                domainResgister += "&billing-contact-id=" + customer.BillingContactID + "&invoice-option=" + invoice.ToString();
                domainResgister += "&protect-privacy=true&cedcontactid=" + customer.CedContactID;
                string result = PostUrl(GetInitalUrl("domains/register.json") + domainResgister);
                var response = ParseJsonResponse(result);

                if (response["status"] != null && response["status"].ToString() == "error")
                {
                    invi.Description = response["error"].ToString();
                    invi.InvioceNumber = "";
                    invi.Status = Constant.OrderItemStatusFailed;
                }
                else
                {
                    invi.Description = response["actiontypedesc"].ToString();
                    invi.InvioceNumber = response["invoiceid"].ToString();
                    invi.Status = Constant.OrderItemStatusProcessed;
                }

                invi.Response = result;
                invi.Amount = 0;
            }

            catch (ServerException ex)
            {
                invi.Response = lastServerException;
                invi.Status = Constant.OrderItemStatusFailed;
            }
            catch (Exception ex)
            {
               LogException(ex,"");
            }
            return invi;

        }

        public List<ITopLevelDomianMessage> GetTopLevelDomian()
        {
            List<ITopLevelDomianMessage> list;
            using (var connection = ConnectionFactory.GetConnection())
            {
                DAL.Domain dalDomain = new ResellerClub.DataAccess.Domain(connection);
                list = dalDomain.GetTopLevelDomian();
            }
            return list;
        }

        private List<string> GetNameServer()
        {
            List<string> list;
            using (var connection = ConnectionFactory.GetConnection())
            {
                DAL.Domain dalDomain = new ResellerClub.DataAccess.Domain(connection);
                list = dalDomain.GetNameServer();
            }
            return list;
        }

        public void InsertTopLevelDomain(string productName, string planName, string topLevelDomain, string currency)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                DAL.Domain dalDomain = new ResellerClub.DataAccess.Domain(connection);
                dalDomain.InsertTopLevelDomain(productName, planName, topLevelDomain, currency);
            }

        }
        #region OffLine
        private List<IDomainInfoMessage> SearchDomain()
        {
            List<IDomainInfoMessage> domains = new List<IDomainInfoMessage>();
            domains.Add(new Messages.DomainInfoMessage("microsoft.com"));
            domains.Add(new Messages.DomainInfoMessage("microsoft.co"));
            domains.Add(new Messages.DomainInfoMessage("microsoft.in"));
            domains.Add(new Messages.DomainInfoMessage("microsoft.net"));
            domains.Add(new Messages.DomainInfoMessage("microsoft.org"));
            domains.Add(new Messages.DomainInfoMessage("microsoft.info"));
            domains.Add(new Messages.DomainInfoMessage("microsoft.biz"));

            return domains;
        }
        #endregion

    }
}
