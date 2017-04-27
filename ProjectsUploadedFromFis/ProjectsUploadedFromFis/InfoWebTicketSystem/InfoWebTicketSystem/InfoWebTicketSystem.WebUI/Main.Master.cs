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

using InfoWebTicketSystem.BRL;

using ResellerClub.Interface.Messages;
using ResellerClub.Messages;

namespace InfoWebTicketSystem.WebUI
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {

                if (Request["button"] != null && Request["button"] == "Login")
                {
                    if (Request.QueryString["admin"] != null)
                    {
                        ValidateAdminUser();
                    }
                    else
                    {
                        ValidateUser();
                    }
                }

            }

        }

        public bool UserValidated()
        {
            bool rtn = false;
            if (Session["userValidated"] != null && (bool)Session["userValidated"])
            {
                rtn = true;
            }
            return rtn;
        }

        private void ValidateUser()
        {
            Customer customer = new Customer();
          
            ((CustomerInfoMessage)customer.CusInfo).Email = "maheshbailwal@gmail.com";
            ((CustomerInfoMessage)customer.CusInfo).Name = "mahesh";

            if (true)
            {
                Session["userValidated"] = true;
                //Session["userEmailId"] = customer.CusInfo.Email;
                Session["customer"] = customer;
            }
            else
            {
                this.Page.RegisterStartupScript("", "<script>$('.dialogerrorcontent').html('Invalid email or password');$('.dialogerror').show();</script>");
            }


        }

        private void ValidateAdminUser()
        {
            Admin admin = new Admin();


            if (admin.VerifyUser(Request["scemail"], Request["scpassword"]))
            {
                Session["userValidated"] = true;
                Session["admin"] = true;
                //Session["userEmailId"] = customer.CusInfo.Email;

            }
            else
            {
                this.Page.RegisterStartupScript("", "<script>$('.dialogerrorcontent').html('Invalid email or password');$('.dialogerror').show();</script>");
            }


        }
    }
}
