using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication3;

namespace StatusMaker.Data
{
    public class OLDBConnection12
    {
        string _filePath;
        OleDbDataAdapter _adap;
        OleDbConnection _connection;
        DataTable _dt = new DataTable();
       const string defaultQuery = "Select * from [Sheet1$]";
        List<DataColumn> addtionalCoumns = new List<DataColumn>(new[] { new DataColumn("ColorCode", typeof(string)),
           new DataColumn(Constants.WordFrequencyColumnName, typeof(string)),
        });


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
            var connection = ExcelProvider.GetExcelConnectionString(_filePath);
            _connection = new OleDbConnection(connection);
            _connection.Open();
        }
        public DataTable ExecuteDatatable(string sql = defaultQuery, bool addCustomColums = true)
        {
            if (Path.GetExtension(_filePath) == ".xml")
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

                if (addCustomColums)
                {
                    foreach (var col in addtionalCoumns)
                    {

                        _dt.Columns.Add(col);
                    }
                }

                this.CloseConnection();
                return _dt;

            }
        }


        public static DataTable ReadFromXml(string file)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(file, XmlReadMode.InferSchema);

            return dataSet.Tables[0];
        }

        public void SaveData(DataColumnCollection dataColumns)
        {

            TakeBackUp();

            this.OpenConnnction();

            var updateStatement = "UPDATE [Sheet1$] SET ";
            var command = new OleDbCommand();
            command.Connection = _connection;

            foreach (DataColumn column in dataColumns)
            {
                if (!addtionalCoumns.Contains(column))
                {
                    updateStatement += " [" + column.ColumnName + "] = ?  ,";
                    command.Parameters.Add("@" + column.ColumnName, OleDbType.Char, 5000).SourceColumn = column.ColumnName;
                }
            }

            updateStatement = updateStatement.Substring(0, updateStatement.Length - 2);

            updateStatement += " where ID=? ";
            command.Parameters.Add("@ID", OleDbType.Char, 255).SourceColumn = "ID";

            command.CommandText = updateStatement;
            _adap.UpdateCommand = command;
            var updateCount = _adap.Update(_dt);
            this.CloseConnection();

        }

        private void TakeBackUp()
        {

            var backUpDir = Path.Combine(Path.GetDirectoryName(_filePath), Path.GetFileNameWithoutExtension(_filePath) + "_dataBackup");

            if (!Directory.Exists(backUpDir))
            {
                Directory.CreateDirectory(backUpDir);
            }

            var backupFile = Path.Combine(backUpDir, GetFileName(backUpDir));
            var dt1 = _dt.GetChanges();

            if (dt1 != null)
            {
                dt1.WriteXml(backupFile, XmlWriteMode.WriteSchema);
            }

            var backupFileDir = Path.Combine(backUpDir, "Full");

            if (!Directory.Exists(backupFileDir))
            {
                Directory.CreateDirectory(backupFileDir);
            }

            _dt.WriteXml(Path.Combine(backupFileDir, GetFileName(backupFileDir)), XmlWriteMode.WriteSchema);

        }

        private string GetFileName(string backUpDir)
        {
            var startFrom = 100;
            var fileCount = Directory.GetFiles(backUpDir, "*.xml").Length;
            startFrom += fileCount;

            //var fileName = "1";
            //for (var i =0; i< fileCount; i++)
            //{
            //    fileName += "1";
            //}

            return startFrom + ".xml";
        }

        public List<string>  GetSheets()
        {
            OpenConnnction();
          var  dt = _connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            List<string> coll = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                coll.Add( row["TABLE_NAME"].ToString());
              
            }
            CloseConnection();
            return coll;
        }
    }
}
