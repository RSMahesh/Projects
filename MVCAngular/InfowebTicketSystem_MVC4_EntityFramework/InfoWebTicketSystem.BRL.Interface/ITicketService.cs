using System;
using System.Web;
namespace InfoWebTicketSystem.BRL.Interface
{
    public interface ITicketService
    {

        Guid AddTicket(Entities.Ticket ticket);
        System.Collections.Generic.List<Entities.Ticket> GetALLTickets();
        Entities.Ticket GetTicket(Guid ticketId);
        System.Collections.Generic.List<Entities.Ticket> GetUserAllTickets(string userEmailId);
        void UpdateTicket(Entities.Ticket ticket);
        string SaveUploadedFile(string rootPath, HttpPostedFileBase uploadedFile);
        string GetUploadedFileName(string file);
    }
}
