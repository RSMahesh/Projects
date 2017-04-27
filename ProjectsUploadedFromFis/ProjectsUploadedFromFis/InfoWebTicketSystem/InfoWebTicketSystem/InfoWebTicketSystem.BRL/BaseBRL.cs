using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoWebTicketSystem.BRL
{
   public  class BaseBRL
    {
       protected  Guid GetEmptyGuid()
       {
           // Create a GUID with all zeros and compare it to Empty.
           Byte[] bytes = new Byte[16];
           return new Guid(bytes);
       }
    }
}
