using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResellerClub.Interface
{
    public interface IBaseInterface
    {
        string UserIP { get; set; }
        Nullable<Guid> SessionID { get; set; }
        string UserURL { get; set; }
    }
}
