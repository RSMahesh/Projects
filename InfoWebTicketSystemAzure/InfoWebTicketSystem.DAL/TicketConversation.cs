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
    public class TicketConversation
    {
        public TicketConversation(IConnection connection)
        {
        }
        public TicketConversation()
        {
        }
        public void InsertConversation(Guid ticketId, string message,
           string attachment, Guid userId, bool staff, string userEmailId)
        {

            TicketAndTicketConversationRow ticketAndTicketConversationRow = new TicketAndTicketConversationRow();
            ticketAndTicketConversationRow.PartitionKey = userEmailId;
            ticketAndTicketConversationRow.FID = Guid.NewGuid();
            ticketAndTicketConversationRow.Kind = Kind.TicketConversation.ToString();

            ticketAndTicketConversationRow.TicketId = ticketId;
            ticketAndTicketConversationRow.Message = message;
            ticketAndTicketConversationRow.Attachment = attachment;
            ticketAndTicketConversationRow.UserId = userId;
            ticketAndTicketConversationRow.Staff = staff.ToString();
            ticketAndTicketConversationRow.UserEmail = userEmailId;

            ticketAndTicketConversationRow.LastUpdate = DateTime.Now;
            ticketAndTicketConversationRow.InsertDate = DateTime.Now;
            
            TableDataStore tableDataStore = new TableDataStore();
            tableDataStore.Insert(ticketAndTicketConversationRow);

            //connection.ExecuteSQL(cmdText);
        }

        public DataTable GetConversion(string userEmailId,Guid ticketId)
        {
            string query = "select * from TicketConversation where ticketId = '" + ticketId.ToString() + "' Order by insertDate";
            //  connection.Select(query);
         //   return new DataTable();

              var tableDataStore = new TableDataStore();

            var amsEventTrackingEntity =
                tableDataStore.Read<TicketAndTicketConversationRow>(
                    x => x.PartitionKey == userEmailId && x.TicketId == ticketId && x.Kind == Kind.TicketConversation.ToString())
                    .FirstOrDefault();

              return ModelExtensions.GetDataTableFromObjects(new []{ amsEventTrackingEntity});
            }
      
    }
}
