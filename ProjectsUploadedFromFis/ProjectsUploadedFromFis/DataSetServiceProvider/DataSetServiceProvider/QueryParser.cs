using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace DataSetServiceProvider
{
    public class QueryParser
    {
        bool includeAllFiled;
        DataTable _orginaldt = null;
        public QueryParser()
        {
        }
        public QueryParser(DataTable dataTable)
        {
            _orginaldt = dataTable;
        }

        private DataTable ExcuteQuery(DataTable dataTable, string sqlQuery)
        {
            sqlQuery = sqlQuery.Trim().ToUpperInvariant();
            var arrStr = sqlQuery.Split(new string[] { "SELECT " }, StringSplitOptions.None)[1]
                .Split(new string[] { " FROM " }, StringSplitOptions.None);
            var fileds = arrStr[0].Split(',');
            var whereConditionArr = arrStr[1].Split(new string[] { " WHERE " }, StringSplitOptions.None);
            string whereCondition = "";
            if (whereConditionArr.Length > 1)
            {
                whereCondition = whereConditionArr[1];
                whereCondition = ParesWhereString(whereCondition);
            }

            return GetFilteredData(dataTable, fileds, whereCondition);
        }

        public DataTable ExcuteDataTable(DataTable dataTable, string sqlQuery)
        {
            return ExcuteQuery(dataTable, sqlQuery);
        }


        public IDataReader ExecuteReader(DataTable dataTable, string sqlQuery)
        {
            var dt = ExcuteQuery(dataTable, sqlQuery);
            return new DataReader(dt);
        }

        public DataTable LoadDataTableFromFile(string sqlQuery, string rootPath)
        {
            sqlQuery = sqlQuery.Trim().ToUpperInvariant();
            sqlQuery = sqlQuery.Replace("[", "").Replace("]", "");

            var arrStr = sqlQuery.Split(new string[] { "SELECT " }, StringSplitOptions.None)[1]
                .Split(new string[] { " FROM " }, StringSplitOptions.None);
            var tableName = arrStr[1].Trim().Split(' ')[0];

            DataTable dt = new DataTable();
            string filePath = rootPath + tableName + ".xml";
            using (StreamReader sr = new StreamReader(filePath))
            {
                dt.ReadXml(sr);
            }
            return dt;

        }


        private DataTable GetFilteredData(DataTable dataTable, string[] fileds, string whereCondition)
        {
            DataTable resultTable = CreateDataTable(dataTable, fileds);
            var drs = GetFilteredDataRows(dataTable, whereCondition);

            if (includeAllFiled)
            {
                var cols = drs[0].Table.Columns;
                for (int i = 0; i < drs.Length; i++)
                {
                    var dr = resultTable.NewRow();
                    for (int j = 0; j < cols.Count; j++)
                    {
                        dr[cols[j].ColumnName] = drs[i][cols[j].ColumnName];
                    }
                    resultTable.Rows.Add(dr);
                }
            }

            else
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    var dr = resultTable.NewRow();
                    for (int j = 0; j < fileds.Length; j++)
                    {
                        dr[fileds[j]] = drs[i][fileds[j]];
                    }
                    resultTable.Rows.Add(dr);
                }

            }
            return resultTable;
        }

        private string ParesWhereString(string whereCondition)
        {
            return whereCondition;
            //var condtions = whereCondition.Split(new string[] { " AND " }, StringSplitOptions.None);

            //foreach (string cond in condtions)
            //{
            //    var conContaint = arrStr[0].Split('=');
            //}
        }

        private DataRow[] GetFilteredDataRows(DataTable dt, string whereCondition)
        {
            return dt.Select(whereCondition);
        }

        private DataTable CreateDataTable(DataTable dataTable, string[] fileds)
        {
            DataTable dt = new DataTable();
            includeAllFiled = fileds.Contains("*");

            if (includeAllFiled)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var sourCol = dataTable.Columns[i];
                    var dtCol = new DataColumn(sourCol.ColumnName, sourCol.DataType);
                    dt.Columns.Add(dtCol);
                }
            }
            else
            {
                for (int i = 0; i < fileds.Length; i++)
                {
                    var sourCol = dataTable.Columns[fileds[i]];
                    var dtCol = new DataColumn(sourCol.ColumnName, sourCol.DataType);
                    dt.Columns.Add(dtCol);
                }
            }

            return dt;
        }

        public DataTable  Insert(string sql)
        {
            sql = sql.Trim();
            List<string> fields, values;
            GetInsertFileds(sql,out fields,out values);
            var dr = _orginaldt.NewRow();
            for (int i = 0; i < fields.Count; i++)
            {
                dr[fields[i]] = values[i];
            }
            _orginaldt.Rows.Add(dr);
            return _orginaldt ;
        }

        private void GetInsertFileds(string sql, out List<string> fields, out List<string> values)
        {
            fields = new List<string>();
            values = new List<string>();
            int startIndx = sql.IndexOf('(', 0);
            int endIndx = sql.IndexOf(')', startIndx);
            var _fields = sql.Substring(startIndx + 1, endIndx - startIndx - 1).Split(',');

            startIndx = sql.IndexOf('(', endIndx);
            endIndx = sql.IndexOf(')', startIndx);
            var _values = sql.Substring(startIndx + 1, endIndx - startIndx-1).Split(new string[] { ",'" }, StringSplitOptions.None);
            var morevalues = new List<string>();

            for (int i = 0; i < _values.Length; i++)
            {
                var arr = _values[i].Split(new string[] { "'," }, StringSplitOptions.None);
                morevalues.Add(arr[0]);
                if (arr.Length > 1)
                {
                    morevalues.Add(arr[1]);
                }
            }
            fields.AddRange(_fields);
            values.AddRange(_values);
        }
    }
}
