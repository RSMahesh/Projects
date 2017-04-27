using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using DataBaseConnectionProvider.Interface;

namespace DataBaseConnectionProvider
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConnectionFactory 
    {
        public static IConnection  GetConnection(string connectionString)
        {
            

            return (IConnection)new SQLConnectionProvider(connectionString);
            
            return (IConnection)new DataSetConnectionProvider();
        }

        public static IConnection GetConnection()
        {
            return  (IConnection) new SQLConnectionProvider(ConfigurationManager.AppSettings["DBConnection"]);
            return (IConnection)new DataSetConnectionProvider(); 
        }
    }
}
