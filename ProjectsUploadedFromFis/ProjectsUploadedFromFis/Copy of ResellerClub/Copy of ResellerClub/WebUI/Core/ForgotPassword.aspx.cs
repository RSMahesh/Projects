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
using ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class ForgotPassword : BasePage
    {
        public string message = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string password = "";
            if (!IsPostBack)
            {
                txtEmailID.Text = Request.QueryString["useremail"];
            }

            if (IsPostBack)
            {
                var customer = ApiObjectFactory.GetObject<ResellerClub.Interface.ICustomer>();

                var success = customer.GenerateTempPassword(txtEmailID.Text, out message, out password);
                if (success)
                {
                    RegisterStartUpScript("$('#divContainer').hide();");

                    message = "Your password have been mailed to " + txtEmailID.Text + "<br>" +
                         "Be sure to check your Junk folder if you do not see an email from us in your Inbox within a few minutes";

                    Dictionary<string, string> replaceString = new Dictionary<string, string>();
                    replaceString["<%=useremail%>"] = customer.CusInfo.Email;
                    replaceString["<%=CustomerId%>"] = customer.CusInfo.CustomerID;
                    replaceString["<%=password%>"] = password;
                    replaceString["<%=userIpAddress%>"] = Helper.GetIPAddress();

                    SendEmail("Forgot_Password.htm", "Password Retrieval for your Infoweb services account", customer.CusInfo.Email, replaceString);
                    DisplayInfo(message);
                }
                else
                {
                    DisplayError(message);
                }
            }

        }
    }
}

