using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using ResellerClub.Interface.Messages;
using ResellerClub.Interface;
using ResellerClub.Common;
using System.IO;

namespace ResellerClub.WebUI
{
    public partial class InvoiceDetail : BasePage
    {
        public string OrderNumber;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["SendInvoiceEmail"] != null)
            {
                this.Page.MasterPageFile = "~/MasterPage/Main.Master";
                var orderId = new Guid(Request.QueryString["rid"]);
                SessionM["OrderId"] = orderId;
                var userEmail = Util.GetUserEmailByOrderId(Request.QueryString["rid"], out OrderNumber);
                LoadCustomer(userEmail);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();
            UserCart.RemoveAllItem();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            TextWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            IOrder order = ApiObjectFactory.GetObject<IOrder>();
            var dt = order.GetOrder((Guid)SessionM["OrderId"]);

            InvoiceDetail1.RenderControl(hw);

            var customer = (ICustomer)SessionM["Customer"];

            OrderNumber = dt.Rows[0]["OrderNumber"].ToString();

            Dictionary<string, string> replaceString = new Dictionary<string, string>();
            replaceString["<%=CustomerName%>"] =Helper.ToProperCase( customer.CusInfo.Name);
            replaceString["<%=CustomerId%>"] = customer.CusInfo.CustomerID;
            replaceString["<%=OrderDetail%>"] = tw.ToString();
            replaceString["<%=OrderNumber%>"] = dt.Rows[0]["OrderNumber"].ToString();
            SendEmail("Order.htm", "Order Detail", customer.CusInfo.Email, replaceString);
        }
        private void LoadCustomer(string email)
        {
            ICustomer customer = ApiObjectFactory.GetObject<ResellerClub.Interface.ICustomer>();
            customer.GetCustomerDetailByUserName(email);
            SessionM["Customer"] = customer;

        }
    }
}
