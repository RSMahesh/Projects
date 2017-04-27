using InfoWebTicketSystem.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InfoWebTicketSystem.MVC.Controllers
{
    public class IndexController : BaseController
    {
        //
        // GET: /Index/
        // [DefaultActionFilter]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {

            if (isSPAResquest)
                return View(@"~/Views/SPA/Index/Blank.cshtml");
            else
                return View();

        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult IndexPartail()
        {
            // return View();

            if (isSPAResquest)
                return PartialView(@"~/Views/SPA/Index/Index.cshtml");
            else
                return View();
        }


        public ActionResult Logout(string Login)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginPanel()
        {
            return PartialView(@"~/Views/SPA/Index/_Login.cshtml");
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOutPanel()
        {
            return PartialView(@"~/Views/SPA/Index/LeftPanel.cshtml");
        }
    }
}
