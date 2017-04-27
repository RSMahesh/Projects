using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using DataBaseConnectionProvider;

namespace InfoWebTicketSystem.BRL
{
    public class Ticket : BaseBRL
    {
        public Guid InsertTicket(string priority, string subject, string message, string attachment, string type, string userEmailId, string domain, string contactNumber, string department, string lastReplier, string userName)
        {
            Guid ticketId = Guid.NewGuid();
                DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket();
                ticket.InsertTicket(ticketId, "OP", priority, subject, type, userEmailId, domain, contactNumber, department, lastReplier, userName);

                TicketConversation tikConv = new TicketConversation();
                Guid userId = GetEmptyGuid();

                tikConv.InsertConversation(ticketId, message, attachment, userId, false, userEmailId);
       
            return ticketId;
        }


        public void UpdateTicket(Guid ticketId, string status, string priority)
        {
                DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket();
                ticket.UpdateTicket(ticketId, status, priority);
        
        }

        public DataTable GetUserAllTickets(string userEmailId)
        {
            DataTable dt;
                DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket();
                dt = ticket.GetUserAllTickets(userEmailId );
        
            return dt;
        }

        public DataTable GetALLTickets()
        {
            DataTable dt;
                DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket();
                dt = ticket.GetAllTickets();
        
            return dt;
        }

        public DataTable GetUserTicket(string userEmailId,Guid ticketId)
        {
                DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket();
                return ticket.GetUserTicket(userEmailId, ticketId);
         
            
        }


    }
}
