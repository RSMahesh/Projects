using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Net;
using ResellerClub.Interface;
using ResellerClub.Interface.Messages;
using ResellerClub.Common.Logging;
using ResellerClub.Common;


namespace ResellerClub.BusinessLogic
{
    public class Customer : BaseBRL, ICustomer
    {
        ICustomerInfoMessage cusInfo;
        internal string __password;

        const string adminAreaUrl = "http://webmasters.infowebservices.in/servlet/AutoLoginServlet?role=customer&langpref=en&newFlow=true&userLoginId=";

        public Customer()
        {
          //  cusInfo = new Messages.CustomerInfoMessage();
        }

        public Customer(string authUser, string authPassword)
            : base(authUser, authPassword)
        {
            cusInfo = new Messages.CustomerInfoMessage();
        }

        public void Register()
        {
            string customerResgisterUrl = string.Format("&username={0}&passwd={1}&name={2}&company={3}&address-line-1={4}&city={5}&state={6}&country={7}&zipcode={8}&phone-cc={9}&phone={10}&lang-pref={11}",
                new object[] { cusInfo.Email, cusInfo.Password, cusInfo.Name, cusInfo.Company.Trim() == "" ? "UNKNOWN" : cusInfo.Company, cusInfo.AddressLine1, cusInfo.City, cusInfo.State, cusInfo.Country, cusInfo.ZipCode, cusInfo.PhoneCountryCode, cusInfo.PhoneNumber, cusInfo.Language }
                );
            PostUrl(GetInitalUrl("customers/signup.xml") + customerResgisterUrl);
           
        }

        public ICustomerInfoMessage CusInfo
        {
            get { return cusInfo; }
            set { cusInfo = value; }
        }

        #region PublicMethods

        public void GetCustomerContactInfo(string customerID)
        {
            string customerContactInfoUrl = "&customer-id=" + customerID + "&type=Contact&type=CnContact";
            string response = PostUrl(GetInitalUrl("contacts/default.json") + customerContactInfoUrl);
            ParseCustomerContactInfoResponse(ParseJsonResponse(response));
        }

        public bool GetCustomerDetailByUserName(string userEmail)
        {
            try
            {
                string customerDetailInfoUrl = "&username=" + userEmail;
                string response = PostUrl(GetInitalUrl("customers/details.json") + customerDetailInfoUrl);
                ParseCustomerDetailResponse(ParseJsonResponse(response));
                
            }
            catch (ServerException ex)
            {
                LogException(ex, "");
                return false;
            }
            return true;
        }

        public bool ValidateCustomer(string userEmail, string password)
        {
            //     &username=username@customer.com&passwd=customerpassword
            try
            {
                string validateUrl = "&username=" + userEmail + "&passwd=" + password + "&ip=1.1.1.1";
                string response = PostUrl(GetInitalUrl("customers/generate-token.json") + validateUrl);
                GetCustomerDetailByUserName(userEmail);
                __password = password;
            }
            catch (ServerException ex)
            {
                LogException(ex, "");
                return false;
            }
            return true;
        }

        public string GetAdminUrlWithToken()
        {

            try
            {
                string ip = Helper.GetIPAddress();
                string generateTokenUrl = "&username=" + cusInfo.Email + "&passwd=" + __password + "&ip=" + ip;
                string token = PostUrl(GetInitalUrl("customers/generate-token.json") + generateTokenUrl);
                //   string token = ParseJsonResponse(response).ToString();
                string authenticateTokenUrl = "&token=" + token.Replace("\"", "").Replace("\\", "");
                object obj = ParseJsonResponse(PostUrl(GetInitalUrl("customers/authenticate-token.json") + authenticateTokenUrl));

                //"http://cp.theadm.in/servlet/AutoLoginServlet?role=customer&langpref=en&newFlow=true&userLoginId=tokenid"
                //return  "http://cp.theadm.in/servlet/AutoLoginServlet?role=customer&langpref=en&newFlow=true&userLoginId=" + token;
                return adminAreaUrl +  token;
            }
            catch (Exception ex)
            {
                LogException(ex,"");
                return adminAreaUrl;
            }

            //return "http://mahe329832.myorderbox.com/servlet/LoginServlet?role=customer&token=" + token.Replace("\"", "").Replace("\\", "");
        }

        public object GetAllOrders()
        {
            //  https://test.httpapi.com/api/webservices/search.json?auth-userid=0&auth-password=password&no-of-records=1&page-no=1 
            string webserviceSearchUrl = "&no-of-records=100&page-no=1";
            return ParseJsonResponse(PostUrl(GetInitalUrl("webservices/search.json") + webserviceSearchUrl));
        }

        public static string GetDefaultAminUrl()
        {
            return adminAreaUrl;
        }

        #endregion

        #region PrivateMethod
        private void ParseCustomerDetailResponse(Dictionary<string, object> response)
        {
            cusInfo.AddressLine1 = response["address1"].ToString();
            cusInfo.City = response["city"].ToString();
            cusInfo.Company = response["company"].ToString();
            cusInfo.Country = response["country"].ToString();
            cusInfo.CustomerID = response["customerid"].ToString();
            cusInfo.Email = response["useremail"].ToString();
            cusInfo.Language = response["langpref"].ToString();
            cusInfo.Name = response["name"].ToString();
            //   cusInfo.Password = response["password"].ToString();
            cusInfo.PhoneCountryCode = response["telnocc"].ToString();
            cusInfo.PhoneNumber = response["telno"].ToString();
            cusInfo.State = response["state"].ToString();
            cusInfo.ZipCode = response["zip"].ToString();

        }

        private void ParseCustomerContactInfoResponse(Dictionary<string, object> response)
        {
            Dictionary<string, object> dic = response["Contact"] as Dictionary<string, object>;
            cusInfo.RegContactID = dic["registrant"].ToString();
            cusInfo.TechContactID = dic["tech"].ToString();
            cusInfo.BillingContactID = dic["billing"].ToString();
            cusInfo.AdminContactID = dic["admin"].ToString();
            //TODO : have to change
            cusInfo.CedContactID = dic["admin"].ToString();

        }

        private int ParseRegisterCustomerResponse(string response)
        {
            return int.Parse(response.Substring(5, response.Length - 5));
        }

        #endregion

        public bool GenerateTempPasswordSendEmail(string userEmail, out string message)
        {
            string tempPassword = "";
            if (!GenerateTempPassword(userEmail, out message, out tempPassword))
            {
                return false;
            }
            Email.SendMail(userEmail, "Your new temp password", GenerateEmailBodyTempPassword(tempPassword), "", "jai-hind@infowebservices.in", "Infowebservices Support");
            message = "Please check your email for new passord";
            return true;
        }


        public bool GenerateTempPassword(string userEmail, out string message, out string tempPassword)
        {
            bool rtn = true;
            string para = "&customer-id=";
            message = "";
            tempPassword = "";
            if (GetCustomerDetailByUserName(userEmail))
            {
                para += CusInfo.CustomerID;
                string response = PostUrl(GetInitalUrl("customers/temp-password.xml") + para);
                //"<string>wREJBM4p</string>"
                tempPassword = response.Substring(response.IndexOf('>') + 1, response.IndexOf("</") - (response.IndexOf('>') + 1));
                return true;
            }
            else
            {
                message = "Unable to locate a user account with \"" + userEmail + "\" email address";
                return false;
            }

          
        }



        public bool ChangePassword(string userEmail, string oldPassword, string newPassword, out string message)
        {
            bool rtn = true;
            message = "Password change successfully";

            try
            {
                if (!GetCustomerDetailByUserName(userEmail))
                {
                    message = "Unable to locate a user account with \"" + userEmail + "\" email address";
                    return false;
                }

                if (!ValidateCustomer(userEmail, oldPassword))
                {

                    message = "Invalid old password";
                    return false;
                }


                string para = "&customer-id=" + CusInfo.CustomerID;
                para += "&new-passwd=" + newPassword;


                PostUrl(GetInitalUrl("customers/change-password.json") + para);
                message = "Password chaged successfully";
            }
            catch (ServerException ex)
            {
                LogException(ex, "");
                message = "Unable to change password do to communication error";
                rtn = false;
            }


            return rtn;

            //https://test.httpapi.com/api/customers/change-password.json?auth-userid=0&auth-password=password&customer-id=0&new-passwd=password1 
        }

        private string GenerateEmailBodyTempPassword(string tempPassword)
        {
            return tempPassword;
        }
        //https://test.httpapi.com/api/customers/change-password.json?auth-userid=0&auth-password=password&customer-id=0&new-passwd=password1 
    }
}
