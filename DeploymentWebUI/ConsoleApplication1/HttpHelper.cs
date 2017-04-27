using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;

namespace ConsoleApplication1
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


    }
}