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
using ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class SelectWebService : BasePage 
    {
        protected string startingPriceWebSite;
        protected string startingPriceLinuxHosting;
        protected string startingPriceEmailHosting;
        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();
            startingPriceLinuxHosting = CurrencySymbol + " " + Util.FormatTo2DecimalPlace( Plan.GetPlanStartingPrice(Constant.SingleDomainHostingLinuxUs));
            startingPriceWebSite = CurrencySymbol + " " + Util.FormatTo2DecimalPlace( Plan.GetPlanStartingPrice(Constant.WebSiteBuilder));
            startingPriceEmailHosting = CurrencySymbol + " " + Util.FormatTo2DecimalPlace(Plan.GetPlanStartingPrice(Constant.EmailHosting));
        }

        protected void btnWebSite_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebSite/WebsiteBuilder.aspx");

        }

        protected void btnHosting_Click(object sender, EventArgs e)
        {
            Response.Redirect("Hosting/LinuxHostingPlan.aspx");
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            Response.Redirect("Hosting/EmailHosting.aspx");
        }
      
    }
}
