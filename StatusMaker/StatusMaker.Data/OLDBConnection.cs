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
            var excelDt = ExecuteDatatable("Select * from [Progress$]");

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
            var excelDt = ExecuteDatatable("Select * from [Ad-Hoc$]");

            var query = GetStatuDateFilter(statusDate);

            if (!string.IsNullOrEmpty(memberName))
            {
                query += " and Author='" + memberName + "'";
            }

            excelDt.DefaultView.RowFilter = query;

            return excelDt;
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
            var connection = Excel.GetExcelConnectionString(_excelDownloadFilePath);
            var conn = new OleDbConnection(connection);
            conn.Open();
            return conn;
        }


        private DataTable ExecuteDatatable(string sql)
        {
            using (var conn = this.GetConnnction())
            {
                var dt = new DataTable();
                var adap = new OleDbDataAdapter(sql, conn);
                adap.Fill(dt);
                adap.FillSchema(dt, SchemaType.Source);
                return dt;
            }
        }

    }
}
