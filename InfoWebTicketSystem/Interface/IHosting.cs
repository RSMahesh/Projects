using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;

namespace ResellerClub.Interface
{
    public interface IHosting
    {
        IInvoiceInfoMessage Add(string domainName, string custromerId, string months, string productName, string planName, ResellerClub.Common.InvoiceOption invoiceOption);
    }

}
