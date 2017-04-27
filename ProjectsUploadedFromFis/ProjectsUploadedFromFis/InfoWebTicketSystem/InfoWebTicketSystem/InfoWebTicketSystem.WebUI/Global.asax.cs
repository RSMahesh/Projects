using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using ResellerClub.Common;
using ResellerClub.Common.Logging;

namespace InfoWebTicketSystem.WebUI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastServerException = null;
            var url = HttpContext.Current.Request.Url.AbsoluteUri;
           

            try
            {
                Nullable<Guid> sessionId = null;
                lastServerException = Server.GetLastError();

            //    var logger = new  ResellerClub.BusinessLogic.ExceptionLogger();

             //   logger.LogException(lastServerException, sessionId, Helper.GetIPAddress(), url, "", lastServerException.GetHashCode(), 0);
            }
            catch (Exception ex)
            {

                if (lastServerException != null)
                {
                    XmlTraceWriter.TraceInfo("=========================================================");
                    XmlTraceWriter.Trace(lastServerException);
                    XmlTraceWriter.Trace(ex);
                    XmlTraceWriter.TraceInfo("=========================================================");
                }
                else
                {
                    XmlTraceWriter.Trace(ex);
                }
            }
            finally
            {

                var exceptionInErrorPage = false;
                try
                {
                    Server.Execute("Error.aspx");
                }
                catch (Exception ex)
                {
                    exceptionInErrorPage = true;
                }

                if (exceptionInErrorPage)
                {
                    Response.Redirect("Error.htm");
                }
                else
                {
                    Response.Redirect("Error.aspx");
                }
            }

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}