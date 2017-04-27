using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ResellerClub.Interface
{
    public interface IAdminQuery : IBaseInterface 
    {
        DataTable ExcueteQuery(string query, bool fillSchema);
       void SaveQuery(string query);
       void DeleteQuery(int queryId);
       DataTable GetAllQuery();
        
    }
}
