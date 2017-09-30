using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace StatusMaker.Data
{
    public class OLDBConnection : IOLDBConnection
    {

#pragma warning disable CS0414 // The field 'OLDBConnection.disposed' is assigned but its value is never used
        private bool disposed = false;
#pragma warning restore CS0414 // The field 'OLDBConnection.disposed' is assigned but its value is never used

        private string _excelDownloadFilePath;

        public OLDBConnection(string excelDownloadFilePath)
        {
            _excelDownloadFilePath = excelDownloadFilePath;
        }

        public DataTable GetInProgessDataTable(DateTime statusDate, string memberName)
        {
            var excelDt = ExecuteDatatable("Select * from [Sheet1$]");

            var query = GetStatuDateFilter(statusDate);

            if (!string.IsNullOrEmpty(memberName))
            {
                query += " and (Author='" + memberName + "' or Tester='" + memberName
                         + "' or Review='" + memberName + "')";
            }
            var dataRows = excelDt.Select(query);

         
            if (dataRows.Length < 1)
            {
                throw new NoDataFoundException();
            }

            return dataRows.CopyToDataTable();
        }

        public DataTable GetAhocDataTable(DateTime statusDate, string memberName)
        {
            //var excelDt = ExecuteDatatable("Select * from [Ad-Hoc$]");

            //var query = GetStatuDateFilter(statusDate);

            //if (!string.IsNullOrEmpty(memberName))
            //{
            //    query += " and Author='" + memberName + "'";
            //}

            //excelDt.DefaultView.RowFilter = query;

            return new DataTable();
        }

        public IEnumerable<DataRow> GetInProgessRowsExcludingRegression(DateTime statusDate, string memberName)
        {
            var excelDt = GetInProgessDataTable(statusDate, memberName);

            var data = excelDt.Select("Regression='No' and Category='In Progress'");

            return data;
        }

        public IEnumerable<DataRow> GetMergedToEpicRows(DateTime statusDate, string memberName)
        {
            var excelDt = GetInProgessDataTable(statusDate, memberName);

            var data = excelDt.Select("Category='Merged to Epic'");

            return data;
        }

        public IEnumerable<DataRow> GetInProgessRegressionRows(DateTime statusDate, string memberName)
        {
            var excelDt = GetInProgessDataTable(statusDate, memberName);

            var data = excelDt.Select("Regression='Yes' and Category='In Progress'");

            return data;
        }

        private string GetStatuDateFilter(DateTime statusDate)
        {
            return " [Status Date] = '" + statusDate.ToString("MM/dd/yyyy") + "' ";
        }

        private OleDbConnection GetConnnction()
        {
            var connection = Excel.GetExcelConnectionString(@"C:\Users\mahesh.bailwal\Documents\11.xlsx");
            var conn = new OleDbConnection(connection);
            conn.Open();
            return conn;
        }


        private DataTable ExecuteDatatable(string sql)
        {
            lock (this)
            {
                using (var conn = this.GetConnnction())
                {
                    var dt = new DataTable();
                    dt.TableName = "Sheet1";
                    var adap = new OleDbDataAdapter(sql, conn);

                    // adap.SelectCommand = new OleDbCommand(sql,conn); // cmd1 is your SELECT command
                    //  OleDbCommandBuilder cb = new OleDbCommandBuilder(adap);

                    adap.UpdateCommand = new OleDbCommand("UPDATE [Sheet1$] SET SKU = ? where ID=?", conn);

                    adap.UpdateCommand.Parameters.Add("@SKU", OleDbType.Char, 255).SourceColumn = "SKU";

                    adap.UpdateCommand.Parameters.Add("@ID", OleDbType.Char, 255).SourceColumn = "ID";


                    // adp1.Update(dt);


                    DataColumn column = new DataColumn("ID");
                    column.DataType = System.Type.GetType("System.Int32");
                    column.AutoIncrement = true;
                    column.AutoIncrementSeed = 1;
                    column.AutoIncrementStep = 1;
                    column.Unique = true;

                    // dt.Columns.Add(column);



                    adap.Fill(dt);

                    //adap.FillSchema(dt, SchemaType.Source);
                    dt.Rows[2][1] = "444";

                     //dt.AcceptChanges();


                    // adap.UpdateCommand = cb.GetUpdateCommand();

                    adap.Update(dt);
                    return dt;
                }
            }
        }

        //private DataTable Save(DataTable dt)
        //{
        //    using (var conn = this.GetConnnction())
        //    {
        //        var adap = new OleDbDataAdapter(sql, conn);

        //        DataColumn column = new DataColumn("ID");
        //        column.DataType = System.Type.GetType("System.Int32");
        //        column.AutoIncrement = true;
        //        column.AutoIncrementSeed = 1;
        //        column.AutoIncrementStep = 1;

        //        dt.Columns.Add(column);

        //        adap.Fill(dt);
        //        adap.FillSchema(dt, SchemaType.Source);


        //        return dt;
        //    }
        //}

    }
}
