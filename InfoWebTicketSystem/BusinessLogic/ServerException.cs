using System;
using System.Collections.Generic;

using System.Text;

namespace ResellerClub.BusinessLogic
{
    public class ServerException : Exception
    {
        public ServerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
