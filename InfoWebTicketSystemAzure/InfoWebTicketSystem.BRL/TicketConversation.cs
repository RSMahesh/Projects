using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using DataBaseConnectionProvider;
using DataBaseConnectionProvider.Interface;


namespace InfoWebTicketSystem.BRL
{
    public class TicketConversation : BaseBRL
    {

        public TicketConversation()
        {
        }

        public void InsertConversation(Guid ticketId, string message,
         string attachment, Guid userId, bool staff, string userEmailId)
        {
                __InsertConversation(ticketId, message, attachment, userId, staff, userEmailId);
        }

    
        private void __InsertConversation(Guid ticketId, string message,
        string attachment, Guid userId, bool staff,  string userEmailId)
        {
            DAL.TicketConversation ticCon = new InfoWebTicketSystem.DAL.TicketConversation();
            ticCon.InsertConversation(ticketId, message, attachment, userId, staff, userEmailId);
        }

        public DataTable GetConversion(string userEmailId, Guid ticketId)
        {
            DataTable dt = null;
                DAL.TicketConversation ticCon = new InfoWebTicketSystem.DAL.TicketConversation();
                dt = ticCon.GetConversion(userEmailId, ticketId);
            return dt;

        }

    }
}
