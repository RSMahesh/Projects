using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResellerClub.Interface
{
    public interface ISessionLogger : IBaseInterface 
    {
        void Insert(Guid fid, string sessionId, string userEmailId, string userIP);
        string  GetUserEmail(Guid sessionId);
    }
}
