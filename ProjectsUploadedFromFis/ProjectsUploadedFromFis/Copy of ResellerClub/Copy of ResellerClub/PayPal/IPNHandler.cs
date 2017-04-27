using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Configuration;

namespace PayPal
{
    public class IPNHandler : IHttpHandler
    {
    
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            ProcessIPN(context.Request, ConfigurationManager.AppSettings["PayPalUrl"]);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected void ProcessIPN(HttpRequest Request, string payPalUrl)
        {
            //    Request.SaveAs(Server.MapPath("notify_url_" + Guid.NewGuid().ToString()), true);
            payPalUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            // string strLive = "https://www.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(payPalUrl);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            strRequest += "&cmd=_notify-validate";
            strRequest += "&fraud=123&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://url:port#"));
            //req.Proxy = proxy;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            if (strResponse == "VERIFIED")
            {
                //check the payment_status is Completed
                //check that txn_id has not been previously processed
                //check that receiver_email is your Primary PayPal email
                //check that payment_amount/payment_currency are correct
                //process payment
            }
            else if (strResponse == "INVALID")
            {
                //log for manual investigation
            }
            else
            {
                //log response/ipn data for manual investigation
            }

        }

    }
}
