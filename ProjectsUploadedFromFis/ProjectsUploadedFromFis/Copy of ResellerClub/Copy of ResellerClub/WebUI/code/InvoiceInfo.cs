using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ResellerClub.WebUI
{
    public sealed  class InvoiceInfo
    {
        public string Domain;
        public string Description;
        public string InvioceNumber;
        public int Amount;

    }
}
