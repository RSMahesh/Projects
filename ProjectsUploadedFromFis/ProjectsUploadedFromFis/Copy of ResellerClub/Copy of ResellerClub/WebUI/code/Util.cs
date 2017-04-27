using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ResellerClub.Interface;

namespace ResellerClub.WebUI
{
    abstract class Util
    {
        public static string RenderControl(Control ctrl)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            ctrl.RenderControl(hw);
            return sb.ToString();
        }
        public static string ObjectToJason(object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                        new System.Web.Script.Serialization.JavaScriptSerializer();

            return serializer.Serialize(obj);
        }

        public static string GetUserEmailByOrderId(string orderId, out string orderNumber)
        {
            IOrder order = ApiObjectFactory.GetObject<IOrder>();
            var dt = order.GetOrder(new Guid(orderId));
            orderNumber = dt.Rows[0]["OrderNumber"].ToString();
            Guid sessionID = (Guid)dt.Rows[0]["SessionID"];
            ISessionLogger sessionLog = ApiObjectFactory.GetObject<ISessionLogger>();
            return sessionLog.GetUserEmail(sessionID);

        }

        public static string GetHTMLFileContent(string filePath)
        {
            return File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/HTML/" + filePath));
        }

        public static string FormatTo2DecimalPlace(decimal value)
        {
            return String.Format("{0:0.00}", value);
        }

       
    }
}
