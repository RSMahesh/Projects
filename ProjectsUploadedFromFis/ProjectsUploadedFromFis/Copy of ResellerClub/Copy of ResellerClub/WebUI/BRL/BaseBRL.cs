using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Collections.Generic;
using System.IO;

namespace WebApplication1.BRL
{
    public class BaseBRL
    {
        string authUserid;
        string authPassword;
        protected string rootUrl = "https://test.httpapi.com/api/";

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

        public BaseBRL(string authUser,string authPassword)
        {
            this.authUserid = authUser;
            this.authPassword = authPassword;

        }

        protected string PostUrl(string url)
        {
            
            // Prepare web request...
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            // Send the data.
            WebResponse res = myRequest.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();
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
            return  rootUrl + url + "?auth-userid="+authUserid +"&auth-password=" + authPassword;
        }

    }
}
