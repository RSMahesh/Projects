using System;
using System.Collections;
using System.Data;

using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.SessionState;
using APIC = ResellerClub.Common;
using ResellerClub.Common;


namespace ResellerClub.WebUI
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AjaxHandler : IHttpHandler, IRequiresSessionState
    {
        HttpContext _context = null;
        protected SessionManager SessionM = new SessionManager();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();

            _context = context;
            if (context.Request["RemoveItem"] != null)
            {
                RemoveItem(context.Request["RemoveItem"]);
                System.Threading.Thread.Sleep(1000);
                context.Response.Write("Done");
            }
            if (context.Request["GetCountryState"] != null)
            {
                GetCountryState(context.Request["GetCountryState"]);
            }
        }

        private void RemoveItem(string removeItem)
        {
            Cart cart = (Cart)SessionM["Cart"];
            string[] arr = removeItem.Split( new string[]{Constant.splitChar}, StringSplitOptions.None);

            var itemToRemove = cart.Items.Find(x => x.DomainName == arr[1] && x.SubPlanID.ToString() == arr[0]);
            cart.Items.Remove(itemToRemove);
            SessionM["Cart"] = cart;
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
