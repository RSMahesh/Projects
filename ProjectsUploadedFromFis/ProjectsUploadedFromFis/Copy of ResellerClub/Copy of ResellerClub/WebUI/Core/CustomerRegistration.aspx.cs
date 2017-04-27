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
    public partial class CustemRegistration : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();
            divError.Visible = false;

            if (IsPostBack)
            {
                if (Request["register_submit_id"] != null)
                {
                    RegisterCustomer();
                }
            }


        }

        private void RegisterCustomer()
        {
            if (!ValidateInput())
            {
                return;
            }
           
            ResellerClub.Interface.ICustomer customer = ApiObjectFactory.GetObject<ResellerClub.Interface.ICustomer>();
            //  ResellerClub.Interface.Messages.ICustomerInfoMessage cusInfo = new ResellerClub.BusinessLogic.Messages.CustomerInfoMessage();
            customer.CusInfo.Name = input_fullname.Value;
            customer.CusInfo.Password = passwd.Value;
            customer.CusInfo.Company = input_companyname.Value;
            customer.CusInfo.Country = country.Value;
            customer.CusInfo.State =string.Compare(Request[stateSelect.UniqueID], "other",StringComparison.InvariantCultureIgnoreCase)==0 ? Request[input_otherState.UniqueID] : Request[stateSelect.UniqueID];
            customer.CusInfo.City = select_city.Value;
            customer.CusInfo.ZipCode = input_zip.Value;
            customer.CusInfo.PhoneNumber = input_phone.Value;
            customer.CusInfo.PhoneCountryCode = input_phone_cc.Value;
            customer.CusInfo.AddressLine1 = input_address1.Value;
            customer.CusInfo.Email = username.Value;
            customer.CusInfo.Language = "en";

            try
            {
                customer.Register();
                customer.GetCustomerDetailByUserName(username.Value);
               
                Guid sessionFID = Guid.NewGuid();
                SessionM["Customer"] = customer;
                SessionM["SessionFID"] = sessionFID;
                ResellerClub.Interface.ISessionLogger sessionLogger = ApiObjectFactory.GetObject<ResellerClub.Interface.ISessionLogger>();
                sessionLogger.Insert(sessionFID,
                                     SessionM.AspSessionId(),
                                     customer.CusInfo.Email,
                                     UserIPAddress());


                if (UserCart.Items.Count > 0)
                    Response.Redirect(Application["rootPath"].ToString() + "/Payment/PaymentOption.aspx");
                else
                    Response.Redirect(Application["rootPath"].ToString() + "/Home.aspx");

            }
            catch (ResellerClub.BusinessLogic.ServerException ex)
            {
                ShowErrorMessage(ex.Message);
            }

        }

        private bool  ValidateInput()
        {
            if (passwd.Value != conf_passwd.Value)
            {
                ShowErrorMessage("Password and Confirm Password does not match");
                return false;
            }
            return true;
        }

        private void ShowErrorMessage(string message)
        {

            divError.Visible = true;
            divError.InnerHtml = "<div>" + message + " </div>";
     
        }
    }
}
