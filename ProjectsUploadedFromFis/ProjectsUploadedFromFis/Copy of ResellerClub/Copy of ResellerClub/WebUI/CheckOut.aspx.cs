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
using System.IO;
using ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class CheckOut : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["login"] != null)
            {
                if (Request.QueryString["login"] == "1")
                {
                    divHeaderPlaceOrder.Visible = false;
                    OderDetail1.Visible = false;
                }

                if (Request.QueryString["login"] == "2")
                {
                    SessionM.Abandon();
                    
                    Response.Redirect(Application["rootPath"].ToString() + "/Home.aspx");
                }
            }

            if (SessionM["Customer"] != null)
            {
                Response.Redirect(Application["rootPath"].ToString()+"/Payment/PaymentOption.aspx");
            }
            HideParentPageHeading();
            CurrentModule = ModuleNames.Domain;
            divError.Style["display"] = "none";
            trRegistrationForm.Style["display"] = "none";


            if (IsPostBack)
            {
                trRegistrationForm.Style["display"] = "";
                trUserOption.Style["display"] = "none";

                if (Request["existing_submit"] != null)
                {
                    ExistingCustomer();
                }

                if (Request["new_submit"] != null)
                {
                    NewCustomer();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        private void ExistingCustomer()
        {
            ApiObjectFactory.GetObject<ResellerClub.Interface.IDomain>();
            //     ResellerClub.BusinessLogic.Customer customer = new ResellerClub.BusinessLogic.Customer(authUser, authPassword);
            ResellerClub.Interface.ICustomer customer = ApiObjectFactory.GetObject<ResellerClub.Interface.ICustomer>();


            //     customer.GetCustomerDetailByUserName(Request["input_email"]);
            trRegistrationForm.Style["display"] = "none";
            trUserOption.Style["display"] = "";

            if (customer.ValidateCustomer(Request["input_email"], Request["password"]))
            {
                Guid sessionFID = Guid.NewGuid();
                SessionM["Customer"] = customer;
                SessionM["SessionFID"] = sessionFID;
                ResellerClub.Interface.ISessionLogger sessionLogger = ApiObjectFactory.GetObject<ResellerClub.Interface.ISessionLogger>();
                sessionLogger.Insert(sessionFID,
                                     SessionM.AspSessionId(),
                                     customer.CusInfo.Email,
                                     UserIPAddress());

            
                RedirectAfterLogin();

            }
            else
            {
                divError.Style["display"] = "";
                divError.InnerHtml = "<div>Invalid Email ID or Password</div>";

            }

        }

        private void NewCustomer()
        {
            Response.Redirect(Application["rootPath"].ToString() + "/Core/CustomerRegistration.aspx");
        }

        private void RedirectAfterLogin()
        {
            if (Request.QueryString["login"] != null)
            {
                Response.Redirect(Application["rootPath"].ToString() +"/Home.aspx");
            }
            else
            {
                Response.Redirect(Application["rootPath"].ToString() +"/Payment/PaymentOption.aspx");
            }
        }
    }
}
