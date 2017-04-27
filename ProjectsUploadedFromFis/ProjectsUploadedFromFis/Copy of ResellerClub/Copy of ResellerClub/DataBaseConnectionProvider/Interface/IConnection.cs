using System;
using System.Data;
namespace DataBaseConnectionProvider.Interface
{
    public interface IConnection : IDisposable
    {
        void AddParameter(string parametrName, object parametrvalue);
        void AddParameter(string parametrName, object parametrvalue, System.Data.ParameterDirection parameterDirection);
        void Close();
        IDataReader ExecuteDataReader();
        System.Data.DataSet ExecuteDataSet();
        System.Data.DataTable ExecuteDataTable(bool fillSchema);
        bool ExecuteNonQuery();
        
        object ExecuteScalar();
        bool ExecuteSQL(string SQL);

        void Insert(string storedProcedureName);
        void Open();
        void Select(string SQL);
      
        void StoredProcedure(string storedProcedureName);
        void Update(string storedProcedureName);

        void Dispose();
    }
}
