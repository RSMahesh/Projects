using System;
using System.Collections;
using System.Data;

using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Configuration;


namespace ResellerClub.WebUI
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class MyAccount : IHttpHandler, IRequiresSessionState
    {
        HttpContext _context = null;
        protected SessionManager SessionM = new SessionManager(); 
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();
            _context = context;
              RedirectToAdminArea();
        }

        private void RedirectToAdminArea()
        {
           string authUser = ConfigurationManager.AppSettings["ResellerUserName"];
           string  authPassword = ConfigurationManager.AppSettings["ResellerPassowrd"];
           ResellerClub.BusinessLogic.Customer customer  = (ResellerClub.BusinessLogic.Customer)  SessionM["Customer"];
          if(customer == null)
              _context.Response.Redirect(ResellerClub.BusinessLogic.Customer.GetDefaultAminUrl());
            else 
            _context.Response.Redirect(customer.GetAdminUrlWithToken());

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
