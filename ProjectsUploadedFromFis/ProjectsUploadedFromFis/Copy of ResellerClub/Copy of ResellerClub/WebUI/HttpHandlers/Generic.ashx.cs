using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.SessionState;

namespace ResellerClub.WebUI.HttpHandlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Generic : IHttpHandler, IRequiresSessionState
    {
        HttpContext _context = null;
        protected SessionManager SessionM = new SessionManager();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();

            _context = context;
            if (context.Request.QueryString["selectcountry"] != null)
            {
                SessionM["country"] = context.Request.QueryString["selectcountry"];
                SessionM["Cart"] = null;
                context.Response.Redirect("/Home.aspx");
            }
            if (context.Request["GetCountryState"] != null)
            {
            }

        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void GetCountryState(string countryCode)
        {
            var state = ApiObjectFactory.GetObject<ResellerClub.Interface.IState>();
            var stateList = state.GetCountryState(countryCode);
            var json = Util.ObjectToJason(stateList);
            _context.Response.Write(json);
        }
    }
}
