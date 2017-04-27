using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ResellerClub.WebUI
{
    public partial class DisplayInformation : BasePage 
    {
        public string HTMLContent = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            TurnOffCache();
            if (ValidateRequest())
            {
                string filePath = Request["Htmlfile"];
                HTMLContent = Util.GetHTMLFileContent(filePath);
            }
        }
        private bool  ValidateRequest()
        {
            if (UserCart == null || UserCart.Items == null || UserCart.Items.Count < 1)
            {
                divContinue.Visible = false;
                DisplayError("There are no items in your cart for processing");
                return false;
            }
            return true;
        }
    }
}
