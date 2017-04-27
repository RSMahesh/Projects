using System;
using System.Collections;
using System.Collections.Generic;
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
using ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class WebsiteBuilder : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();
            HideParentPageHeading();
            PopulatePlanPanel(PlanPanel1, Constant.WebSiteBuilder, Plan.GetProductPalns(Constant.WebSiteBuilder), divPlanFeature1, divPlanFeature2, divPlanFeature3, divPlanFeature4);
        }

    }
}
