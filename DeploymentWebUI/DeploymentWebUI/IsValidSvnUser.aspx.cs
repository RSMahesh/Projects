using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeploymentWebUI
{
    public partial class IsValidSvnUser : System.Web.UI.Page
    {
        static object objLock = new object();
        protected void Page_Load(object sender, EventArgs e)
        {
            LogUser();
        }

        private void LogUser()
        {
            if (Request.QueryString["user"] == null)
            {
                return;
            }

            var info = Environment.NewLine + "Date : "+ DateTime.Now.ToString() +" user:" + Request.QueryString["user"] + " ip:" + Request.QueryString["ip"];
            var file = Server.MapPath("SVN.svn");
            lock (objLock)
            {
                System.IO.File.AppendAllText(file, info);
            }
        }
    }
}