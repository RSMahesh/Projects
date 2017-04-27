using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoWebTicketSystem.BRL
{
  public  class Admin
    {

      public bool VerifyUser(string userName, string password)
      {
          return userName == "admin" && password == "123";
      }

    }
}
