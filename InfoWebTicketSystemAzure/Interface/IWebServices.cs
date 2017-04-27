using System;
using ResellerClub.Interface.Messages; 
namespace ResellerClub.Interface
{
    public interface IWebServices : IBaseInterface 
    {
        IInvoiceInfoMessage Register(string domainName, string custromerId, string months,string productName , string planName, ResellerClub.Common.InvoiceOption invoiceOption);
        System.Collections.Generic.Dictionary<string, object> GetPlanDetail();
        
    }
}
