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
using ResellerClub.Common.Logging;

namespace ResellerClub.WebUI
{
    public partial class Home : BasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideParentPageHeading();
            DomainSearchControl1.ProcessDomainSerachResult += new Action(ProcessDomainSerachResult);
            CurrentModule = ModuleNames.Home;
        }

        private void ProcessDomainSerachResult()
        {
           Response.Redirect("Domain.aspx?DoSearch=1");
        }
    }
}
