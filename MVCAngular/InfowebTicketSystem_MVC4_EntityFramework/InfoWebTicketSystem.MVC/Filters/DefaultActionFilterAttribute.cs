using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using InfoWebTicketSystem.MVC.ViewModels;
using System.Web.Routing;
using System.Diagnostics;

namespace InfoWebTicketSystem.MVC.Filters
{
    public class DefaultActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.UserValidated = true;
            if (filterContext.RequestContext.HttpContext.Session["userValidated"] == null || (bool)filterContext.RequestContext.HttpContext.Session["userValidated"] == false)
            {
                filterContext.Controller.ViewBag.UserValidated = false;
            }
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    Log("OnActionExecuted", filterContext.RouteData);
        //}

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    Log("OnResultExecuting", filterContext.RouteData);
        //}

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    Log("OnResultExecuted", filterContext.RouteData);
        //}


        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Debug.WriteLine(message, "Action Filter Log");
        }

    }
}