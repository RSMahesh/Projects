using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataBaseConnectionProvider.Interface;

namespace InfoWebTicketSystem.DAL
{
  public  class TicketConversation : DALBase
    {
        public TicketConversation(IConnection connection)
            : base(connection)
        {
        }
        public void InsertConversation(Guid ticketId, string message,
           string attachment, Guid  userId, bool staff)
       {
            
           string cmdText = "insert into [TicketConversation] (FID,ticketId,attachment,message,userId,staff,InsertDate,UpdateDate)  values (" +
             "'" + Guid.NewGuid().ToString() + "'," +
               "'" + ticketId.ToString() + "'," +
               "'" + attachment + "'," +
               "'" + HandleSingleQuotes(message) + "'," +
             "'" + userId + "'," +
             "" + (staff==true ?"1":"0") + "," +    
             "'" + DateTime.Now.ToString() + "'," +
             "'" + DateTime.Now.ToString() + "')";
           connection.ExecuteSQL(cmdText);
       }

        public DataTable GetConversion(Guid ticketId)
        {
            string query = "select * from TicketConversation where ticketId = '" + ticketId.ToString() + "' Order by insertDate";
            connection.Select(query);
            return connection.ExecuteDataTable();
        }

    }
}
