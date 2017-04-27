using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface;
using ResellerClub.DataAccess;
using ResellerClub.Common.Logging;

namespace ResellerClub.BusinessLogic
{
    public class ExceptionLogger : BaseBRL, IExceptionLogger
    {
        public void LogException(Exception ex, Nullable<Guid> sessionId, string userIP, string url, string additionalInfo,int exceptionObjectHash, int innerExceptionCount )
        {
            try
            {
                var exMessage = ex.Message;
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var dalLogger = new ResellerClub.DataAccess.ExceptionLogger(ConnectionFactory.GetConnection());
                    
                    if(ex.InnerException != null || innerExceptionCount > 0)
                    {
                      exMessage =  ex.Message + Environment.NewLine + exceptionObjectHash.ToString() + "[" + innerExceptionCount.ToString() + "]";
                    }
                    
                    dalLogger.LogException(sessionId, userIP, exMessage, ex.StackTrace, url, additionalInfo);
                }
                if (ex.InnerException != null)
                {
                    LogException(ex.InnerException, sessionId, userIP, url, additionalInfo,exceptionObjectHash,++innerExceptionCount);
                }
            }
            catch (Exception excep)
            {
                XmlTraceWriter.TraceInfo("=========================================================");
                XmlTraceWriter.Trace(ex);
                XmlTraceWriter.Trace(excep);
                XmlTraceWriter.TraceInfo(Environment.NewLine + "UserIP:" + userIP + Environment.NewLine
                    + "SessionID:" + sessionId + Environment.NewLine + "AdditionalInfo" + additionalInfo + Environment.NewLine);
                XmlTraceWriter.TraceInfo("=========================================================");

            }



        }
    }
}
