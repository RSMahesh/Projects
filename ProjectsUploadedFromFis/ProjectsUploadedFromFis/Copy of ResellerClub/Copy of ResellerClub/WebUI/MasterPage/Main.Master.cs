using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ResellerClub.Interface;
using ResellerClub.WebUI.code;
using ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class Main : System.Web.UI.MasterPage
    {
        public string cartItemCount = "0";
        public string loginText = "Login";
        public string loginPara = "1";
        public SessionManager SessionM = null;
        public string UserName = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            SessionM = new SessionManager();

            ddlCountry.Attributes["onchange"] = "OnCountryChange(this)";
            if (SessionM["country"] == null)
            {
                SessionM["country"] = ddlCountry.SelectedValue;
            }

            if (SessionM.SessionExist())
            {
                if (String.Compare(SessionM["country"].ToString(), "india", StringComparison.InvariantCultureIgnoreCase) == 0)
                    imgFlag.Src = "../images/india_flag.gif";
                else
                    imgFlag.Src = "../images/usa_flag.gif";
            }

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            cartItemCount = "0";

            if (SessionM["Cart"] != null)
            {
                var cart = (Cart)SessionM["Cart"];
                cartItemCount = cart.Items.Count.ToString();
            }

            if (SessionM["Customer"] != null)
            {
                var customer = (ICustomer)SessionM["Customer"];
                UserName = "Hello " + customer.CusInfo.Name;

                loginText = "SignOut";
                loginPara = "2";
                UserName = Helper.ToProperCase(UserName);
            }

            if(SessionM["country"] != null)
            ddlCountry.SelectedValue = SessionM["country"].ToString();
        }


        protected string __(string key)
        {
            return LanguageSetting.GetString(key);
        }

        protected string __()
        {
            return LanguageSetting.GetLanguage();
        }

        protected string GetStyleSheet()
        {
            return @"<link rel='stylesheet' type='text/css' href='" + Application["rootPath"] + "/StyleSheet/" + LanguageSetting.GetLanguage() + "_MASTERPAGE.css' />";
                   // @"<link rel='stylesheet' type='text/css' href='../StyleSheet/" + LanguageSetting.GetLanguage() + "_MASTERPAGE.css' />";
        }
    }
}
