using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using ResellerClub;
using ResellerClub.Interface;

using ResellerClub.Interface;
using ResellerClub.Interface.Messages;
using ResellerClub.Messages;
using DAL = ResellerClub.DataAccess;
using ResellerClub.DataAccess;


namespace Test
{
    public class Domain : IDomain
    {
        public IInvoiceInfoMessage Register(string domain, int years, ICustomerInfoMessage customer, ResellerClub.Common.InvoiceOption invoice)
        {
            IInvoiceInfoMessage invi = new ResellerClub.Messages.InvoiceInfoMessage();
            invi.Domain = domain;
            invi.Description = "Registration of " + domain + " for 1 year";
            invi.InvioceNumber = "789453";
            invi.Amount = 200;
            return invi;
        }

        public List<IDomainInfoMessage> SearchDomain(string domain, List<ITopLevelDomianMessage> tlds)
        {
            List<IDomainInfoMessage> domains = new List<IDomainInfoMessage>();
            domains.Add(new DomainInfoMessage("microsoft.com"));
            domains.Add(new DomainInfoMessage("microsoft.co"));
            domains.Add(new DomainInfoMessage("microsoft.in"));
            domains.Add(new DomainInfoMessage("microsoft.net"));
            domains.Add(new DomainInfoMessage("microsoft.org"));
            domains.Add(new DomainInfoMessage("microsoft.info"));
            domains.Add(new DomainInfoMessage("microsoft.biz"));
            return domains;

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

        #region IDomain Members


        public void InsertTopLevelDomain(string productName, string planName, string topLevelDomain)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDomain Members


        public void InsertTopLevelDomain(string productName, string planName, string topLevelDomain, string currency)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBaseInterface Members

        public string UserIP
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IBaseInterface Members


        public Guid? SessionID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IBaseInterface Members


        public string UserURL
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
