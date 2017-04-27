using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.DataAccess;
using ResellerClub.Interface.Messages;

namespace ResellerClub.BusinessLogic
{
   public  class ConversionRate: BaseBRL 
    {

       private  decimal ConvertRupeeToDollar(decimal rupeeAmount)
       {
           decimal conversionRate;
           using (var connection = ConnectionFactory.GetConnection())
           {
               var conversion = new ResellerClub.DataAccess.ConversionRate(connection);
               conversionRate = conversion.GetUSDToRupeeRate();
           }

           decimal dollar = rupeeAmount / conversionRate;
           return decimal.Round(dollar, 2, MidpointRounding.AwayFromZero);
       }

       public decimal ConvertRupeeToDollar(IPlanMessage plan)
       {

           if (plan.CurrencyName != "USD")
           {
               return ConvertRupeeToDollar(plan.Price);
           }
           else
              return  plan.Price;

       }
 
   }
}
