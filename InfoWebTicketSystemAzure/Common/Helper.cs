using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ResellerClub.Common
{
    public abstract class Helper
    {
        public static string GetIPAddress()
        {
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            else
            {
                string[] arr = ip.Split(new char[] { ',' });
                return arr[0];
            }
        }

        public static string GetCurrentUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static Guid GetEmptyGuid()
        {
            // Create a GUID with all zeros and compare it to Empty.
            Byte[] bytes = new Byte[16];
            return new Guid(bytes);
        }

        public static string ToProperCase(string text)
        {
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo txtInfo = cultureInfo.TextInfo;
            return txtInfo.ToTitleCase(text);
        }
    }
}
