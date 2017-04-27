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
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SelectDepartment(SelectDepartmentViewModel modal)
        {
            if (ModelState.IsValid)
            {
                Session["SelectedDepartment"] = modal.Department;
                return RedirectToAction("CollectTicketDetail");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult CollectTicketDetail()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CollectTicketDetail(CollectTicketDetailViewModal modal)
        {
            if (ModelState.IsValid)
            {
                if (modal.AttachedFile != null && !string.IsNullOrEmpty(modal.AttachedFile.FileName))
                {
                    modal.Attachment = ticketService.SaveUploadedFile(GetPhysicalUploadFolder(),modal.AttachedFile);
                }
                
                var ticket = Mapper.Map<CollectTicketDetailViewModal, Entities.Ticket>(modal);
               
                ticket.Department = Session["SelectedDepartment"].ToString();
                ticket.UserID = LoggedInUserAccount.ID;
                ticket.LastUpdatedUserID = LoggedInUserAccount.ID;
                ticketService.AddTicket(ticket);

                ticket = ticketService.GetTicket(ticket.ID);

                var submitConfirmationVM = Mapper.Map<Entities.Ticket, SubmitConfirmationViewModal>(ticket);
                submitConfirmationVM.Message = modal.Message;
                return View("SubmitConfirmation", submitConfirmationVM);
            }

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
