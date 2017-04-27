using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MediaProcessor.ServiceLibrary.Common;

namespace DeploymentWebUI
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          // var output12 = ExeRunner.Execute(@"notepad.exe","");
          var tt =  HttpHelper.CleanDeploymentBranch();
            //var gitPth = @"C:\Program Files (x86)\Git\bin\git.exe";
            //var dir = @"D:\GitRepo\Deployment\MediaValetAPI";
            //Directory.SetCurrentDirectory(dir);
            //var output = ExeRunner.Execute(gitPth, "clean -fdx");
            //output = ExeRunner.Execute(gitPth, "pull upstream stage");

        }
    }
}