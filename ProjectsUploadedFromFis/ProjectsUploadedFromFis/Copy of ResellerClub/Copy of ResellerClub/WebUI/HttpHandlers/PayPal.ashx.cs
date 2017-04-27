using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using System.Configuration;
using System.Web.SessionState;
using ResellerClub.Common;

namespace ResellerClub.WebUI.HttpHandlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class PayPal : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //   context.Response.ContentType = "text/plain";
            context.Response.Clear();
            SessionManager SessionM = new SessionManager();
            var processor = ApiObjectFactory.GetObject<ResellerClub.Interface.IPaymentProcessor>();
            var order = ApiObjectFactory.GetObject<ResellerClub.Interface.IOrder>();
            
            var settings = new Dictionary<string, string>();
            settings["BusinessEmail"] = ConfigurationManager.AppSettings["BusinessEmail"]; ;
            settings["ReturnUrl"] = ConfigurationManager.AppSettings["ReturnUrl"];
            settings["NotifyUrl"] = ConfigurationManager.AppSettings["NotifyUrl"];
            settings["CancelPurchaseUrl"] = ConfigurationManager.AppSettings["CancelPurchaseUrl"]; ;
            var strRequest = processor.CreateRequest(((Cart)SessionM["Cart"]).Items, (Guid)SessionM["OrderId"], ConfigurationManager.AppSettings["PayPalUrl"], settings);
            context.Response.Write(strRequest);
            processor.InsertTransactionLog(strRequest, (Guid)SessionM["OrderId"]);
            order.UpdateOrderStatus((Guid)SessionM["OrderId"], Constant.OrderStatusSentToPaymentProcessor); 
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}
