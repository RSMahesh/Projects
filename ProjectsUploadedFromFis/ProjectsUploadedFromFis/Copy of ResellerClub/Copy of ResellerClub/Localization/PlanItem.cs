using System;
using System.Collections.Generic;

using System.Text;

namespace ResellerClub.Localization
{
    public class PlanItem
    {
      
        public int Years { get; set; }
        public double Price { get; set; }

        public PlanItem(int years, double price)
        {
            Years = years;
            Price = price;
        }

        //int years;
        //int price;
        //Duration priceDuration;

        //public string PriceDuration
        //{
        //    get { return priceDuration.ToString(); }
           
        //}

        //public PlanItem(int years, int price):this(years,price,Duration.month)
        //{
        //}

        //public PlanItem(int years, int price, Duration priceDuration)
        //{
        //    this.years = years;
        //    this.price = price;
        //    this.priceDuration  = priceDuration;
        //}
        //public int Price
        //{
        //    get { return price; }
        //}

        //public int Years
        //{
        //    get { return years; }
        //}

        //public int AnnualPrice
        //{
        //    get {
        //        if (priceDuration == Duration.month)
        //            return (years * 12) * price;
        //        else
        //            return years * price;
            
        //    }
        //}

    }
}
