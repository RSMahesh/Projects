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
using ResellerClub.Common;

namespace ResellerClub.WebUI
{
    public partial class EmailHosting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();
            PopulatePlanPanel(PlanPanel1,Constant.EmailHosting, Plan.GetProductPalns(Constant.EmailHosting),divPlanFeature1, divPlanFeature2, divPlanFeature3, divPlanFeature4);
        }

    
    }
}
