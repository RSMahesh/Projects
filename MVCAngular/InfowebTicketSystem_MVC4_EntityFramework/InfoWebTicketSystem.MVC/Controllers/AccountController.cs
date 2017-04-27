using InfoWebTicketSystem.MVC.Filters;
using InfoWebTicketSystem.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using Entities;
using InfoWebTicketSystem.BRL.Interface;
using Microsoft.Practices.Unity;
using System.Web.Security;



namespace InfoWebTicketSystem.MVC.Controllers
{
    public class AccountController : BaseController
    {
       //[Dependency]
        IAccountService accountService { get; set; }

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [DefaultActionFilter]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            RegisterViewModel registeModal = new RegisterViewModel();
            registeModal.Password = "MB248001";
            registeModal.ConfirmPassword = "MB248001";
            registeModal.Email = "maheshbailwal@gmail.com";
            registeModal.UserName = "bailwal";
            return View(registeModal);
        }

        //
        // POST: /Account/Register
      //  [DefaultActionFilter]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = Mapper.Map<RegisterViewModel, Account>(model);
                accountService.Register(account);
                Session["userValidated"] = true;
                return RedirectToAction("Index", "Index");
            }

            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            var account = Mapper.Map<LoginViewModel, Account>(model);
            var succes = false;

            if (ModelState.IsValid)
            {
                var validAcc = accountService.Authentication(account);
                if (validAcc != null)
                {
                    Session["userValidated"] = true;
                    Session["LoggedInUserAccount"] = validAcc;
                    FormsAuthentication.SetAuthCookie(account.Email, false);
                    succes = true;
                }
            }

            if (isSPAResquest)
            {
                return Json(succes, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public JsonResult GetLoginViewModel()
        {
            var model = new LoginViewModel();
            model.UserName = "maheshbailwal@gmail.com";
            model.Password = "MB248001";
            return Json(model, JsonRequestBehavior.AllowGet);

        }
    }
}
