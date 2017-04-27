using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseConnectionProvider.Interface;

namespace ResellerClub.DataAccess
{
    public class SessionLogger : DALBase
    {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SessionLogger(IConnection sqlConnection)
            : base(sqlConnection)
        { }

        public void Insert(Guid fid, string sessionId, string userEmailId, string userIP)
        {
            string cmdText = "Insert into SessionLog (ID,AspSessionID,UserEmailID,InsertDate,UserIP)  values (" +
                "'" + fid + "'," +
                  "'" + sessionId + "'," +
                "'" + userEmailId + "'," +
                "'" + DateTime.Now.ToString() + "'," +
                "'" + userIP + "')";
            connection.ExecuteSQL(cmdText);
           
        }

        public string GetUserEmail(Guid sessionId)
        {
            string cmdText = "select UserEmailID from SessionLog with(nolock) where ID='" + sessionId.ToString() + "'";
            connection.Select(cmdText);
            return (string)connection.ExecuteScalar();
        }
    }
}
