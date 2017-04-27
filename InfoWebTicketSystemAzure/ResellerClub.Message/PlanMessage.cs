using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;

namespace ResellerClub.Messages
{
    public class PlanMessage : IPlanMessage
    {
        public Guid ProductID { get; set; }
        public Guid PlanID { get; set; }
        public Guid SubPlanID { get; set; }
        public string ProductName { get; set; }
        public int PlanSequence { get; set; }
        public int Year { get; set; }
        public decimal  Price { get; set; }

        public string PlanName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string DisplayName { get; set; }
        public bool Selling { get; set; }

        public PlanMessage(Guid productID,Guid planID, Guid subPlanID, string productName,
                           int planSequence, int year, decimal price)
        {
            ProductID = productID;
            PlanID = planID;
            SubPlanID = subPlanID;
            ProductName = productName;
            PlanSequence = planSequence;
            Year = year;
            Price = price;
        }

        public PlanMessage(Guid productID, Guid planID, Guid subPlanID, string productName,
                          int planSequence, int year, decimal price,
                           string planName, string currencyName, string currencySymbol, string displayName, bool selling)
        {
            ProductID = productID;
            PlanID = planID;
            SubPlanID = subPlanID;
            ProductName = productName;
            PlanSequence = planSequence;
            Year = year;
            Price = price;
            PlanName = planName;
            CurrencyName = currencyName;
            CurrencySymbol = currencySymbol;
            DisplayName = displayName;
            Selling = selling;
        }


        #region IPlanMessage Members


      

        #endregion
    }
}
