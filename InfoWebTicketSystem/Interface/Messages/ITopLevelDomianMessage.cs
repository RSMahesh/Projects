using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResellerClub.Interface.Messages
{
  public  interface ITopLevelDomianMessage
    {
        string Name { get; set; }
        string Category { get; set; }
        Guid PlanID { get; set; }
    }
}
