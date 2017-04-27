using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace BaseDAL
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Connection : IDisposable
    {
        readonly  int DEFAULT_COMMAND_TIMEOUT = 600;
        readonly  int MAX_FLD_NAME_LEN = 128;
        /// <summary>
        /// 
        /// </summary>
        public int timeout;
        /// <summary>
        /// 
        /// </summary>
        public SqlConnection connection;
        /// <summary>
        /// 
        /// </summary>
        public SqlTransaction transaction;
        /// <summary>
        /// 
        /// </summary>
        public SqlCommand result;
        /// <summary>
        /// 
        /// </summary>
        public SqlCommand command;
        /// <summary>
        /// 
        /// </summary>
        public SqlDataReader dataReader;

        private bool isDisposed = false;

        public static Connection GetConnection(string connectionString)
        {
            return new Connection(connectionString); 
        }

        public static Connection GetConnection()
        {
            return new Connection(ConfigurationManager.AppSettings["DBConnection"]);
        }

        /// <summary>
        /// 
        /// </summary>
        private Connection(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            Open();
            timeout = DEFAULT_COMMAND_TIMEOUT;
        }

        /// <summary>
        /// 
        /// </summary>
        ~Connection()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    //dispose of all managed code here
                    if (null != connection)
                    {
                        connection.Dispose();
                    }
                }
                //dispose of all unmanaged code here

                isDisposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public bool ExecuteSQL(string SQL)
        {
            command = new SqlCommand(SQL);

            if (transaction != null)
            {
                command.Transaction = transaction;
                command.Connection = transaction.Connection;
            }
            else if (connection != null)
                command.Connection = connection;

            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = timeout;
            return ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        public void StoredProcedure(string storedProcedureName)
        {
            command = new SqlCommand(storedProcedureName);

            if (transaction != null)
            {
                command.Transaction = transaction;
                command.Connection = transaction.Connection;
            }
            else if (connection != null)
                command.Connection = connection;

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = timeout;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        public void Insert(string storedProcedureName)
        {
            StoredProcedure(storedProcedureName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        public void Update(string storedProcedureName)
        {
            StoredProcedure(storedProcedureName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        public void Generate(string storedProcedureName)
        {
            StoredProcedure(storedProcedureName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        public void Set(string storedProcedureName)
        {
            StoredProcedure(storedProcedureName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        public void Select(string SQL)
        {
            command = new SqlCommand(SQL);

            if (transaction != null)
            {
                command.Transaction = transaction;
                command.Connection = transaction.Connection;
            }
            else if (connection != null)
                command.Connection = connection;

            command.CommandTimeout = timeout;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataSet()
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataSet);
            return dataSet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteDataTable()
        {
            var dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        public DataSet ExecuteSQLQuery(string SQL)
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataSet);
            return dataSet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void AddParameter(SqlParameter parameter)
        {
            command.Parameters.Add(parameter);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="fieldValue"></param>
        /// <param name="sqlDbType"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="parameterDirection"></param>
        public void AddParameter(string field, object fieldValue, SqlDbType sqlDbType, byte precision, byte scale, ParameterDirection parameterDirection)
        {
            SqlParameter parameter = new SqlParameter(field, sqlDbType);
            parameter.Direction = parameterDirection;
            parameter.Precision = precision;
            parameter.Scale = scale;
            parameter.Value = fieldValue;
            command.Parameters.Add(parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="fieldValue"></param>
        /// <param name="sqlDbType"></param>
        /// <param name="size"></param>
        /// <param name="parameterDirection"></param>
        public void AddParameter(string field, object fieldValue, SqlDbType sqlDbType, int size, ParameterDirection parameterDirection)
        {
            SqlParameter parameter = new SqlParameter(field, sqlDbType, size);
            parameter.Direction = parameterDirection;
            parameter.Value = fieldValue;
            command.Parameters.Add(parameter);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void AddResult(SqlParameter parameter)
        {
            result.Parameters.Add(parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object  ExecuteScalar()
        {
            return command.ExecuteScalar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ExecuteNonQuery()
        {
            return (command.ExecuteNonQuery() != 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ExecuteReader()
        {
            return (ExecuteReader(System.Data.CommandBehavior.Default));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandBehavior"></param>
        /// <returns></returns>
        public bool ExecuteReader(System.Data.CommandBehavior commandBehavior)
        {
                dataReader = command.ExecuteReader(commandBehavior);
                result = new SqlCommand();
                if (System.Data.CommandBehavior.SingleRow == commandBehavior)
                    return (dataReader.Read());
                
            return (null != dataReader);
         
        }

        /// <summary>
        /// executes the command
        /// </summary>
        /// <returns></returns>
        public SqlDataReader ExecuteDataReader()
        {
            dataReader = command.ExecuteReader();
            return dataReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public SqlParameter ReturnParameter(string field)
        {
            return (command.Parameters[field]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RECORD_NOT_FOUND()
        {
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            connection.Open();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            connection.Close();
        }

        private bool wasError;
        /// <summary>
        /// 
        /// </summary>
        public bool WasError
        {
            get
            {
                return (wasError);
            }
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public SqlParameter ReturnResult(string field)
        {
            return (result.Parameters[field]);
        }
    }
}
