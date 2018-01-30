using StatusMaker.Data;
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
        public SearchService(string folder)
        {
            _folder = folder;
        }
        public List<SearchResult> SearchText(string text)
        {
            var files = System.IO.Directory.GetFiles(_folder, "*.xlsx");

            List<SearchResult> allResults = new List<SearchResult>();

            foreach (var file in files)
            {
                try
                {
                    OLDBConnection12 connection = new OLDBConnection12(file);
                    var datatable = connection.ExecuteDatatable("Select * from [Sheet1$]");
                    allResults.AddRange(SearchDataTable(file, datatable, text));
                }
                catch(Exception ex)
                {
                  // MessageBox.Show(file + ":" + ex);
                }
            }

            return allResults;
        }

        IEnumerable<SearchResult> SearchDataTable(string file, DataTable dataTable, string searchText)
        {
            List<SearchResult> lst = new List<SearchResult>();

            foreach (DataRow row in dataTable.Rows)
            {
                for (int colIndex = 0; colIndex < row.ItemArray.Length; colIndex++)
                {
                    var indx1 = row.ItemArray[colIndex].ToString().IndexOf(searchText, 0);

                    if (indx1 > -1)
                    {
                        lst.Add(new SearchResult(file, int.Parse(row.ItemArray[0].ToString()), colIndex));
                    }
                }

                //var allRowData = string.Join("|", row.ItemArray);

                //var indx = allRowData.IndexOf(searchText, 0);

                //if (indx > -1)
                //{
                //    return int.Parse(row.ItemArray[0].ToString());
                //}
            }
            return lst;
        }
    }

    public class SearchResult
    {
        public SearchResult(string file, int row, int col)
        {
            File = file;
            Row = row;
            Col = col;
        }
        public string File { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
