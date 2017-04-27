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
using ResellerClub.Interface.Messages;
using ResellerClub.Common;
using ResellerClub.Interface;
using System.Collections.Generic;

namespace ResellerClub.WebUI.UserControl
{
    public partial class InvoiceDetail : BaseUserControl 
    {
        decimal amount = 0;
        public Func<IOrderItemMessage,string> GetDescription;
        public bool PaymentAwaited = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            IOrder order = ApiObjectFactory.GetObject<IOrder>();
            List<IOrderItemMessage> orderItems = order.GetOrderItem((Guid)SessionM["OrderId"]);
            decimal tax = 0;
            decimal taxPercentage = decimal.Parse(ConfigurationManager.AppSettings["ServiceTax"]);

            Repeater1.DataSource = orderItems;
            Repeater1.DataBind();

            spnSamt.InnerText = Plan.GetCurrencySymbol() + " "  + String.Format("{0:0.00}",amount);
            tax = (amount * taxPercentage) / 100;
            amount += tax;
            spnTax.InnerText = Plan.GetCurrencySymbol() + " " + String.Format("{0:0.00}", tax); ;
            spnTamt.InnerText = Plan.GetCurrencySymbol() + " " + String.Format("{0:0.00}", amount);
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.DataItem != null)
                {
                    IOrderItemMessage orderItem = (IOrderItemMessage)e.Item.DataItem;
                    ((Label)e.Item.FindControl("lblItem")).Text = orderItem.DomainName;
                    if (GetDescription == null)
                    {
                        ((Label)e.Item.FindControl("lblActionDecscription")).Text = GetItemDiscription(orderItem);
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lblActionDecscription")).Text = GetDescription(orderItem);
                    }

                    if (PaymentAwaited)
                    {
                        ((Label)e.Item.FindControl("lblInvoiceId")).Text = "Payment Awaited";
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lblInvoiceId")).Text = orderItem.InvoiceNumber.Trim() == "" ? "Under Process" : orderItem.InvoiceNumber;
                    }
                 
                    ((Label)e.Item.FindControl("lblPrice")).Text = Plan.GetCurrencySymbol() + Plan.GetPlanBySubPlanId(orderItem.SubPlanID).Price.ToString();
                    amount += Plan.GetPlanBySubPlanId(orderItem.SubPlanID).Price;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private string GetItemDiscription(IOrderItemMessage orderItem)
        {
            string description = "";
            IPlanMessage planMsg = null;
            switch (orderItem.Status)
            {
                case Constant.OrderItemStatusProcessed:
                 planMsg =  Plan.GetPlanBySubPlanId(orderItem.SubPlanID);
                 description = Constant.GetItemDescription("", planMsg.ProductName, planMsg.DisplayName) +
                     " for " + planMsg.Year.ToString()+(planMsg.Year > 1 ? " years" : " year");
                 break;

                default:
                 planMsg = Plan.GetPlanBySubPlanId(orderItem.SubPlanID);
                 description = Constant.GetItemDescription("", planMsg.ProductName, planMsg.DisplayName) +
                     " for " + planMsg.Year.ToString() + (planMsg.Year > 1 ? " years" : " year");
                 break;

            }
            return description;
        }

    }
}