using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Runtime
    {
        public static string GetCurrentUserName()
        {
           return System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').LastOrDefault();
        }
    }
}
