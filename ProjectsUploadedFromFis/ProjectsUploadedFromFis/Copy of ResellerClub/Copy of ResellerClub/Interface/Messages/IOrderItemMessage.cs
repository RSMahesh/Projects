using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResellerClub.Interface.Messages
{
   public interface IOrderItemMessage
    {
        Guid SubPlanID { get; set; }
        string DomainName { get; set; }
        bool EnableSsl { get; set; }
        bool EnableMaintenance { get; set; }
        string InvoiceNumber { get; set; }
        string Description { get; set; }
        int Status { get; set; }
    }
}
