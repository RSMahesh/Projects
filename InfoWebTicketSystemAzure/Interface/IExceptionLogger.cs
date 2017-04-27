using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResellerClub.Interface
{
    public interface IExceptionLogger 
    {
       void LogException(Exception ex, Nullable<Guid> sessionId, string userIP, string url, string additionalInfo,int exceptionObjectHash, int innerExceptionCount);
    }
}
