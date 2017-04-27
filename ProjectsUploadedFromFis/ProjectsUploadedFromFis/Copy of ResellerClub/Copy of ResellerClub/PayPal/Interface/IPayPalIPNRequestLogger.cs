using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PayPal.Interface
{
   public interface IPayPalIPNRequestLogger
    {
        void Log(HttpRequest IPNRequest);
    }
}
