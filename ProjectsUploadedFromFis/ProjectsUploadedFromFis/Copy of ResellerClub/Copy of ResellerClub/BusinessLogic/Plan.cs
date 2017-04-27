using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface;
using ResellerClub.Interface.Messages;
using ResellerClub.DataAccess;


namespace ResellerClub.BusinessLogic
{
    public class Plan :BaseBRL, IPlan
    {
        public List<IPlanMessage> GetPlans()
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var dalPlan = new ResellerClub.DataAccess.Plan(ConnectionFactory.GetConnection());
                return dalPlan.GetPlans();
            }
        }

        public void InsertPlan(string productName, int PlanSequence, int year, decimal price, string planName, string currencyName, string currencySymbol, Nullable<Guid> subPlanID, Nullable<Guid> planID, string displayName, bool selling)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var dalPlan = new ResellerClub.DataAccess.Plan(ConnectionFactory.GetConnection());
                dalPlan.InsertPlan(productName, PlanSequence, year, price, planName, currencyName, currencySymbol,subPlanID,planID,displayName,selling);
            }
        }

       
    }
}
