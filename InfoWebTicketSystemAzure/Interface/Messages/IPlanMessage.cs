using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResellerClub.Interface.Messages
{
    public interface IPlanMessage
    {
        Guid ProductID { get; set; }
        Guid PlanID { get; set; }
        Guid SubPlanID { get; set; }

        string ProductName { get; set; }
        int PlanSequence { get; set; }
        int Year { get; set; }
        decimal Price { get; set; }
        string PlanName { get; set; }
        string CurrencyName { get; set; }
        string CurrencySymbol { get; set; }
        string DisplayName { get; set; }
        bool Selling { get; set; }
    }
}
