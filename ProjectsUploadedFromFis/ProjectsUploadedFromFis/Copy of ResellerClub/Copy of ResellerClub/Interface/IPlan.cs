using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;

namespace ResellerClub.Interface
{
    public interface IPlan : IBaseInterface 
    {
        List<IPlanMessage> GetPlans();
        void InsertPlan(string productName, int PlanSequence, int year, decimal price, string planName, string currencyName, string currencySymbol, Nullable<Guid> subPlanID, Nullable<Guid> planID, string displayName, bool selling);
    }
}
