using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InfoWebTicketSystem.MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            AutoMapper.Map();

            ConfigureDependencyResolver(GlobalConfiguration.Configuration);
        }

        protected void ConfigureDependencyResolver(HttpConfiguration config)
        {

            Microsoft.Practices.Unity.IUnityContainer container = new Microsoft.Practices.Unity.UnityContainer();
            IDependencyResolver unityDR = new UnityDependencyResolver(container);
            UnityRegisterTypesConfig unityRes = new UnityRegisterTypesConfig(container);

            unityRes.Register();
            DependencyResolver.SetResolver(unityDR);
           //ControllerBuilder.Current.SetControllerFactory(new ActionControllerFactory());
            //  config.DependencyResolver = ;

        }
    }

    public class ActionControllerFactory : DefaultControllerFactory
    {
        //http://jeffreypalermo.com/blog/the-asp-net-mvc-actioncontroller-ndash-the-controllerless-action-or-actionless-controller/
        
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {

            string action = requestContext.RouteData.GetRequiredString("action");
            return base.GetControllerType(requestContext, action);

        }
    }
}