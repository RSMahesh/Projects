using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface;
using ResellerClub.DataAccess;

namespace ResellerClub.BusinessLogic
{
    public class SessionLogger : BaseBRL, ISessionLogger
    {
        #region ISessionLogger Members

        public void Insert(Guid fid, string sessionId, string userEmailId, string userIP)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var sessionLogger = new ResellerClub.DataAccess.SessionLogger(connection);
                sessionLogger.Insert(fid, sessionId, userEmailId, userIP);
            }
        }
        public string GetUserEmail(Guid sessionId)
        {
            string userEmail = "";
            using (var connection = ConnectionFactory.GetConnection())
            {
                var sessionLogger = new ResellerClub.DataAccess.SessionLogger(connection);
                userEmail = sessionLogger.GetUserEmail(sessionId);
            }
            return userEmail;
        }
        #endregion
    }
}
