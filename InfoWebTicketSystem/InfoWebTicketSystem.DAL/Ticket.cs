using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using DataBaseConnectionProvider.Interface;

namespace InfoWebTicketSystem.DAL
{
    public class Ticket : DALBase
    {
        public Ticket(IConnection connection)
            : base(connection)
        {
        }
        public void InsertTicket(Guid ticketId, string status,
            string priority, string subject, string type, string userEmailId, string domain, string contactNumber, string department, string lastReplier, string userName)
        {
            string cmdText = "insert into [Ticket] (FID,Status,priority,subject,type,UserEmail,Domain,ContactNumber,Department,LastReplier,userName,InsertDate,LastUpdate)  values (" +
              "'" + ticketId.ToString() + "'," +
              "'" + status + "'," +
              "'" + priority + "'," +
              "'" + HandleSingleQuotes(subject) + "'," +
              "'" + type + "'," +
              "'" + userEmailId + "'," +
              "'" + domain + "'," +
              "'" + contactNumber + "'," +
              "'" + department + "'," +
               "'" + lastReplier + "'," +
               "'" + userName  + "'," +
              "'" + DateTime.Now.ToString() + "'," +
              "'" + DateTime.Now.ToString() + "')";
            connection.ExecuteSQL(cmdText);
        }

        public void UpdateTicket(Guid ticketId, string status,
        string priority)
        {
            string cmdText = "update [Ticket] set status= '" + status + "'," +
                "priority='" + priority + "'," +
                "LastUpdate='" + DateTime.Now.ToString() + "'" +
              " where fid='" + ticketId.ToString() + "'";

            connection.ExecuteSQL(cmdText);
        }

        public DataTable GetUserAllTickets(string userEmailId)
        {
            string query = "select * from Ticket where UserEmail = '" + userEmailId + "' Order by LastUpdate desc";
            connection.Select(query);
            return connection.ExecuteDataTable();
        }


        public DataTable GetAllTickets()
        {
            string query = "select * from Ticket Order by LastUpdate desc";
            connection.Select(query);
            return connection.ExecuteDataTable();
        }
        public DataTable GetUserTicket(Guid ticketID)
        {
            string query = "select * from Ticket where FID = '" + ticketID.ToString() + "'";
            connection.Select(query);
            return connection.ExecuteDataTable();
        }
    }
}
