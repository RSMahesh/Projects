using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace WebApplication1.BRL
{
    public class Domain : BaseBRL
    {

        public Domain(string authUser, string authPassword)
            : base(authUser, authPassword)
        {

        }

        //erid=329832&auth-password=MB248001&
        //domain-name=ccccccccccccc.com&years=1&ns=ns1.domain.com&ns=ns2.domain.com
        //    &customer-id=6952394&reg-contact-id=16117848&admin-contact-id=16117848&tech-contact-id=16117848&billing-contact-id=16117848&
        //invoice-option=KeepInvoice&protect-privacy=true&cedcontactid=16117848

        public Dictionary<string,int> SearchDomain(List<string> domains, List<string> tlds)
        {
            string domainSerachurl = "";
            string searchResult;
            foreach(string domain in domains)
            {
                domainSerachurl  += "&domain-name="+ domain;
            }

            foreach (string tld in tlds)
            {
                domainSerachurl += "&tlds=" + tld;
            }

            searchResult = PostUrl( GetInitalUrl("domains/available.json")+ domainSerachurl);

           return ParseDomainSearchResult(ParseJsonResponse(searchResult));

        }

        private Dictionary<string, int> ParseDomainSearchResult(Dictionary<string, object> dic)
        {
            Dictionary<string, int> domains = new Dictionary<string, int>();
            foreach (string key in dic.Keys)
            {
                Dictionary<string, object> innerDic = (Dictionary<string, object>)dic[key];
                if(innerDic["status"].ToString().ToUpperInvariant() == "AVAILABLE")
                domains.Add(key, GetDomainPrice(key));
            }

            return domains;
        }

        public string RegisterDomain(string domain,int years, List<string> nameServers, 
            Customer customer,InvoiceOption invoice)
        {
            return "";
        }

        public int GetDomainPrice(string domain)
        {
            return 45;
        }

    }
}
