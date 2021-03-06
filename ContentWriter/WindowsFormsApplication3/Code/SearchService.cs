﻿using StatusMaker.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public class SearchService
    {
        string _folder;
        public int FoundRowIndex { get; private set; }

        public static Dictionary<string, DataTable> cachedDataTables;
        public SearchService(string folder)
        {
            _folder = folder;
        }
        public List<SearchResult> SearchText(string text)
        {
            var files = System.IO.Directory.GetFiles(_folder, "*.xlsx");
        
            if (cachedDataTables == null)
            {
                cachedDataTables = new Dictionary<string, DataTable>();

                foreach (var file in files)
                {
                    try
                    {
                        OLDBConnection connection = new OLDBConnection(file);
                        var datatable = connection.ExecuteDatatable("Select * from [Sheet1$]");
                        cachedDataTables[file] = datatable;
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(file + ":" + ex);
                    }
                }
            }

            return SearchDataTables(text);
        }

        private List<SearchResult> SearchDataTables(string text)
        {
            List<SearchResult> allResults = new List<SearchResult>();

            foreach (var key in cachedDataTables.Keys)
            {
                try
                {
                    allResults.AddRange(SearchDataTable(key, cachedDataTables[key], text));
                }
                catch (Exception)
                {

                }
            }

            return allResults;
        }

        IEnumerable<SearchResult> SearchDataTable(string file, DataTable dataTable, string searchText)
        {
            List<SearchResult> lst = new List<SearchResult>();

            foreach (DataRow row in dataTable.Rows)
            {
                string columns = string.Empty;
                for (int colIndex = 0; colIndex < row.ItemArray.Length; colIndex++)
                {
                    var indx1 = row.ItemArray[colIndex].ToString().IndexOf(searchText, 0, StringComparison.OrdinalIgnoreCase);

                    if (indx1 > -1)
                    {
                        columns += dataTable.Columns[colIndex].ColumnName + ", ";
                    }
                }
                if (!string.IsNullOrEmpty(columns))
                {
                    lst.Add(new SearchResult(file, int.Parse(row.ItemArray[0].ToString()), columns, row));
                }

            }

            return lst;
        }
    }

    public class SearchResult
    {
        public SearchResult(string file, int row, string col, DataRow dataRow)
        {
            File = file;
            Row = row;
            ColName = col;
            DataRow = dataRow;
        }
        public string File { get; set; }
        public int Row { get; set; }
        public string ColName { get; set; }
        public DataRow DataRow { get; set; }

    }
}
