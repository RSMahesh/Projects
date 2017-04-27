using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages; 
namespace ResellerClub.Messages
{
    class CartInfoMessage : ICartInfoMessage 
    {
       public  string ItemCode { get; set; }
       public int Quantity { get; set; }
       public double UnitAmount { get; set; }
       public string Status { get; set; }
        
      // public CartInfoMessage(string itemCode,
       }
    
}
