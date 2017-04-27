using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;

namespace ResellerClub.Localization
{
    public class Plan 
    {
        public List<PlanItem> planItems;
        private string planId;

        public string PlanId
        {
            get { return planId; }
        }

        int startingPrice;

        public int StartingPrice
        {
            get { return startingPrice; }
        }

        public Plan(int startingFrom, string planId)
        {
            planItems = new List<PlanItem>();
            this.planId = planId;
            this.startingPrice = startingFrom;
        }

        public void AddItem(int years, int price, Duration priceduration)
        {
            planItems.Add(new PlanItem(years, price));
        }

        public PlanItem this[int index]
        {
            get
            {
                return planItems[index];
            }
            set
            {
                planItems[index] = value;
            }
        }

      

       

    }

    //===========================================================
  
}
