using EventPublisher;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class Statistics
    {
        AppContext appContext;
        public Statistics(AppContext appContext)
        {
            this.appContext = appContext;
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
    }
}
