using System;
using System.Collections.Generic;

using System.Text;

namespace ResellerClub.Interface.Messages
{
    public interface IInvoiceInfoMessage
    {
        string Domain { get; set; }
        string Description { get; set; }
        string InvioceNumber { get; set; }
        decimal  Amount { get; set; }
        string Response { get; set; }
        int Status { get; set; }
    }
}
