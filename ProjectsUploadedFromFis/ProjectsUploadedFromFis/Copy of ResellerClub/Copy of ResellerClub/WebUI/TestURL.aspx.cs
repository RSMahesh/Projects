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
using System.Xml.Linq;
using System.Net;
using System.Collections.Generic;
using System.IO;

namespace ResellerClub.WebUI
{
    public partial class TestURL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           ParseResponse( CheckURL(TextBox1.Text));

        }

        private object CheckURL(string url)
        {
            //  string url = "https://test.httpapi.com/api/domains/available.json?auth-userid=92820&auth-password=ybailwal123&domain-name=banumati&domain-name=domain2&tlds=com&tlds=net";
            // Prepare web request...
            HttpWebRequest myRequest =
              (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            // Send the data.
            WebResponse res = myRequest.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();

            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                             new System.Web.Script.Serialization.JavaScriptSerializer();
            object obj = serializer.Deserialize<object>(str);
            return obj;

        }

        private void ParseResponse(object obj)
        {
            Dictionary<string, object> dic = obj as Dictionary<string, object>;
           
            foreach (string key in dic.Keys)
            {
                Dictionary<string, object> innerDic = (Dictionary<string, object>)dic[key];
              
            }

           
        }

    }
}
