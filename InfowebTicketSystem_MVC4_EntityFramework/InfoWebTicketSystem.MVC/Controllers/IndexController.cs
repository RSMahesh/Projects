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
            return View();
        }

        public ActionResult Logout(string Login)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Index");
        }

    }
}
