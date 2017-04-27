using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Web;

using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;

namespace DeploymentWebUI
{
    public class HttpHelper
    {
        static string gitToken = "token e7aba87c95671df23f6c5b3dde789efe16d85a49";
        static string teamCityToken = "Basic cnNtYWhlc2g6MTIzNHRlc3Qh";

        public static string PostUrl(string url, string method, string authorization, string json = "")
        {
            string str = "";
            try
            {

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = method;
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                myRequest.Headers["Authorization"] = authorization;

                if (json != string.Empty)
                {
                    using (var streamWriter = new StreamWriter(myRequest.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                // Send the data.
                WebResponse res = myRequest.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream());
                str = sr.ReadToEnd();
                sr.Close();


            }
            catch (WebException ex)
            {
                int indx;
                string message = "";
                try
                {
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.InternalServerError)
                    {
                        using (StreamReader sr = new StreamReader(ex.Response.GetResponseStream()))
                        {
                            message = sr.ReadToEnd();
                            //  lastServerException = message;
                            indx = message.IndexOf("<message>");
                            if (indx > -1)
                            {
                                indx = indx + "<message>".Length;
                                message = message.Substring(indx, message.IndexOf("</message>") - indx);
                            }
                            else
                            {
                                // message = ParseJsonResponse(message)["message"].ToString();

                            }
                        }
                    }
                }
                catch { }

                throw new ServerException(message, ex);
            }
            return str;
        }

        public static IEnumerable<object> GetPulls()
        {
            var res = HttpHelper.PostUrl("https://api.github.com/repos/RsMahesh/MediaValetAPI/pulls", "GET", gitToken);
            return (object[])ParseJsonResponse(res);
        }

        public static void MergePull(IEnumerable<object> pulls)
        {
            if (!pulls.Any())
            {
                throw new DeploymentException("No Pulls found against RsMahesh/MediaValetAPI");
            }

            if (pulls.Count() > 1)
            {
                throw new DeploymentException("More then one Pull found against RsMahesh/MediaValetAPI");
            }

            var dic = pulls.FirstOrDefault() as Dictionary<string, object>;
            string mergeUrl = GetMergeUrl(dic);
            string commitSha = GetCommitSHA(dic);
            var json = "{\"sha\":\"" + commitSha + "\",\"commit_message\": \"Merging for deployment using rest api\"}";
            var response = ParseJsonResponse(HttpHelper.PostUrl(mergeUrl, "PUT", gitToken, json)) as Dictionary<string, object>;

            if (!response["merged"].ToString().Equals("True", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new DeploymentException("Merged Failed");
            }

        }

        private static object ParseJsonResponse(string response)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                     new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Deserialize<object>(response);
        }


        public static string GetCommitSHA(Dictionary<string, object> dic)
        {
            var headDic = (Dictionary<string, object>)dic["head"];
            return headDic["sha"].ToString();
        }

        public static string GetPullLabel(Dictionary<string, object> dic)
        {
            var headDic = (Dictionary<string, object>)dic["head"];
            return headDic["label"].ToString();
        }


        public static string GetMergeUrl(Dictionary<string, object> dic)
        {
            return dic["url"].ToString() + "/merge";
        }

        public static void TriggerTeamCityBuild()
        {
            var res = HttpHelper.PostUrl("http://localhost:81/httpAuth/action.html?add2Queue=Api_OnEachCommit", "GET", teamCityToken);
        }

        public static string CleanDeploymentBranch()
        {
            var res = HttpHelper.PostUrl(@"http://localhost:9180/abc.aspx?exe=D:\GitRepo\Deployment\GitGetLatestFromStage.bat", "GET", string.Empty);
            return res;
        }

        public static string UpdateEnterpriseDllReference()
        {
            var res = HttpHelper.PostUrl(@"http://localhost:9180/abc.aspx?exe=D:\GitRepo\Deployment\GitUpdateReference.bat", "GET", string.Empty);
            return res;
        }
    }
}