using System;
using System.Collections.Generic;

using System.Text;
using ResellerClub.Interface.Messages; 

namespace ResellerClub.BusinessLogic.Messages
{
   public class DomainInfoMessage : IDomainInfoMessage 
    {
       string domainName;

       public DomainInfoMessage(string _domainName)
       {
           domainName = _domainName;
       }

       public string DomainName
        {
            get
            {
                return domainName;
            }
            set
            {
                domainName = value;
            }
        }

    }
}
