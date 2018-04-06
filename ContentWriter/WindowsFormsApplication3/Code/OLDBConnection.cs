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
    public class OLDBConnection
    {
        string _filePath;
        OleDbDataAdapter _adap;
        OleDbConnection _connection;
        DataTable _dt = new DataTable();
        const string defaultQuery = "Select * from [Sheet1$]";
        List<DataColumn> addtionalCoumns = new List<DataColumn>(new[] { new DataColumn("ColorCode", typeof(string)),
           new DataColumn(Constants.WordFrequencyColumnName, typeof(string)),
        });


        public OLDBConnection(string filePath)
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

        static int count =0;
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

                   var oldbType = GetOleDbType(column.DataType);

                    if (oldbType == OleDbType.Char)
                    {
                        command.Parameters.Add("@" + column.ColumnName, oldbType, 5000).SourceColumn = column.ColumnName;
                    }
                    else
                    {
                        command.Parameters.Add("@" + column.ColumnName, oldbType).SourceColumn = column.ColumnName;
                    }

                }
            }

            updateStatement = updateStatement.Substring(0, updateStatement.Length - 2);

            updateStatement += " where ID=? ";
            command.Parameters.Add("@ID", OleDbType.Double).SourceColumn = "ID";

            command.CommandText = updateStatement;
            _adap.UpdateCommand = command;
            var updateCount = _adap.Update(_dt);
            this.CloseConnection();

        }

        private OleDbType GetOleDbType(Type sysType)
        {
            if (object.ReferenceEquals(sysType, typeof(string)))
            {
                return OleDbType.VarChar;
            }
            else if (object.ReferenceEquals(sysType, typeof(int)))
            {
                return OleDbType.Integer;
            }
            else if (object.ReferenceEquals(sysType, typeof(bool)))
            {
                return OleDbType.Boolean;
            }
            else if (object.ReferenceEquals(sysType, typeof(System.DateTime)))
            {
                return OleDbType.Date;
            }
            else if (object.ReferenceEquals(sysType, typeof(char)))
            {
                return OleDbType.Char;
            }
            else if (object.ReferenceEquals(sysType, typeof(decimal)))
            {
                return OleDbType.Decimal;
            }
            else if (object.ReferenceEquals(sysType, typeof(double)))
            {
                return OleDbType.Double;
            }
            else if (object.ReferenceEquals(sysType, typeof(float)))
            {
                return OleDbType.Single;
            }
            else if (object.ReferenceEquals(sysType, typeof(byte[])))
            {
                return OleDbType.Binary;
            }
            else if (object.ReferenceEquals(sysType, typeof(Guid)))
            {
                return OleDbType.Guid;
            }

            throw new Exception("OleDbType not found for :" + sysType);
        }


        public void CheckForError(DataColumnCollection dataColumns)
        {
            var command = new OleDbCommand();
            command.Connection = _connection;

            List<DataColumn> correctDataColumns = new List<DataColumn>();
            List<DataColumn> errorDataColumns = new List<DataColumn>();

            foreach (DataColumn column1 in dataColumns)
            {

                //   correctDataColumns.Add(column1);
            }

            var updateStatement = string.Empty;

            foreach (DataColumn column1 in dataColumns)
            {
                this.OpenConnnction();
                correctDataColumns.Add(column1);
                updateStatement = "UPDATE [Sheet1$] SET ";
                
                try
                {
                    foreach (DataColumn column in correctDataColumns)
                    {
                        if (!addtionalCoumns.Contains(column))
                        {
                            updateStatement += " [" + column.ColumnName + "] = ?  ,";

                            var oldbType = GetOleDbType(column.DataType);

                            if (oldbType == OleDbType.Char)
                            {
                                command.Parameters.Add("@" + column.ColumnName, oldbType, 5000).SourceColumn = column.ColumnName;
                            }
                            else
                            {
                                command.Parameters.Add("@" + column.ColumnName, oldbType).SourceColumn = column.ColumnName;
                            }
                        }
                    }

                    updateStatement = updateStatement.Substring(0, updateStatement.Length - 2);
                    updateStatement += " where ID=? ";
                    command.Parameters.Add("@ID", OleDbType.Double, 255).SourceColumn = "ID";

                    command.CommandText = updateStatement;
                    _adap.UpdateCommand = command;

                    try
                    {
                        _dt.Rows[0].SetModified();

                    }
                    catch (Exception ex)
                    {

                    }

                    var dt1 = _dt.GetChanges();

                    var updateCount = _adap.Update(_dt);

                    _dt.AcceptChanges();

                }
                catch (Exception ex)
                {
                    correctDataColumns.Remove(column1);
                    errorDataColumns.Add(column1);
                }

                this.CloseConnection();
            }

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
            return startFrom + ".xml";
        }

        public List<string> GetSheets()
        {
            OpenConnnction();
            var dt = _connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            List<string> coll = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                coll.Add(row["TABLE_NAME"].ToString());

            }

            CloseConnection();
            return coll;
        }
    }
}
