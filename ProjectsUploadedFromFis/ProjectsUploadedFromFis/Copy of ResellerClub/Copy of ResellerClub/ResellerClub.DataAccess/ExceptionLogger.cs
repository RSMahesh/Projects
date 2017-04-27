using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseConnectionProvider.Interface;

namespace ResellerClub.DataAccess
{
    public class ExceptionLogger : DALBase
    {
        public ExceptionLogger(IConnection connection)
            : base(connection)
        {
        }

        public void LogException(Nullable<Guid> sessionID, string userIP, string message, string stackTrace, string url, string additionalInfo)
        {
            message = HandleSingleQuotes(message);
            stackTrace = HandleSingleQuotes(stackTrace);
            additionalInfo = HandleSingleQuotes(additionalInfo);
          
            string cmdText = "insert into [ErrorLog] (SessionFID,UserIP,ErrorMessage,StackTrace,Url,additionalInfo,InsertDate)  values (" +
                 (sessionID.HasValue ? "'" + sessionID.ToString() + "'": "NULL")  + "," +
                "'" + ( userIP == null ||  userIP.Trim() == "" ? "NULL" : userIP) + "'," +
              "'" + message + "'," +
              "'" + stackTrace + "'," +
              "'" + (url == null || url.Trim() == "" ? "NULL" : url) + "'," +
               "'" + (additionalInfo == null || additionalInfo.Trim() == "" ? "NULL" : additionalInfo) + "'," +
              "'" + DateTime.Now.ToString() + "')";
            connection.ExecuteSQL(cmdText);

        }
    }
}
