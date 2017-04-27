using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using InfoWebTicketSystem.MVC.Filters;
using InfoWebTicketSystem.MVC.ViewModels;
using Entities;

namespace InfoWebTicketSystem.MVC.Controllers
{

    public abstract partial class BaseController : Controller
    {
        public BaseController()
        {

        }

        protected string GetAttachmentPath(HttpPostedFileBase uploadFile)
        {
            if (uploadFile.FileName == null || uploadFile.FileName.Trim() == "")
                return "";
            string file = GetPhysicalUploadFolder() + "\\" + Guid.NewGuid().ToString() + "______" + uploadFile.FileName;
            uploadFile.SaveAs(file);
            return file;
        }

        protected string GetPhysicalUploadFolder()
        {
            return Server.MapPath("/" + GetUploadFolderName());
        }

        protected string GetUploadFolderName()
        {
            return @"UploadedFiles";
        }

   

        protected Account LoggedInUserAccount
        {
            get
            {
                if (Session["LoggedInUserAccount"] != null)
                {
                    return (Account)Session["LoggedInUserAccount"];
                }
                return null;
            }
        }
        // Session["LoginedUserAccount"] = account;
    }

    public abstract partial class BaseController12 : Controller
    {
        #region singleactioncontoller
        private const string CANONICAL_ACTION_NAME = "execute";

        protected override void ExecuteCore()
        {
            ControllerContext.RouteData.Values["action"] = GetControllerName();
            ControllerContext.RouteData.Values["controller"] = GetNamespace();
            bool succeeded = ActionInvoker.InvokeAction(ControllerContext, CANONICAL_ACTION_NAME);
            if (!succeeded)
            {
                HandleUnknownAction(CANONICAL_ACTION_NAME);
            }
        }

        public string GetControllerName()
        {
            return GetType().Name.ToLowerInvariant().Replace("controller", "");
        }

        public string GetNamespace()
        {
            string[] namespaces = GetType().Namespace.Split('.');
            string immediateNamespace = namespaces[namespaces.Length - 1];
            return immediateNamespace;
        }

        #endregion
    }
}
