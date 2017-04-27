using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MediaProcessor.ServiceLibrary.Common;

using Microsoft.Ajax.Utilities;

using Newtonsoft.Json.Linq;

namespace DeploymentWebUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var res = HttpHelper.PostUrl("https://api.github.com/repos/RsMahesh/MediaValetAPI/pulls", "GET");

            //var arr = (object[])this.ParseJsonResponse12(res);
            //var dic = (Dictionary<string, object>)arr[1];
            //var mergeUrl = HttpHelper.GetMergeUrl(dic);
            //var commitSha = HttpHelper.GetCommitSHA(dic);

            //var json = "{\"sha\":\"66d717221119a1ec7881b9ca27abb239f91b2de2\",\"commit_message\": \"REST Merge Try\"}";

            //var res1 = HttpHelper.PostUrl(mergeUrl, "PUT", json);

            var pulls = HttpHelper.GetPulls();

            if (!pulls.Any())
            {
                throw new Exception("No pull");
            }
            
            if (pulls.Count() > 1)
            {
                throw new Exception("More than one pull");
            }

            HttpHelper.MergePull(pulls);
           
        }

        public async void GetFoldersShouldReturnOkStatusCode()
        {
            var response = await HttpRequestEngine.ExecuteGetAsync("repos/RsMahesh/MediaValetAPI/pulls");
        }


        protected void BtnCleanBranch_Click(object sender, EventArgs e)
        {
            var output = ExeRunner.Execute(@"D:\GitRepo\Deployment\GitGetLatestFromStage.bat", "");
            //  txtCleanBranchOutPut.Text = output;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateReference_Click(object sender, EventArgs e)
        {
            var output = ExeRunner.Execute(@"D:\GitRepo\Deployment\GitUpdateReference.bat", "");
            txtUpdateRefence.Text = output;

        }

        protected Dictionary<string, object> ParseJsonResponse(string response)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                     new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> dic = (Dictionary<string, object>)serializer.Deserialize<object>(response);
            return dic;
        }

        protected object ParseJsonResponse12(string response)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                     new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Deserialize<object>(response);

        }



    }
}