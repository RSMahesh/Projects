using System;
using System.Collections.Generic;

using System.Text;
using ResellerClub.Interface.Messages; 

namespace ResellerClub.BusinessLogic.Messages
{
   public class CustomerInfoMessage : ICustomerInfoMessage 
    {
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string company;

        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        private string addressline1;

        public string AddressLine1
        {
            get { return addressline1; }
            set { addressline1 = value; }
        }
        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private string zipcode;

        public string ZipCode
        {
            get { return zipcode; }
            set { zipcode = value; }
        }
        private string phonecountrycode;

        public string PhoneCountryCode
        {
            get { return phonecountrycode; }
            set { phonecountrycode = value; }
        }
        private string phonenumber;

        public string PhoneNumber
        {
            get { return phonenumber; }
            set { phonenumber = value; }
        }
        private string language;

        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        private string customerid;

        public string CustomerID
        {
            get { return customerid; }
            set { customerid = value; }
        }
        private string regcontactid;

        public string RegContactID
        {
            get { return regcontactid; }
            set { regcontactid = value; }
        }
        private string admincontactid;

        public string AdminContactID
        {
            get { return admincontactid; }
            set { admincontactid = value; }
        }
        private string techcontactid;

        public string TechContactID
        {
            get { return techcontactid; }
            set { techcontactid = value; }
        }
        private string billingcontactid;

        public string BillingContactID
        {
            get { return billingcontactid; }
            set { billingcontactid = value; }
        }
        private string cedcontactid;

        public string CedContactID
        {
            get { return cedcontactid; }
            set { cedcontactid = value; }
        }
    }
}
