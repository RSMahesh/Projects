using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Net;
using ResellerClub.Interface;
using ResellerClub.Interface.Messages;
using ResellerClub.Messages;


namespace Test
{
    public class Customer : ICustomer
    {

        public Customer()
        {

        }


        #region ICustomer Members

        public ICustomerInfoMessage CusInfo
        {
            get
            {
                ICustomerInfoMessage cusInfo = new CustomerInfoMessage();
                cusInfo.AddressLine1 = "testName1testName2";
                cusInfo.City = "testName";
                cusInfo.Company = "testName1";
                cusInfo.Country = "AX";
                cusInfo.CustomerID = "7050773";
                cusInfo.Email = "maheshbailwal2@gmail.com";
                cusInfo.Name = "mahesh";
                cusInfo.Password = "MB248001";
                cusInfo.RegContactID = "23295500";
                cusInfo.TechContactID = "23295500";
                cusInfo.BillingContactID = "23295500";
                cusInfo.AdminContactID = "23295500";
                cusInfo.CedContactID = "23295500";

                return  cusInfo;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string GetAdminUrlWithToken()
        {
            throw new NotImplementedException();
        }

        public object GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public void GetCustomerContactInfo(string customerID)
        {
           
        }

        public bool GetCustomerDetailByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void Register()
        {
            throw new NotImplementedException();
        }

        public bool ValidateCustomer(string userEmail, string password)
        {
            return true;
        }

        #endregion

        #region IBaseInterface Members

        public string UserIP
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IBaseInterface Members


        public Guid? SessionID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IBaseInterface Members


        public string UserURL
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICustomer Members


        public bool GenerateTempPassword(string userEmail, out string message)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICustomer Members


        public bool GenerateTempPasswordSendEmail(string userEmail, out string message)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICustomer Members


        public bool ChangePassword(string userEmail, string oldPassword, string newPassword, out string message)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICustomer Members


        public bool GenerateTempPassword(string userEmail, out string message, out string tempPassword)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
