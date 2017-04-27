using System;
using ResellerClub.Interface.Messages;
using System.Collections.Generic;
namespace ResellerClub.Interface
{
    public interface IDomain : IBaseInterface 
    {
        IInvoiceInfoMessage Register(string domain, int years, ResellerClub.Interface.Messages.ICustomerInfoMessage customer, ResellerClub.Common.InvoiceOption invoice);
        System.Collections.Generic.List<IDomainInfoMessage> SearchDomain(string domain,  List<ITopLevelDomianMessage>  tlds);
        List<ITopLevelDomianMessage> GetTopLevelDomian();
        void InsertTopLevelDomain(string productName, string planName, string topLevelDomain, string currency);
    }
}
