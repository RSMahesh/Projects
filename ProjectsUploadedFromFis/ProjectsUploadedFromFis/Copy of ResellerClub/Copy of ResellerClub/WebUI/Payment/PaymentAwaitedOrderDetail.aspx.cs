using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using ResellerClub.Interface.Messages;
using ResellerClub.Common;
using ResellerClub.Interface;
using System.Collections.Generic;

namespace ResellerClub.WebUI
{
    public partial class PaymentAwaitedOrderDetail : BasePage
    {
        public string Email = "";

        public string OrderNumber = "";
    
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["ViewOrderDetail"] != null)
            {
                this.Page.MasterPageFile = "~/MasterPage/Blank.Master";
            }
        }
          
        protected void Page_Load(object sender, EventArgs e)
        {
            
            TurnOffCache();
          //  InvoiceDetail1.GetDescription += GetItemDescription;
            InvoiceDetail1.PaymentAwaited = true;
            divOrderDetail.Visible = false;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            string paymentMode = "";
            if (Request.QueryString["ViewOrderDetail"] != null)
            {
                LoadOrder(out paymentMode);
                divUserMsg.Visible = false;
            }
            else
            {
                LoadOrder(out paymentMode);
                SendEmail(paymentMode);
                UserCart.RemoveAllItem();
                ShowUserMessage(paymentMode);
            }
        }
       
        private void ShowUserMessage(string paymentMode)
        {
            switch (paymentMode)
            {
                case Constant.PaymentModeNetBanking:
                case Constant.PaymentModeIMPS:
                    spnOnlinePaymentMsg.Visible = true;
                    spnOfflinePaymentMsg.Visible = false;
                    break;
                case Constant.PaymentModeOffLine:
                    spnOnlinePaymentMsg.Visible = false;
                    spnOfflinePaymentMsg.Visible = true;
                    break;

            }

        }

        private string GetItemDescription(IOrderItemMessage orderItem)
        {
           var p = Plan.GetPlanBySubPlanId(orderItem.SubPlanID);
           return Constant.GetItemDescription(orderItem.DomainName, p.ProductName,p.DisplayName);
        }

        private string GetHTMLContent()
        {
            return Util.GetHTMLFileContent("IMPS.htm");
        }

        private void LoadOrder(out string paymentMode)
        {
            divOrderDetail.Visible = true;
            IOrder order = ApiObjectFactory.GetObject<IOrder>();
            var dt = order.GetOrder((Guid)SessionM["OrderId"]);
            OrderNumber = dt.Rows[0]["OrderNumber"].ToString();
            paymentMode = dt.Rows[0]["PaymentMode"].ToString();
        }

        private void SendEmail(string paymentMode)
        {
            TextWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            IOrder order = ApiObjectFactory.GetObject<IOrder>();
            var dt = order.GetOrder((Guid)SessionM["OrderId"]);
            InvoiceDetail1.RenderControl(hw);
            var customer = (ICustomer)SessionM["Customer"];
            Email = customer.CusInfo.Email;
            Dictionary<string, string> replaceString = new Dictionary<string, string>();
            replaceString["<%=CustomerName%>"] = Helper.ToProperCase(customer.CusInfo.Name);
            replaceString["<%=CustomerId%>"] = customer.CusInfo.CustomerID;
            replaceString["<%=OrderDetail%>"] = tw.ToString();
            replaceString["<%=OrderNumber%>"] = dt.Rows[0]["OrderNumber"].ToString();

            string orderUpdateLink = Request.Url.AbsoluteUri;
            orderUpdateLink = orderUpdateLink.Replace(Request.FilePath, "/Payment/UpdatePaymentReference.aspx");

            orderUpdateLink = "<a href=" + orderUpdateLink + "?rid=" + SessionM["OrderId"].ToString() + " >" + orderUpdateLink + "</a>";


            replaceString["<%=OrderUpdateLink%>"] = orderUpdateLink;

            SendEmail(GetEmailHtmlFile(paymentMode), "Order Detail", customer.CusInfo.Email, replaceString);

        }

        private string GetEmailHtmlFile(string paymentMode)
        {
            string filePath = "";
            switch (paymentMode)
            {
                case Constant.PaymentModeNetBanking:
                case Constant.PaymentModeIMPS:
                   filePath = "PaymentAwaited_Order_Online.htm";
                break;
                case Constant.PaymentModeOffLine:
                filePath = "PaymentAwaited_Order_Offline.htm"; 
                break;
            }

            return filePath;
        }

    }
}
