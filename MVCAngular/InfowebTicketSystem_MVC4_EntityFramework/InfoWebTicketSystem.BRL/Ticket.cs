using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
//using DataBaseConnectionProvider;

namespace InfoWebTicketSystem.BRL
{
    public class Ticket : BaseBRL
    {
        public Guid InsertTicket(string priority, string subject, string message, string attachment, string type, string userEmailId, string domain, string contactNumber, string department, string lastReplier, string userName)
        {
            Guid ticketId = Guid.NewGuid();
            //using (var connection = ConnectionFactory.GetConnection())
            //{
            //    DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket(connection);
            //    ticket.InsertTicket(ticketId, "OP", priority, subject, type, userEmailId, domain, contactNumber, department, lastReplier, userName);

            //    TicketConversation tikConv = new TicketConversation();
            //    Guid userId = GetEmptyGuid();
                
            //    tikConv.InsertConversation(ticketId, message, attachment, userId, false, connection);
            //}

            return ticketId;
        }


        public void UpdateTicket(Guid ticketId, string status, string priority)
        {
            //using (var connection = ConnectionFactory.GetConnection())
            //{
            //    DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket(connection);
            //    ticket.UpdateTicket(ticketId, status, priority);
            //}

        }

        public DataTable GetUserAllTickets(string userEmailId)
        {
            DataTable dt;
            //using (var connection = ConnectionFactory.GetConnection())
            //{
            //    DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket(connection);
            //    dt = ticket.GetUserAllTickets(userEmailId );
            //}

            return dt;
        }

        public DataTable GetALLTickets()
        {
            DataTable dt;
            //using (var connection = ConnectionFactory.GetConnection())
            //{
            //    DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket(connection);
            //    dt = ticket.GetAllTickets();
            //}

            return dt;
        }

        public DataTable GetUserTicket(Guid ticketId)
        {
            DataTable dt;
            //using (var connection = ConnectionFactory.GetConnection())
            //{
            //    DAL.Ticket ticket = new InfoWebTicketSystem.DAL.Ticket(connection);
            //    dt = ticket.GetUserTicket(ticketId);
            //}

            return dt;
        }


    }
}
