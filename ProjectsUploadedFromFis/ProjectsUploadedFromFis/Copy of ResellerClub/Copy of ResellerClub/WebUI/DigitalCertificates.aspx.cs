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
    public partial class DigitalCertificates : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();

            PlanPanel1.PlanHeading[0] = "SSL123 Certificate";
            PlanPanel1.PlanHeading[1] = "Web Server Certificate";
            PlanPanel1.PlanHeading[2] = "SGC SuperCert";
            PlanPanel1.PlanHeading[3] = "Wildcard Server Certificate";


            PlanPanel1.Product[0] = Constant.SSL_SSL123;
            PlanPanel1.Product[1] = Constant.SSL_FSSL;
            PlanPanel1.Product[2] = Constant.SSL_SGC;
            PlanPanel1.Product[3] = Constant.SSL_WILD;

            PlanPanel1.PlanSequence[0] = 1;
            PlanPanel1.PlanSequence[1] = 1;
            PlanPanel1.PlanSequence[2] = 1;
            PlanPanel1.PlanSequence[3] = 1;

            PlanPanel1.PlanFeature[0] = divPlanFeature1.InnerHtml;
            PlanPanel1.PlanFeature[1] = divPlanFeature2.InnerHtml;
            PlanPanel1.PlanFeature[2] = divPlanFeature3.InnerHtml;
            PlanPanel1.PlanFeature[3] = divPlanFeature4.InnerHtml;
        }

      
    }
}
