using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResellerClub.Interface;
using ResellerClub.DataAccess;
using System.Data;

namespace ResellerClub.BusinessLogic
{
   public class AdminQuery: BaseBRL, IAdminQuery
    {
        public DataTable ExcueteQuery(string query,bool fillSchema)
        {
            DataTable dt;
            using (var connection = ConnectionFactory.GetConnection())
            {
                var adminQ = new ResellerClub.DataAccess.AdminQuery(connection);
                dt =adminQ.ExcueteQuery(query,fillSchema);
            }
            return dt;
        }

        public void SaveQuery(string query)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var adminQ = new ResellerClub.DataAccess.AdminQuery(connection);
                adminQ.SaveQuery(query);
            }
        }

        public void DeleteQuery(int queryId)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var adminQ = new ResellerClub.DataAccess.AdminQuery(connection);
                adminQ.DeleteQuery(queryId);
            }
        }

        public void UpdateQuery(int queryId, string query)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                var adminQ = new ResellerClub.DataAccess.AdminQuery(connection);
                adminQ.UpdateQuery(queryId,query);
            }
        }


        public System.Data.DataTable GetAllQuery()
        {
            DataTable dt;
            using (var connection = ConnectionFactory.GetConnection())
            {
                var adminQ = new ResellerClub.DataAccess.AdminQuery(connection);
                dt = adminQ.GetAllQuery();
            }
            return dt;
        }
    }
}
