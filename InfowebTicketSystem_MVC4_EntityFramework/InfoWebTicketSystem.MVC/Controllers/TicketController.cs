using AutoMapper;
using InfoWebTicketSystem.BRL.Interface;
using InfoWebTicketSystem.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfoWebTicketSystem.MVC.Controllers
{
    public class TicketController : BaseController
    {
        ITicketService ticketService;

        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        public ActionResult TicketList()
        {
            var tickLst = ticketService.GetALLTickets();
            var ttNew = new ViewModels.TicketList();
            foreach (var t in tickLst)
            {
                ttNew.Tickets.Add(Mapper.Map<Entities.Ticket, ViewModels.Ticket>(t));
            }
            return View(ttNew);
        }

        [HttpGet]
        public ActionResult ViewTicket(Guid ticketId)
        {
            var ticket = ticketService.GetTicket(ticketId);
            var vtVM = Mapper.Map<Entities.Ticket, ViewModels.ViewTicketViewModel>(ticket);
            foreach (var conv in ticket.TicketConversations)
            {

                ViewModels.TicketConversation tconv = new TicketConversation();
                tconv.Message = conv.Message;
             
                tconv.CreateDate = conv.CreateDate.Value ;
                tconv.LastUpdateDate = conv.LastUpdateDate.Value;

                if (!string.IsNullOrEmpty(conv.Attachment))
                {
                    tconv.AttachmentFileName = ticketService.GetUploadedFileName(conv.Attachment);
                    tconv.AttachmentFileRaw = "/" + GetUploadFolderName() + "/" + Path.GetFileName(conv.Attachment);
                }

                tconv.User = conv.LastUpdatedUser.UserName;
                tconv.Staff = conv.Staff ? "STAFF" : "USER";
                vtVM.Conversation.Add(tconv);

            }

           // return  View(vtVM);
            return PartialView(vtVM);
        }

        
        [HttpPost]
        public ActionResult UpdateTicket(ViewModels.ViewTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = ticketService.GetTicket(model.ID);

               
                if (!string.IsNullOrEmpty(model.Message))
                {
                    Entities.TicketConversation conver = new Entities.TicketConversation();
                    conver.Message = model.Message;
                    conver.TicketID = ticket.ID;
                    conver.CreateDate = DateTime.Now;
                    conver.LastUpdateDate = DateTime.Now;
                    conver.ID = Guid.NewGuid();
                    conver.LastUpdatedUserID = LoggedInUserAccount.ID;
                    conver.Staff = LoggedInUserAccount.Staff;

                    if (model.AttachedFile != null && !string.IsNullOrEmpty(model.AttachedFile.FileName))
                    {
                        conver.Attachment = ticketService.SaveUploadedFile(GetPhysicalUploadFolder(), model.AttachedFile);
                    }
          
                    ticket.TicketConversations.Add(conver);
                }
                ticket.Priority = model.Priority;
                ticket.Status = model.Status;

                ticketService.UpdateTicket(ticket);
                ticket.LastUpdatedUserID = LoggedInUserAccount.ID;

                return RedirectToAction("ViewTicket", new { ticketId = ticket.ID});

            }

            return View();
        }
    }
}
