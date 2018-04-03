using EventPublisher;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class Statistics
    {
        AppContext appContext;
        Styler styler;
        List<RowInfo> rowsInfo = new List<RowInfo>();
        public Statistics(AppContext appContext, Styler styler)
        {
            this.appContext = appContext;
            this.styler = styler;
        }
        public void ShowStatistics(EventArg arg)
        {
            Dictionary<int, int> statics = new Dictionary<int, int>();
            var highLighted = 0;
            foreach (DataGridViewRow row in appContext.dataGridView.Rows)
            {

                if (row.DefaultCellStyle.BackColor == appContext.Theme.HighlightedRowColor)
                {
                    highLighted++;
                }

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!(appContext.dataGridView.Columns[cell.ColumnIndex] is DataGridViewImageColumn))
                    {
                        if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString().Trim()))
                        {

                            if (statics.ContainsKey(cell.ColumnIndex))
                            {
                                statics[cell.ColumnIndex]++;
                            }
                            else
                            {
                                statics[cell.ColumnIndex] = 1;
                            }
                        }
                    }
                }
            }

            var message = Environment.NewLine + "HighLighted : " + highLighted.ToString();

            foreach (DataGridViewColumn col in appContext.dataGridView.Columns)
            {
                if (!(col is DataGridViewImageColumn))
                {
                    message += statics.ContainsKey(col.Index) ? Environment.NewLine + col.Name + ": " + statics[col.Index] :
                        Environment.NewLine + col.Name + " : 0";
                }
            }

            MessageBox.Show(message);
        }

        public  void SetenceCountInBullets(EventArg arg)
        {
            int threshold = 0;
            var columsToCheck = GetColumsToCheckForSentenceCount();
            var rowsWithLessSentenceCount = 0;
            var fixedCell = 0;

            foreach (DataGridViewRow row in appContext.dataGridView.Rows)
            {
               styler.UnHighlightRow(int.Parse(row.Cells["ID"].Value.ToString()));

                foreach (var columnName in columsToCheck)
                {
                    var cell = row.Cells[columnName];

                    if (cell.Value.ToString().StartsWith("Dimensions:"))
                    {
                        continue;
                    }

                    var count = GetSetenceCount(cell.Value.ToString());

                    if (count == 1 && cell.Value.ToString().EndsWith("."))
                    {
                        cell.Value = cell.Value.ToString().TrimEnd('.');
                        fixedCell++;
                        continue;
                    }

                    if (count > threshold)
                    {
                        styler.HighLightRow(int.Parse(row.Cells["ID"].Value.ToString()));
                        cell.Style.BackColor = Color.AntiqueWhite;
                        rowsWithLessSentenceCount++;
                    }
                }

            }

            var msg = "Bullets with more then 0 sentence are :" + rowsWithLessSentenceCount;
            msg += Environment.NewLine + "Fixed Bullets : " + fixedCell;

            MessageBox.Show(msg);
        }

        public  void SetenceCountInDescription(EventArg arg)
        {
            var rowsWithLessSentenceCount = 0;
            foreach (DataGridViewRow row in appContext.dataGridView.Rows)
            {
                styler.UnHighlightRow(int.Parse(row.Cells["ID"].Value.ToString()));


                var count = GetSetenceCount(row.Cells["Description"].Value.ToString());

                if (count < 3)
                {
                    styler.HighLightRow(int.Parse(row.Cells["ID"].Value.ToString()));
                    rowsWithLessSentenceCount++;
                }

            }

            MessageBox.Show("Description with less then 3 sentence are :" + rowsWithLessSentenceCount);
        }
        
        private IEnumerable<string> GetColumsToCheckForSentenceCount()
        {
            List<string> lst = new List<string>();

            foreach (DataGridViewColumn column in appContext.dataGridView.Columns)
            {
                if (column.Name.StartsWith("Bullet", StringComparison.OrdinalIgnoreCase))
                {
                    lst.Add(column.Name);
                }
            }

            return lst.AsEnumerable();
        }

        private int GetSetenceCount(string text)
        {
            var c = text.Count(f => f == '.');
            return c;
        }
   
    }
}
