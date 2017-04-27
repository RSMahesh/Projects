using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ResellerClub.Common.Logging;
using DataBaseConnectionProvider;
using DataBaseConnectionProvider.Interface;


namespace ResellerClub.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConnectionFactory
    {
        public static IConnection GetConnection(string connectionString)
        {

            if (ConfigurationManager.AppSettings["ConnectionProvider"] != null &&
                ConfigurationManager.AppSettings["ConnectionProvider"].ToUpperInvariant() == "DATASET")
            {
                return (IConnection)new DataSetConnectionProvider();
            }
            else
            {
                return (IConnection)new SQLConnectionProvider(connectionString);
            }

        }

        public static IConnection GetConnection()
        {
            if (ConfigurationManager.AppSettings["ConnectionProvider"] != null &&
                 ConfigurationManager.AppSettings["ConnectionProvider"].ToUpperInvariant() == "DATASET")
            {
                return (IConnection)new DataSetConnectionProvider();
            }
            else
            {
                return (IConnection)new SQLConnectionProvider(ConfigurationManager.AppSettings["DBConnection"]);
            }
        }
    }
}

