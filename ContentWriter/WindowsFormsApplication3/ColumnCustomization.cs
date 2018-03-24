using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    partial class ColumnCustomization : Form
    {
        AppContext appContext;

        public ColumnCustomization(AppContext appContext)
        {
            this.appContext = appContext;

            InitializeComponent();
        }

        private void FindText_Load(object sender, EventArgs e)
        {
        }

        private void ColumnCustomization_Load(object sender, EventArgs e)
        {
            LoadColumns();
            checkedListBox1.ItemCheck += CheckedListBox1_ItemCheck;
        }

        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ShowHide(e.Index, e.NewValue == CheckState.Checked);
        }

        private void ShowHide(int index, bool value)
        {
            appContext.dataGridView.Columns[checkedListBox1.Items[index].ToString()].Visible = value;
            SaveToFile();
        }

        private void LoadColumns()
        {
            foreach (DataGridViewColumn col in appContext.dataGridView.Columns)
            {
                checkedListBox1.Items.Add(col.Name);
                checkedListBox1.SetItemChecked(checkedListBox1.Items.Count - 1, col.Visible);
            }
        }

        private string DataFile
        {
            get
            {
                return Path.Combine(Utility.FreeLanceAppDataFolder, Path.GetFileNameWithoutExtension(appContext.ExcelFilePath) + "_columnInformation.txt");
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var checkedState = checkedListBox1.GetItemCheckState(checkedListBox1.SelectedIndex) == CheckState.Checked;
            checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, !checkedState);
        }

        private void SaveToFile()
        {
            List<string> cols = new List<string>();
            foreach (DataGridViewColumn col in appContext.dataGridView.Columns)
            {
                if (!col.Visible)
                {
                    cols.Add(col.Name);
                }
            }

            File.WriteAllText(DataFile, string.Join("|", cols));
        }

        public void ShowHideGridColums()
        {
            if (File.Exists(DataFile))
            {
                var str = File.ReadAllText(DataFile);
                var columnsName = str.Split('|');

                foreach (var colName in columnsName)
                {
                    appContext.dataGridView.Columns[colName].Visible = false;
                }
            }
        }
    }
}
