using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface.Messages;

namespace ResellerClub.Messages
{
    public class TopLevelDomianMessage : ITopLevelDomianMessage
    {
       public  string Name { get; set; }
       public  string Category { get; set; }
       public  Guid   PlanID { get; set; }

        public TopLevelDomianMessage(string name, string category, Guid planID)
        {
            Name = name;
            Category = category;
            PlanID = planID;
        }
    }
}
