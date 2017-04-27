using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseConnectionProvider.Interface;
using DataSetServiceProvider;

namespace DataBaseConnectionProvider
{
   public  class DataSetConnectionProvider : IConnection
    {

        string sqlQuery;
        QueryParser queryParser;
        static string rootPath = @"C:\DataBaseBackUp\";

        public DataSetConnectionProvider()
        {
            queryParser = new QueryParser();
        }


        public void AddParameter(string parametrName, object parametrvalue)
        {
           
        }

        public void AddParameter(string parametrName, object parametrvalue, System.Data.ParameterDirection parameterDirection)
        {
           
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
           
        }

        public System.Data.IDataReader ExecuteDataReader()
        {
         
           var dt = queryParser.LoadDataTableFromFile(sqlQuery,rootPath);
          return  queryParser.ExecuteReader(dt, sqlQuery);
        }

        public System.Data.DataSet ExecuteDataSet()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ExecuteDataTable(bool fillSchema)
        {
            var dt = queryParser.LoadDataTableFromFile(sqlQuery, rootPath);
            return queryParser.ExcuteDataTable(dt, sqlQuery);
        }

        public bool ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public bool ExecuteSQL(string SQL)
        {
            throw new NotImplementedException();
        }

        public void Insert(string storedProcedureName)
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Select(string SQL)
        {
            sqlQuery = SQL;
            sqlQuery = sqlQuery.Replace("[", "").Replace("]", "");
        }

        public void StoredProcedure(string storedProcedureName)
        {
            throw new NotImplementedException();
        }

        public void Update(string storedProcedureName)
        {
            throw new NotImplementedException();
        }
       
    }
}
