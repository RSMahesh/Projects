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

namespace ResellerClub.WebUI
{
    public partial class CustomerRegistration : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string authUser = ConfigurationManager.AppSettings["ResellerUserName"];
            string authPassword = ConfigurationManager.AppSettings["ResellerPassowrd"];
        
            ResellerClub.BusinessLogic.Customer customer = new ResellerClub.BusinessLogic.Customer(authUser, authPassword); ;
            customer.CusInfo.Name = input_fullname.Value;
            customer.CusInfo.Password = passwd.Value;
            customer.CusInfo.Company = input_companyname.Value;
            customer.CusInfo.Country = country.Value;
            customer.CusInfo.State = stateSelect.Value;
            customer.CusInfo.City = select_city.Value;
            customer.CusInfo.ZipCode = input_zip.Value;
            customer.CusInfo.PhoneNumber = input_phone.Value;
            customer.CusInfo.PhoneCountryCode = input_phone_cc.Value;
            customer.CusInfo.AddressLine1 = input_address1.Value + input_address2.Value;
            customer.CusInfo.Email = username.Value;
            customer.CusInfo.Language = "en";

            try
            {
                customer.Register();
                customer.GetCustomerDetailByUserName(username.Value);
                SessionM["Customer"] = customer;
                Response.Redirect("PaymentOption.aspx");
            }
            catch (ResellerClub.BusinessLogic.ServerException ex)
            {
                divError.Visible = true;
                divError.InnerHtml = "<div>" + ex.Message + " </div>";

            }

        }
    }
}