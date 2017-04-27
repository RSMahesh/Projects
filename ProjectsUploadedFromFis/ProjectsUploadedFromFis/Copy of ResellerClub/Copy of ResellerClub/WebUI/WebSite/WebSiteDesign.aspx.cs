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
using APIC = ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class WebSiteDesign : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();
            if (IsPostBack)
            {
                if (Request["btnEcom"] != null)
                {
                    AddEcomPlan();
                }

                if (Request["btnStandard"] != null)
                {
                    AddStandardPaln();
                }
            }
        }

        private void AddEcomPlan()
        {

            UserCart.SetSelectedWebservice(Plan.EcomWebSite[0].SubPlanID, false, true);
            Response.Redirect("SelectHostingDomain.aspx");
        }
        private void AddStandardPaln()
        {
            UserCart.SetSelectedWebservice(Plan.StandardWebSite[0].SubPlanID, false, true);
            Response.Redirect("SelectHostingDomain.aspx");

        }

        protected void btnPwebsite_Click(object sender, EventArgs e)
        {
            AddStandardPaln();
        }

        protected void btnEwebsite_Click(object sender, EventArgs e)
        {
            AddEcomPlan();
        }
    }
}
