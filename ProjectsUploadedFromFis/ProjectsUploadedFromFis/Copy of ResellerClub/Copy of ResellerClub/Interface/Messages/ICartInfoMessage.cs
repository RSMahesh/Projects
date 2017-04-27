using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResellerClub.Interface.Messages
{
   public  interface ICartInfoMessage
    {
        string ItemCode { get; set; }
        int Quantity { get; set; }
        double UnitAmount { get; set; }
        string Status { get; set; }
    }
}
