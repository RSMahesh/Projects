using EventPublisher;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class Styler
    {
        const string HighLighted = "highlighted";
        AppContext appContext;
        List<RowInfo> rowsInfo = new List<RowInfo>();
        public Styler(AppContext appContext)
        {
            this.appContext = appContext;
        }
        public void HighLightRow(int rowId)
        {
            var rowIndex = GetRowWithId(rowId);

            if (appContext.dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor == appContext.Theme.HighlightedRowColor)
            {
                UnHighlightRow(rowId);
            }
            else
            {
                HighLightRowColor(rowIndex);
                RowInfo rowInfo = new RowInfo();
                rowInfo.Highlighted = true;
                rowInfo.RowId = rowId;
                rowsInfo.Add(rowInfo);
            }
        }

        public void UnHighlightRow(int rowId)
        {
            var rowIndex = GetRowWithId(rowId);
            appContext.dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = appContext.Theme.BackGroundColor;
            appContext.dataGridView.Rows[rowIndex].Tag = string.Empty;
            appContext.dataGridView.Rows[rowIndex].Cells["ColorCode"].Value = string.Empty;
            rowsInfo.Remove(rowsInfo.FirstOrDefault(x => x.RowId == rowId));
        }

        public void SaveRowInfo()
        {
            if (!rowsInfo.Any())
            {
                // return;
            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(rowsInfo);
            var filePath = Path.Combine(Utility.FreeLanceAppDataFolder, Path.GetFileNameWithoutExtension(appContext.ExcelFilePath) + "_style.txt");
            //  MessageBox.Show(serializedResult);
            File.WriteAllText(filePath, serializedResult);

        }

        public void LoadHighLightRowsInfo()
        {
            var serializer = new JavaScriptSerializer();
            var filePath = Path.Combine(Utility.FreeLanceAppDataFolder, Path.GetFileNameWithoutExtension(appContext.ExcelFilePath) + "_style.txt");

            if (File.Exists(filePath))
            {
                rowsInfo = serializer.Deserialize<List<RowInfo>>(File.ReadAllText(filePath));
                HighLightRows();
            }
        }
        public void HighLightRows()
        {
            foreach (var info in rowsInfo)
            {
                var rowIndex = GetRowWithId(info.RowId);
                HighLightRowColor(rowIndex);
            }
        }
        public void LoadTheme(EventArg arg)
        {
            appContext.Theme = (Theme)arg.Arg;
            appContext.dataGridView.RowTemplate.DefaultCellStyle.BackColor = appContext.Theme.BackGroundColor;
            appContext.dataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 11F);
            appContext.dataGridView.RowTemplate.DefaultCellStyle.ForeColor = appContext.Theme.ForeColor;
            appContext.dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = appContext.Theme.SelectionBackColor;
            appContext.dataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = appContext.Theme.SelectionForeColor;

            foreach (DataGridViewRow row in appContext.dataGridView.Rows)
            {
                row.DefaultCellStyle.BackColor = appContext.Theme.BackGroundColor;
                row.DefaultCellStyle.ForeColor = appContext.Theme.ForeColor;

                if (row.Tag == HighLighted)
                {
                    row.DefaultCellStyle.BackColor = appContext.Theme.HighlightedRowColor;
                }
            }

            appContext.wpfRichTextBox.BackgroundColor = Utility.ToMediaColor(appContext.Theme.BackGroundColor);
            appContext.wpfRichTextBox.BorderColor = Utility.ToMediaColor(appContext.Theme.CurrentCellBorderColor);
            appContext.wpfRichTextBox.ForegroundColor = Utility.ToMediaColor(appContext.Theme.ForeColor);
        }
        public void ChangeBackGroundColor(EventArg arg)
        {
            var bgColor = (Color)arg.Arg;
            appContext.Theme.BackGroundColor = bgColor;
            appContext.Theme.HighlightedRowColor = appContext.Theme.SelectionBackColor = appContext.Theme.CurrentRowColor = bgColor;
            Theme.SavedBackGroundColor = bgColor;

            LoadTheme(new EventArg(appContext.Theme));
        }
        private void HighLightRowColor(int rowIndex)
        {
            appContext.dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = appContext.Theme.HighlightedRowColor;
            appContext.dataGridView.Rows[rowIndex].Tag = HighLighted;
            appContext.dataGridView.Rows[rowIndex].Cells["ColorCode"].Value = appContext.Theme.HighlightedRowColor.ToString();
        }
        private int GetRowWithId(int rowId)
        {
            foreach (DataGridViewRow row in appContext.dataGridView.Rows)
            {
                if (int.Parse(row.Cells["ID"].Value.ToString()) == rowId)
                {
                    return row.Index;
                }
            }

            return -1;
        }
    }
}
