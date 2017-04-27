using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Xml.Linq;
using System.Net;
using System.Collections.Generic;
using System.IO;
using ResellerClub.Interface.Messages;
using ResellerClub.Common;
using ResellerClub.Common.Logging;
using ResellerClub.Interface;

namespace ResellerClub.BusinessLogic
{
    public abstract class BaseBRL : IBaseInterface
    {
        string authUserid;
        string authPassword;
        protected string rootUrl = "https://test.httpapi.com/api/";
        protected string lastServerException;
        private string httpApiRequesturl = "";
        private string userIP;
        // protected bool offLine = true;

        #region Properties
        public string AuthUserid
        {
            get { return authUserid; }
            set { authUserid = value; }
        }

        public string AuthPassword
        {
            get { return authPassword; }
            set { authPassword = value; }
        }
        #endregion

        static BaseBRL()
        {
            if (Cache.Get("SubPlanProductMapping") == null)
            {
                LoadCache();
            }
        }

        public BaseBRL()
        {
            //this.authUserid = ConfigurationManager.AppSettings["ResellerUserName"]; ;
            //this.authPassword = ConfigurationManager.AppSettings["ResellerPassowrd"];
        }

        public BaseBRL(string authUser, string authPassword)
        {
            this.authUserid = authUser;
            this.authPassword = authPassword;

        }

        protected string PostUrl(string url)
        {
            string str = "";
            httpApiRequesturl = url;
            try
            {

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
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
                            lastServerException = message;
                            indx = message.IndexOf("<message>");
                            if (indx > -1)
                            {
                                indx = indx + "<message>".Length;
                                message = message.Substring(indx, message.IndexOf("</message>") - indx);
                            }
                            else
                            {
                                message = ParseJsonResponse(message)["message"].ToString();

                            }
                        }
                    }
                }
                catch { }

                throw new ServerException(message, ex);
            }
            return str;
        }

        protected Dictionary<string, object> ParseJsonResponse(string response)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                     new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> dic = (Dictionary<string, object>)serializer.Deserialize<object>(response);
            return dic;

        }

        protected string GetInitalUrl(string url)
        {
            return rootUrl + url + "?auth-userid=" + authUserid + "&auth-password=" + authPassword;
        }

        protected string GetInitalUrl(string url, string _authuserName, string _authPassword)
        {
            return rootUrl + url + "?auth-userid=" + _authuserName + "&auth-password=" + _authPassword;
        }

        public string GetConfigSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string GetProductName(Guid subPlanId)
        {
            var subPlanProductMapping = (Dictionary<Guid, IPlanMessage>)Cache.Get("SubPlanProductMapping");
            return subPlanProductMapping[subPlanId].ProductName;
        }

        public IPlanMessage GetPlan(Guid subPlanId)
        {
            var subPlanProductMapping = (Dictionary<Guid, IPlanMessage>)Cache.Get("SubPlanProductMapping");
            return subPlanProductMapping[subPlanId];
        }

        private static void LoadCache()
        {
            var subPlanProductMapping = new Dictionary<Guid, IPlanMessage>();
            Plan plan = new Plan();
            var planList = plan.GetPlans();
            foreach (var p in planList)
            {
                subPlanProductMapping[p.SubPlanID] = p;
            }

            Cache.Set("SubPlanProductMapping", subPlanProductMapping);
        }

        protected void LogException(Exception ex, string additionalInfo)
        {
            var logger = new ExceptionLogger();
            if (additionalInfo != null && additionalInfo.Trim() != "")
            {
                additionalInfo += Environment.NewLine + "URL:" + httpApiRequesturl;
            }
            else
            {
                additionalInfo = "URL:" + httpApiRequesturl;
            }
            logger.LogException(ex, SessionID, UserIP,UserURL, additionalInfo,ex.GetHashCode(),0);
        }

        protected string GetPlanId(string productName, string planName)
        {
            Dictionary<string, object> response = GetPlanDetail();
            Dictionary<string, object> product = (Dictionary<string, object>)response[productName];

            string serviceId = "";

            foreach (string key in product.Keys)
            {
                if (((Dictionary<string, object>)product[key])["plan_name"].ToString().Trim().ToUpperInvariant() == planName.Trim().ToUpperInvariant())
                {
                    serviceId = key;
                    break;
                }
            }

            return serviceId;
        }

        public Dictionary<string, object> GetPlanDetail()
        {
            string response = PostUrl(GetInitalUrl("products/plan-details.json"));
            return ParseJsonResponse(response);
        }


        #region IBaseInterface Members
        public string UserIP { get; set; }
        public Nullable<Guid> SessionID { get; set; }
        public string UserURL { get; set; }
        #endregion
    }

}
