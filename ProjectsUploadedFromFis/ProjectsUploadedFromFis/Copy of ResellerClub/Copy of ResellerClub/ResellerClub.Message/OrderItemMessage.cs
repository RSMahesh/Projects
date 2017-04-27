using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;

namespace ResellerClub.Messages
{
    public class OrderItemMessage : IOrderItemMessage
    {
        public Guid SubPlanID { get; set; }
        public string DomainName { get; set; }
        public bool EnableSsl { get; set; }
        public bool EnableMaintenance { get; set; }
        public string InvoiceNumber { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public OrderItemMessage(Guid subPlanID, string domainName, bool enableSsl, bool enableMaintenance)
        {
            SubPlanID = subPlanID;
            DomainName = domainName;
            EnableSsl = enableSsl;
            EnableMaintenance = enableMaintenance;
            InvoiceNumber = "";
            Description = "";
        }

        public OrderItemMessage(Guid subPlanID, string domainName, bool enableSsl, bool enableMaintenance, string invoiceNumber, string description, int status)
        {
            SubPlanID = subPlanID;
            DomainName = domainName;
            EnableSsl = enableSsl;
            EnableMaintenance = enableMaintenance;
            InvoiceNumber = invoiceNumber;
            Description = description;
            Status = status;
        }

    }
}

