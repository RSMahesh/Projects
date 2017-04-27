using System;
using ResellerClub.Interface.Messages; 

namespace ResellerClub.Interface
{
    public interface ISSL : IBaseInterface 
    {
        IInvoiceInfoMessage Register(string domain, int years, string sslType, int additionalLicenses, ICustomerInfoMessage  customer, ResellerClub.Common.InvoiceOption invoice);
    }
}
