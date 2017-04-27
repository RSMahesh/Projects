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
using System.IO;

namespace ResellerClub.WebUI
{
    public partial class RefreshPlanCache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRefersh_Click(object sender, EventArgs e)
        {
            Plan.Load();
            ResellerClub.WebUI.code.LanguageSetting.LoadLanguageSetting("ENGLISH");
            ResellerClub.WebUI.code.LanguageSetting.LoadLanguageSetting("HINDI");
            var htmlFolderPath = Server.MapPath(@"HTML");
            var files = Directory.GetFiles(htmlFolderPath);

            foreach (var file in files)
            {
                var f = Path.GetFileName(file);
                if (Cache[f] != null)
                    Cache[f] = "";
            }
        }
    }
}
