using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.SessionState;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AjaxHandler : IHttpHandler, IRequiresSessionState
    {
        HttpContext _context = null;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();

            _context = context;
            if (context.Request["RemoveItem"] != null)
            {
                RemoveItem(context.Request["RemoveItem"]);
                System.Threading.Thread.Sleep(2000);
                context.Response.Write("Done");
            }

        }

        private void RemoveItem(string item)
        {
            Dictionary<string, string> domainCart = _context.Session["DomainCart"] as Dictionary<string, string>;
            domainCart.Remove(item);
            _context.Session["DomainCart"] = domainCart;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

      
    }
}
