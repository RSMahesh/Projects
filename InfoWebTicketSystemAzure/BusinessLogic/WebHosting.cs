using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API
{
   public class WebHosting : BaseBRL 
    {
        public WebHosting(string authUser, string authPassword)
            : base(authUser, authPassword) { }
      

        public void GetPalin()
        {
              object  searchResult = ParseJsonResponse( PostUrl(GetInitalUrl("products/plan-details.json")));

        }
        
    }
}
