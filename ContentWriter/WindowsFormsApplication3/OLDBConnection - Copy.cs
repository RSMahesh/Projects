using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace StatusMaker.Data
{
    public class OLDBConnection12
    {
        string _filePath;
        OleDbDataAdapter _adap;
        OleDbConnection _connection;
        DataTable _dt = new DataTable();
        public OLDBConnection12(string filePath)
        {
            _filePath = filePath;
        }
        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        public void OpenConnnction()
        {
            var connection = Excel.GetExcelConnectionString(_filePath);
            _connection = new OleDbConnection(connection);
            _connection.Open();
        }
        public DataTable ExecuteDatatable(string sql)
        {
            if(Path.GetExtension(_filePath) == ".xml")
            {
                return ReadFromXml(_filePath);
            }

            lock (this)
            {
                this.OpenConnnction();
             
                _dt.TableName = "Sheet1";
                _adap = new OleDbDataAdapter(sql, _connection);
                _adap.Fill(_dt);
               _adap.FillSchema(_dt, SchemaType.Source);

                this.CloseConnection();
                return _dt;
               
            }
        }

        private DataTable ReadFromXml(string file)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(file, XmlReadMode.InferSchema);

            return dataSet.Tables[0];
        }

        public void SetUpdateCommand(DataColumnCollection dataColumns)
        {
            TakeBackUp();

            this.OpenConnnction();

            var updateStatement = "UPDATE [Sheet1$] SET ";
            var command = new OleDbCommand();
            command.Connection = _connection;

            foreach (DataColumn column in dataColumns)
            {

                updateStatement +=" ["+ column.ColumnName + "] = ?  ,";
                command.Parameters.Add("@" + column.ColumnName, OleDbType.Char, 255).SourceColumn = column.ColumnName;
            }

            updateStatement = updateStatement.Substring(0, updateStatement.Length - 2);

            updateStatement += " where ID=? ";
            command.Parameters.Add("@ID", OleDbType.Char, 255).SourceColumn = "ID";

            command.CommandText = updateStatement;
            _adap.UpdateCommand = command;
            _adap.Update(_dt);
            this.CloseConnection();
           
        }

        private void TakeBackUp()
        {
          var backUpDir = Path.Combine(Path.GetDirectoryName(_filePath), Path.GetFileNameWithoutExtension( _filePath) + "_dataBackup");

            if (!Directory.Exists(backUpDir))
            {
                Directory.CreateDirectory(backUpDir);
            }

          var backupFile =  Path.Combine(backUpDir, GetFileName(backUpDir));
           _dt.WriteXml(backupFile,XmlWriteMode.WriteSchema);
         
        }

        private string GetFileName(string backUpDir)
        {
           var fileCount = Directory.GetFiles(backUpDir, "*.xml").Length;
            var fileName = "1";
            for (var i =0; i< fileCount; i++)
            {
                fileName += "1";
            }

            return fileName + ".xml";
        }
    }
}
