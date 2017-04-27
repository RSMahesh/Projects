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

namespace ResellerClub.WebUI
{
    public partial class ChangePassword : BasePage
    {
        private  string message;
        protected void Page_Load(object sender, EventArgs e)
        {
            divUserMsg.Style["display"] = "none";

            if (IsPostBack)
            {
              var customer =  ApiObjectFactory.GetObject<ResellerClub.Interface.ICustomer>();
                message = "";
                var success = customer.ChangePassword(txtEmailID.Text, txtOldPassword.Text, txtPassword.Text, out message);
                divUserMsg.Style["display"] = "block";
                if (success)
                {
                    DisplayInfo(message);
                    RegisterStartUpScript("$('#divContainer').hide();");

                    Dictionary<string, string> replaceString = new Dictionary<string, string>();
                    replaceString["<%=useremail%>"] = customer.CusInfo.Email;
                    replaceString["<%=CustomerId%>"] = customer.CusInfo.CustomerID;


                    SendEmail("New_User_Registration_Email_Format.htm", "Confirmation of your Customer Registration with InfoWeb Services", customer.CusInfo.Email, replaceString);

                }
                else
                {
                    DisplayError(message);
                }
            }
        }
    }
}
