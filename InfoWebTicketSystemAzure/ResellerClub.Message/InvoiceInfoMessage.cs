using System;
using System.Collections.Generic;

using System.Text;
using ResellerClub.Interface.Messages; 

namespace ResellerClub.Messages
{
    public class InvoiceInfoMessage : IInvoiceInfoMessage
    {
       public  string Domain { get; set; }
       public string Description { get; set; }
       public string InvioceNumber { get; set; }
       public decimal Amount { get; set; }
       public string Response { get; set; }
       public int Status { get; set; }

       public InvoiceInfoMessage()
       {
           this.Domain = this.Description = this.InvioceNumber = this.Response = "";

      }

    }
}
