using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseConnectionProvider.Interface;

namespace ResellerClub.DataAccess
{
   public  class AdminQuery : DALBase
    {
       public AdminQuery(IConnection connection)
           : base(connection)
       {
       }
       public System.Data.DataTable ExcueteQuery(string query, bool fillSchema)
        {
           // query = HandleSingleQuotes(query);
            connection.Select(query);
            return connection.ExecuteDataTable(fillSchema);
        }

        public void SaveQuery(string query)
        {
            query = HandleSingleQuotes(query);
            string cmdText = "INSERT INTO AdminQuery ([Query]) VALUES ('" + query + "')";
            connection.ExecuteSQL(cmdText);
        }

        public void DeleteQuery(int queryId)
        {
            string cmdText = "DELETE from AdminQuery where ID="+ queryId.ToString();
            connection.ExecuteSQL(cmdText);
        }

        public void UpdateQuery(int queryId, string query)
        {
            query = HandleSingleQuotes(query);
            string cmdText = "UPDAtE AdminQuery SET [Query] ='"+query+"' where ID=" + queryId.ToString();
            connection.ExecuteSQL(cmdText);
        }

       public System.Data.DataTable GetAllQuery()
        {
            string query = "select * from AdminQuery with(nolock) ";
            connection.Select(query);
            return connection.ExecuteDataTable(false);
     
        }
    }
}
