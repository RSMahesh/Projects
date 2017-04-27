using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace HindiUnicode
{
    public partial class UniCodeConverter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
         txtUnicode.Text = UniCodeConverter.GetUniCodeString(txtKurti.Text);
         txtUnicode.Visible = true;
         span1.Visible = true;
         span2.Visible = true;

         if (Request.UserAgent.ToUpperInvariant().IndexOf("FIREFOX") == -1 && Request.UserAgent.ToUpperInvariant().IndexOf("SAFARI") == -1)
         btnCopy.Visible = true;
        }
    }
}
