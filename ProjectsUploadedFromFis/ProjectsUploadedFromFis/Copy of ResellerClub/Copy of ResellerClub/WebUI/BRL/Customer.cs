using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WebApplication1.BRL
{
    public class Customer : BaseBRL
    {
       
        public string CustomerID;
        string RegContactID;
        string AdminContactID;
        string TechContactID;
        string BillingContactID;
        string CedContactID;
       
        public Customer(string authUser, string authPassword)
            : base(authUser, authPassword)
        {
           


        }

    }
}
