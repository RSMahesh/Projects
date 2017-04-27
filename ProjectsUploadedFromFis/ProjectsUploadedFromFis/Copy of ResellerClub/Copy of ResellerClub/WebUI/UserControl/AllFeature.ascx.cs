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

namespace ResellerClub.WebUI.UserControl
{
    public partial class AllFeature : BaseUserControl
    {
        protected string fileName;

        protected void Page_Load(object sender, EventArgs e)
        {


        }


        public string GetAllFeature()
        {
            string fileName = Path.GetFileNameWithoutExtension(Request.Url.ToString()) + "_Feature.htm";

            if (Cache[fileName] == null || Cache[fileName] == "")
            {
                Cache[fileName] = Util.GetHTMLFileContent(fileName);
            }

            return Cache[fileName].ToString();
        }

        public string GetFaq()
        {
            string fileName = Path.GetFileNameWithoutExtension(Request.Url.ToString()) + "_Faq.htm";

            if (Cache[fileName] == null || Cache[fileName] == "")
            {
                Cache[fileName] = Util.GetHTMLFileContent(fileName);
            }

            return Cache[fileName].ToString();
        }
    }
}