using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;



namespace InfoWebTicketSystem.BRL
{
    public class TicketConversation : BaseBRL
    {

        public TicketConversation()
        {
        }

        public void InsertConversation(Guid ticketId, string message,
         string attachment, Guid userId, bool staff)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                __InsertConversation(ticketId, message, attachment, userId, staff, connection);
            }
        }

        internal void InsertConversation(Guid ticketId, string message,
         string attachment, Guid userId, bool staff, IConnection connection)
        {
            __InsertConversation(ticketId, message, attachment, userId, staff, connection);
        }


        private void __InsertConversation(Guid ticketId, string message,
        string attachment, Guid userId, bool staff, IConnection connection)
        {
            DAL.TicketConversation ticCon = new InfoWebTicketSystem.DAL.TicketConversation(connection);
            ticCon.InsertConversation(ticketId, message, attachment, userId, staff);
        }

        public DataTable GetConversion(Guid ticketId)
        {
            DataTable dt = null;
            using (var connection = ConnectionFactory.GetConnection())
            {
                DAL.TicketConversation ticCon = new InfoWebTicketSystem.DAL.TicketConversation(connection);
                dt = ticCon.GetConversion(ticketId);
            }
            return dt;

        }

    }
}
