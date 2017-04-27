using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using APIC = ResellerClub.Common;
using ResellerClub.Common;
using ResellerClub.WebUI.UserControl;
using ResellerClub.Interface.Messages;

namespace ResellerClub.WebUI
{
    public partial class LinuxHostingPlan : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();
            PopulatePlanPanel(PlanPanel1,Constant.SingleDomainHostingLinuxUs, Plan.LinuxHostingPlan,divPlanFeature1, divPlanFeature2, divPlanFeature3, divPlanFeature4);
        }
    }
}
