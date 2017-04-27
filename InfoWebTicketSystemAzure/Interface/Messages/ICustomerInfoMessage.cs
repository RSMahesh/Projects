using System;
using System.Collections.Generic;

using System.Text;

namespace ResellerClub.Interface.Messages
{
   public  interface  ICustomerInfoMessage
    {
        string AddressLine1 { get; set; }
        string AdminContactID { get; set; }
        string BillingContactID { get; set; }
        string CedContactID { get; set; }
        string City { get; set; }
        string Company { get; set; }
        string Country { get; set; }
        string CustomerID { get; set; }
        string Email { get; set; }
        string Language { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        string PhoneCountryCode { get; set; }
        string PhoneNumber { get; set; }
        string RegContactID { get; set; }
       
        string State { get; set; }
        string TechContactID { get; set; }
      
        string ZipCode { get; set; }
    }
}
