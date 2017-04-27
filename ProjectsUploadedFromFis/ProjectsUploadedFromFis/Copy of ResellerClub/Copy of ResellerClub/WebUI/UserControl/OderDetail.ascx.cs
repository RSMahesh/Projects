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
using System.Collections.Generic;
using APIC = ResellerClub.Common;
using ResellerClub.Interface.Messages;
using ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class OderDetail : BaseUserControl
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            Repeater1.DataSource = ParentBasePage.UserCart.Items;
            Repeater1.DataBind();

            ParentBasePage.GetPlansJSON();

            if (IsPostBack)
            {
                if (Request.QueryString["login"] != null && Request.QueryString["login"] == "1")
                {
                    return;
                }
                UpdateCart();
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                var orderItem = (IOrderItemMessage)e.Item.DataItem;
                string labeltext = "";
                var plan = Plan.GetPlanBySubPlanId(orderItem.SubPlanID);
                labeltext = Constant.GetItemDescription(orderItem.DomainName, plan.ProductName,plan.DisplayName);

                ((Label)e.Item.FindControl("lblItem")).Text = labeltext;
                DropDownList ddlPlan = e.Item.FindControl("ddlPlan") as DropDownList;
                ddlPlan.ID = orderItem.DomainName + "_" + orderItem.SubPlanID + "_ddlPlan";
                string selectValue = orderItem.SubPlanID.ToString();
                ParentBasePage.BindPlan(orderItem.SubPlanID, ref ddlPlan);
                ddlPlan.SelectedValue = selectValue;
            }

        }

        public string GetSubPlanIdAndDomain(object obj)
        {
            var item = (IOrderItemMessage)obj;
            return item.SubPlanID.ToString() + Constant.splitChar + item.DomainName;
        }

        private void UpdateCart()
        {

            var domainCart = (Cart)SessionM["Cart"];
            if (domainCart != null)
            {
                for (int indx = 0; indx < domainCart.Items.Count; indx++)
                {
                    var key = Request.Form.AllKeys.First(x => x.EndsWith(domainCart.Items[indx].DomainName + "_" + domainCart.Items[indx].SubPlanID + "_ddlPlan"));

                    if (key != null && key.Trim() != "")
                    {
                        domainCart.Items[indx].SubPlanID = new Guid(Request.Form[key]);
                    }
                }
                SessionM["DomainCart"] = domainCart;
            }

        }

        public string CurrencySymbol
        {
            get
            {
                return Plan.GetCurrencySymbol();
            }
        }
    }
}