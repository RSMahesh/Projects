using InfoWebTicketSystem.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfoWebTicketSystem.MVC.ViewModels;
using System.IO;
using AutoMapper;
using InfoWebTicketSystem.BRL.Interface;

namespace InfoWebTicketSystem.MVC.Controllers
{
    public class NewTicketController : BaseController
    {

        ITicketService ticketService;
        public NewTicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SelectDepartment()
        {
            if (isSPAResquest)
                return PartialView(@"~/Views/SPA/NewTicket/SelectDepartment.cshtml");
            else
                return View();

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SelectDepartment(SelectDepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Session["SelectedDepartment"] = model.Department;
                if (isSPAResquest)
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return RedirectToAction("CollectTicketDetail");
            }

            if (isSPAResquest)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult CollectTicketDetail()
        {
            if (isSPAResquest)
                return PartialView(@"~/Views/SPA/NewTicket/CollectTicketDetail.cshtml");
            else
                return View();

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CollectTicketDetail(CollectTicketDetailViewModal model)
        {
            if (ModelState.IsValid)
            {
                if (model.AttachedFile != null && !string.IsNullOrEmpty(model.AttachedFile.FileName))
                {
                    model.Attachment = ticketService.SaveUploadedFile(GetPhysicalUploadFolder(), model.AttachedFile);
                }

                var ticket = Mapper.Map<CollectTicketDetailViewModal, Entities.Ticket>(model);

                ticket.Department = Session["SelectedDepartment"].ToString();
                ticket.UserID = LoggedInUserAccount.ID;
                ticket.LastUpdatedUserID = LoggedInUserAccount.ID;
                ticketService.AddTicket(ticket);

                ticket = ticketService.GetTicket(ticket.ID);

                var submitConfirmationVM = Mapper.Map<Entities.Ticket, SubmitConfirmationViewModal>(ticket);
                submitConfirmationVM.Message = model.Message;
                if (isSPAResquest)
                {
                    return Json(submitConfirmationVM, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View("SubmitConfirmation", submitConfirmationVM);
                }
            }

            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult SubmitConfirmation()
        {
            if (isSPAResquest)
                return PartialView(@"~/Views/SPA/NewTicket/SubmitConfirmation.cshtml");
            else
                return View();

        }


        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Confirmation()
        {
            return View();
        }
    }
}
