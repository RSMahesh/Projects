using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using ResellerClub.Common;
using ResellerClub.Interface;
using ResellerClub.Interface.Messages;
using ResellerClub.Common.Logging;

namespace ResellerClub.BusinessLogic
{
    public class SSL : BaseBRL, ISSL
    {
        public SSL()
        {
        }
        public SSL(string authUser, string authPassword)
            : base(authUser, authPassword) { }


        public ResellerClub.Interface.Messages.IInvoiceInfoMessage Register(string domain, int years, string sslType, int additionalLicenses,
          ICustomerInfoMessage customer, InvoiceOption invoice)
        {
            var invi = new ResellerClub.Messages.InvoiceInfoMessage();

            try
            {
                string addSsl = "&domain-name=" + domain + "&customer-id=" + customer.CustomerID +
                    "&years=" + years.ToString() + "&additional-licenses=" + additionalLicenses.ToString() +
                    "&cert-key=" + sslType + "&invoice-option=" + invoice.ToString();

                var response = PostUrl(GetInitalUrl("digitalcertificate/add.xml") + addSsl);
                var doc = new XmlDocument();

                doc.LoadXml(response);
                invi.Domain = domain;
                invi.Description = doc.SelectSingleNode("hashtable/entry/hashtable/entry/string[.='actiontypedesc']").NextSibling.InnerText;
                invi.InvioceNumber = doc.SelectSingleNode("hashtable/entry/hashtable/entry/string[.='invoiceid']").NextSibling.InnerText;
                invi.Response = response;
                invi.Status = Constant.OrderItemStatusProcessed;
            }
            catch (ServerException ex)
            {
                invi.Response = lastServerException;
                invi.Status = Constant.OrderItemStatusFailed;
            }
            catch (Exception ex)
            {
                LogException(ex, "");
            }
            return invi;
        }

    }
}
