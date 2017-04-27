using InfoWebTicketSystem.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfoWebTicketSystem.MVC.Controllers
{
    public class ViewModalFactoryController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetLoginViewModel()
        {
            var model = new LoginViewModel();
            model.UserName = "maheshbailwal@gmail.com";
            model.Password = "MB248001";
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetSelectDepartmentViewModel()
        {
            var model = new SelectDepartmentViewModel();
            model.Department = "SALE";
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCollectTicketDetailViewModal()
        {
            var model = new CollectTicketDetailViewModal();
            model.Domain = "1111";
            model.Message = "1111";
            model.Subject = "1111";
            model.TicketType = "NI";
            model.UserEmail = "abcd";
            return Json(model, JsonRequestBehavior.AllowGet);
        }


    }
}
