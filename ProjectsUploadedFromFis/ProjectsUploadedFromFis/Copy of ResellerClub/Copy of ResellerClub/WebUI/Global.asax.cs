using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Diagnostics;
using ResellerClub.Common.Logging;
using ResellerClub.Common;
using System.Web.Routing;
using System.IO;


namespace ResellerClub.WebUI
{
    public class Global : System.Web.HttpApplication
    {


        protected void Application_Start(object sender, EventArgs e)
        {
            // set log path location
            string logPath;
            if (ConfigurationManager.AppSettings["logPath"] != null)

                logPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["logPath"]);

            else
                logPath = HttpContext.Current.Server.MapPath(".");

            Trace.Listeners.Clear();
            Trace.Listeners.Add(new TextTraceListener(logPath));

            UnityContainer unityContainer = new UnityContainer();
            object[] paras = new object[2];
            paras[0] = ConfigurationManager.AppSettings["ResellerUserName"]; ;
            paras[1] = ConfigurationManager.AppSettings["ResellerPassowrd"];

            unityContainer.RegisterType<ResellerClub.Interface.ISSL, ResellerClub.BusinessLogic.SSL>().Configure<InjectedMembers>().ConfigureInjectionFor<ResellerClub.BusinessLogic.SSL>(new InjectionConstructor(paras));
            unityContainer.RegisterType<ResellerClub.Interface.IWebServices, ResellerClub.BusinessLogic.WebServices>().Configure<InjectedMembers>().ConfigureInjectionFor<ResellerClub.BusinessLogic.WebServices>(new InjectionConstructor(paras));
            unityContainer.RegisterType<ResellerClub.Interface.IOrder, ResellerClub.BusinessLogic.Order>().Configure<InjectedMembers>().ConfigureInjectionFor<ResellerClub.BusinessLogic.Order>(new InjectionConstructor(paras));
            unityContainer.RegisterType<ResellerClub.Interface.ISessionLogger, ResellerClub.BusinessLogic.SessionLogger>();
            unityContainer.RegisterType<ResellerClub.Interface.IPlan, ResellerClub.BusinessLogic.Plan>();
            unityContainer.RegisterType<ResellerClub.Interface.IPaymentProcessor, ResellerClub.BusinessLogic.PayPalTranscationLogger>();
            unityContainer.RegisterType<ResellerClub.Interface.IState, ResellerClub.BusinessLogic.State>();
            unityContainer.RegisterType<ResellerClub.Interface.IExceptionLogger, ResellerClub.BusinessLogic.ExceptionLogger>();
            unityContainer.RegisterType<ResellerClub.Interface.IHosting, ResellerClub.BusinessLogic.Hosting>();

            if (ConfigurationManager.AppSettings["OffLine"] != null && bool.Parse(ConfigurationManager.AppSettings["OffLine"]))
            {
                unityContainer.RegisterType<ResellerClub.Interface.ICustomer, Test.Customer>();
                unityContainer.RegisterType<ResellerClub.Interface.IDomain, Test.Domain>();
            }
            else
            {
                unityContainer.RegisterType<ResellerClub.Interface.ICustomer, ResellerClub.BusinessLogic.Customer>().Configure<InjectedMembers>().ConfigureInjectionFor<ResellerClub.BusinessLogic.Customer>(new InjectionConstructor(paras));
                unityContainer.RegisterType<ResellerClub.Interface.IDomain, ResellerClub.BusinessLogic.Domain>().Configure<InjectedMembers>().ConfigureInjectionFor<ResellerClub.BusinessLogic.Domain>(new InjectionConstructor(paras));

            }

            Application["unityContainer"] = unityContainer;
            Application["rootPath"] = ConfigurationManager.AppSettings["rootPath"];
            //Application["rootPath"] = HttpContext.Current.Request.file;
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
            SessionManager sessionM = new SessionManager();
            string addtonalInfo = "";

            try
            {
                addtonalInfo += Environment.NewLine + " SessionValues :" + sessionM.ToJasonString();
            }
            catch { }
            try
            {
                var requestInputStream = new StreamReader(HttpContext.Current.Request.InputStream);
                var requestContent = requestInputStream.ReadToEnd();
                requestInputStream.Close();
                addtonalInfo += Environment.NewLine + " Request :" + requestContent;
            }
            catch { }

            try
            {
                Nullable<Guid> sessionId = null;
                lastServerException = Server.GetLastError();

                if (sessionM["SessionFID"] != null)
                {
                    sessionId = (Guid)sessionM["SessionFID"];
                }
                var logger = ApiObjectFactory.GetObject<ResellerClub.Interface.IExceptionLogger>();

                logger.LogException(lastServerException, sessionId, Helper.GetIPAddress(), url, addtonalInfo , lastServerException.GetHashCode(), 0);
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
                    Server.Execute("/Error.aspx");
                }
                catch (Exception ex)
                {
                    exceptionInErrorPage = true;
                }

                if (exceptionInErrorPage)
                {
                    Response.Redirect("/Error.htm");
                }
                else
                {
                    Response.Redirect("/Error.aspx");
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