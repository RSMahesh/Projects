using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using APIC = ResellerClub.Common;
using ResellerClub.BusinessLogic;
using ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class PaymentOption : BasePage
    {
        private string paymentMode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            HideParentPageHeading();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                if (Request["payonline_submit"] != null)
                {
                    switch (Request["rdoOnline"])
                    {
                        case "paypal":
                            PayPal();
                            break;
                        case "imps":
                            Imps();
                            break;
                        case "netbanking":
                            Imps();
                            break;
                    }


                    if (Request["rdoOnline"] == "paypal")
                    {
                        PayPal();
                    }
                    if (Request["rdoOnline"] == "imps")
                    {
                        Imps();
                    }
                }

                if (Request["payoffline_submit"] != null)
                {
                    OffLine();
                }
            }
        }

        private void OffLine()
        {
            paymentMode = Constant.PaymentModeOffLine;
            SaveOrder();
            Response.Redirect("PaymentAwaitedOrderDetail.aspx");
        }


        private void NetBanking()
        {
            paymentMode = Constant.PaymentModeNetBanking;
            SaveOrder();
            Response.Redirect("PaymentAwaitedOrderDetail.aspx");
        }

        private void Imps()
        {
            paymentMode = Constant.PaymentModeIMPS;
            SaveOrder();
            Response.Redirect("PaymentAwaitedOrderDetail.aspx");
        }

        private void PayPal()
        {
            paymentMode = Constant.PaymentModePayPal;
            SaveOrder();
            var order = new Order();
            var orderID = (Guid)SessionM["OrderId"];

            if (ConfigurationManager.AppSettings["ProcessWithoutPayment"] != null &&
                ConfigurationManager.AppSettings["ProcessWithoutPayment"] == "true")
            {
                order.UpdateOrderStatus(orderID, Constant.OrderStatusPaymentVerified);
                order.ProcessOrder(orderID);
                Response.Redirect("../InvoiceDetail.aspx");
            }
            else
            {
                Response.Redirect("../HttpHandlers/PayPal.ashx");
                //Server.Transfer("HttpHandlers/PayPal.ashx");
            }

        }


        private void SaveOrder()
        {
            var order = ApiObjectFactory.GetObject<ResellerClub.Interface.IOrder>();
            var cartItems = UserCart.Items;
            decimal amount = cartItems.Sum(x => Plan.GetPlanBySubPlanId(x.SubPlanID).Price);
            Plan.GetPlanBySubPlanId(cartItems[0].SubPlanID);
            var orderId = order.SaveOrder(UserCart.Items, (Guid)SessionM["SessionFID"], amount);
            SessionM["OrderId"] = orderId;
            order.UpdatePaymentMode(orderId, paymentMode);
        }

    }
}
