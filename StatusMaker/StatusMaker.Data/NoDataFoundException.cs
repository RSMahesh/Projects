using System;
using System.Runtime.Serialization;

namespace StatusMaker.Data
{
    [Serializable]
    public class NoDataFoundException : Exception
    {
        public NoDataFoundException()
        {
        }

        public NoDataFoundException(string message) : base(message)
        {
        }

        public NoDataFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDataFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}