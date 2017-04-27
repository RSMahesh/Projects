using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DataBaseConnectionProvider.Interface;

namespace ResellerClub.DataAccess
{
    public class State : DALBase
    {

        public State(IConnection  connection)
            : base(connection){}

        public List<string> GetCountryState(string countryCode)
        {
            string cmdText = "Select State from [State] with(nolock) where CountryCode ='" + countryCode + "'";
            connection.Select(cmdText);
            return DataReaderToMessage(connection.ExecuteDataReader());
        }

        private List<string> DataReaderToMessage(IDataReader dataReader)
        {
            var list = new List<string>();
            try
            {
                while (dataReader.Read())
                {
                    list.Add(dataReader.GetString(0));
                }
            }
            finally
            {
                if ((!dataReader.IsClosed) || (dataReader != null))
                {
                    dataReader.Close();
                }
            }
            return list;
        }

        public void InsertCountryState(string countryCode, List<string> stateList)
        {
          
            string cmdText = "Delete from State where CountryCode='" + countryCode + "'";
            connection.ExecuteSQL(cmdText);

            foreach (var state in stateList)
            {
                cmdText = "Insert into State Values(newid(),'"+countryCode+"','"+state+"')";
                connection.ExecuteSQL(cmdText);
            }

        }
    }
}
