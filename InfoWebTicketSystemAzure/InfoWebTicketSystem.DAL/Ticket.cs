using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using AExpense.Model;

using DataBaseConnectionProvider.Interface;

using Entities;

namespace InfoWebTicketSystem.DAL
{
    public class Ticket 
    {
        public Ticket()
        {
        }
        public void InsertTicket(Guid ticketId, string status,
            string priority, string subject, string type, string userEmailId, string domain, string contactNumber, string department, string lastReplier, string userName)
        {
            TicketAndTicketConversationRow ticketAndTicketConversationRow = new TicketAndTicketConversationRow();
            ticketAndTicketConversationRow.PartitionKey = userEmailId;
            ticketAndTicketConversationRow.RowKey = ticketId.ToString();
            ticketAndTicketConversationRow.Kind = Kind.Ticket.ToString();
            ticketAndTicketConversationRow.TicketNumber = DateTime.Now.GetHashCode();
            
          //  ticketAndTicketConversationRow.TicketId = ticketId;

            ticketAndTicketConversationRow.Status = status;

            ticketAndTicketConversationRow.Priority = priority;

            ticketAndTicketConversationRow.Subject = subject;

            ticketAndTicketConversationRow.Type = type;

            ticketAndTicketConversationRow.UserEmail = userEmailId;

            ticketAndTicketConversationRow.Domain = domain;

            ticketAndTicketConversationRow.ContactNumber = contactNumber;

            ticketAndTicketConversationRow.Department = department;

            ticketAndTicketConversationRow.LastReplier = lastReplier;

            ticketAndTicketConversationRow.UserName = userName;

            ticketAndTicketConversationRow.LastUpdate = DateTime.Now;

            ticketAndTicketConversationRow.InsertDate = DateTime.Now;

            TableDataStore tableDataStore = new TableDataStore();
            tableDataStore.Insert(ticketAndTicketConversationRow);


        }

        public void UpdateTicket(Guid ticketId, string status,
        string priority)
        {
            string cmdText = "update [Ticket] set status= '" + status + "'," +
                "priority='" + priority + "'," +
                "LastUpdate='" + DateTime.Now.ToString() + "'" +
              " where fid='" + ticketId.ToString() + "'";

        }

        public DataTable GetUserAllTickets(string userEmailId)
        {
            string query = "select * from Ticket where UserEmail = '" + userEmailId + "' Order by LastUpdate desc";
            var tableDataStore = new TableDataStore();

            var amsEventTrackingEntity =
              tableDataStore.Read<TicketAndTicketConversationRow>(
                  x => x.PartitionKey == userEmailId)
                  .FirstOrDefault();

            return ModelExtensions.GetDataTableFromObjects(new[] { amsEventTrackingEntity });
            return null;
           
        }


        public DataTable GetAllTickets()
        {
            string query = "select * from Ticket Order by LastUpdate desc";
            //connection.Select(query);
            //return connection.ExecuteDataTable();
            return null;
        }
        public DataTable GetUserTicket(string userEmailId,Guid ticketID)
        {
            var tableDataStore = new TableDataStore();

            var amsEventTrackingEntity =
                tableDataStore.Read<TicketAndTicketConversationRow>(
                    x => x.PartitionKey == userEmailId && x.RowKey == ticketID.ToString())
                    .FirstOrDefault();

              return ModelExtensions.GetDataTableFromObjects(new []{ amsEventTrackingEntity});
            }
            
        
    }
}
