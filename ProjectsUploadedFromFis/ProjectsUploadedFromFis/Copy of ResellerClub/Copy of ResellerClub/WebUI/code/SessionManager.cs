using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;


using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.SessionState;

namespace ResellerClub.WebUI
{
    public sealed class SessionManager
    {

        public object this[string key]
        {
            get
            {
                try
                {

                    return HttpContext.Current.Session[key];
                }
                catch
                {
                    return null;
                }

            }

            set
            {
                if(HttpContext.Current.Session != null)
                HttpContext.Current.Session[key] = value;
            }
        }

        public bool SessionExist()
        {
            return HttpContext.Current.Session != null;
        }

        public string AspSessionId()
        {
            return HttpContext.Current.Session.SessionID; 
        }

        public Nullable<Guid> SessionId()
        {
           object id = this["SessionFID"];
           Nullable<Guid> sid = null; ;
            if (id != null)
                sid = id as Nullable<Guid>;
            return sid;

        }

        public void Abandon()
        {
            HttpContext.Current.Session.Abandon();
        }

        public string  ToJasonString()
        {
            string rtn = "";
            if (HttpContext.Current.Session != null)
            {
                Dictionary<string, object> _session = new Dictionary<string, object>();
                foreach (string key in HttpContext.Current.Session)
                {
                    _session[key] = HttpContext.Current.Session[key];
                }
                rtn = Util.ObjectToJason(_session);
            }
            return rtn;
        }
    }
}
