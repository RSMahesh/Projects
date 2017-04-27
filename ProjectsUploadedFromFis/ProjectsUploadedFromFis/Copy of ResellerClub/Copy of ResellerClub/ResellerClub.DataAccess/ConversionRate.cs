using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseConnectionProvider.Interface;

namespace ResellerClub.DataAccess
{
   public class ConversionRate:DALBase 
    {
       public ConversionRate(IConnection connection)
           : base(connection)
        {
        
        }

         public decimal GetUSDToRupeeRate()
         {
             string cmdText = "select USDToRupee from  [ConversionRate] with(nolock) ";
              connection.Select(cmdText);
              return (decimal)connection.ExecuteScalar();
         
        }
         
    }
}
