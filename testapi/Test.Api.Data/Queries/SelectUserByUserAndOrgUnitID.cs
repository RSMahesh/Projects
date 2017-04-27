using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Data.Queries
{
    public class SelectUserByUserAndOrgUnitID : IDataQuery
    {
        public SelectUserByUserAndOrgUnitID(IQueryField userID, IQueryField orgUnitID)
        {
            UserID = userID;
            OrgUnitID = orgUnitID;
        }

        public IQueryField UserID { get; private set; }

        public IQueryField OrgUnitID { get; private set; }
    }
}
