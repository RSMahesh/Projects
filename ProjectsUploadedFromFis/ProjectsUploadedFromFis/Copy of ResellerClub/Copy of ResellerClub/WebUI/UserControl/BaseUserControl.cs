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
using ResellerClub.WebUI.code;

namespace ResellerClub.WebUI
{
    public class BaseUserControl : System.Web.UI.UserControl
    {
        private BasePage parentBasePage = null;

        protected BasePage ParentBasePage
        {
            get
            {
                if (parentBasePage == null)
                {
                    parentBasePage = (BasePage)this.Page;
                }

                return parentBasePage;
            }
        }

        protected SessionManager SessionM = new SessionManager();

        protected override void OnLoad(EventArgs e)
        {
            parentBasePage = (BasePage)this.Page;
            base.OnLoad(e);
        }

        protected void RedirectToRootPage(string pageName)
        {
            parentBasePage.RedirectToRootPage(pageName);
        }
        protected string __(string key)
        {
            return LanguageSetting.GetString(key);
        }

    }
}
