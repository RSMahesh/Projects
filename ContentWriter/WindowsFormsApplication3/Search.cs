using StatusMaker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Search : Form
    {
        Form _mdiPArent;
        ImageLoader imageLoader;
        public Search(Form MDIpARENT)
        {
            InitializeComponent();
            this.splitContainer1.SplitterDistance = 800;
            _mdiPArent = MDIpARENT;
            this.MdiParent = _mdiPArent;
            this.Resize += Search_Resize;
        }
        private void Search_Resize(object sender, EventArgs e)
        {
            this.splitContainer1.Width = this.Width - 30;
            this.splitContainer1.Height = this.Height - 80;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ShowResult(txtSerchText.Text);
            Cursor = Cursors.Default;
        }

        private void OpenFile()
        {
            var filePath = dataGridView1.CurrentRow.Cells["FilePath"].Value.ToString();

            var form1 = new Form1(filePath, false, true);
            form1.Show();
        }
        public void ShowResult(string searchText)
        {
            txtSerchText.Text = searchText;
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["BackUpFolder"]))
            {
                MessageBox.Show("BackUpFolder not defeined in config");
                return;
            }

            var search = new SearchService(ConfigurationManager.AppSettings["BackUpFolder"]);
            var results = search.SearchText(searchText);


            if (results.Any())
            {

                dataGridView1.Columns.Clear();
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
              
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridView1.ColumnHeadersHeight = 30;
                dataGridView1.EnableHeadersVisualStyles = false;
                

                dataGridView1.RowHeadersVisible = false;

                dataGridView1.RowTemplate.Height = Constants.ImageIconSize;
                dataGridView1.DataSource = GetTable(results);
                SetColumnWidth();
                dataGridView1.Columns["FilePath"].Visible = false;
                dataGridView1.Columns["File"].Width = 100;
                dataGridView1.Columns[0].Width = 50;
                ToggleMetaInfo();

                ImageLoader imgeLoader = new ImageLoader(dataGridView1);
                imgeLoader.AddImageColumn();
                imgeLoader.LoadImageInCell();
                DisplaySearchedInFiles();

            }
            else
            {
                MessageBox.Show("No found");
            }

            AddButton();
        }

        private void SetColumnWidth()
        {
           foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 200;
            }
        }

        private string GetImageUrl(DataRow dataRow)
        {
            foreach (var item in dataRow.ItemArray)
            {
                if (item != null && ImageLoader.IsValidImageUrl(item.ToString()))
                {
                    return item.ToString();
                }
            }
            return string.Empty;
        }

        private void DisplaySearchedInFiles()
        {
            //listBox1.Items.Clear();
            foreach (var key in SearchService.cachedDataTables.Keys)
            {
                // listBox1.Items.Add(key);
            }
        }
        private void ShowData()
        {
            var file = dataGridView1.CurrentRow.Cells["FilePath"].Value.ToString();
            showDataInGrid(file);
            imageLoader = new ImageLoader(dataGridView2, file);
            imageLoader.AddImageColumn();
            imageLoader.LoadImageInCell();
            //groupBox1.Text = Path.GetFileName(file);
        }
        private void showDataInGrid(string file)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.RowTemplate.Height = Constants.ImageIconSize;
            dataGridView2.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnHeadersVisible = false;

            OLDBConnection12 connection = new OLDBConnection12(file);
            var sourceTable = connection.ExecuteDatatable("Select * from [Sheet1$]");
            var newTable = sourceTable.Clone();
            var rowIndex = int.Parse(dataGridView1.CurrentRow.Cells["Row"].Value.ToString()) - 1;
            var row = sourceTable.Rows[rowIndex];
            newTable.ImportRow(row);
            sourceTable = FlipDataSet(newTable);

            dataGridView2.DataSource = sourceTable;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect;
         //  dataGridView2.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView2.Columns[0].Width = 100;
            dataGridView2.Columns[1].Width = 250;
            dataGridView2.Columns[0].DefaultCellStyle.BackColor = SystemColors.Control;
           // dataGridView2.Columns[0].DefaultCellStyle.ForeColor = SystemColors.Control;
            dataGridView2.Columns[0].ReadOnly = true;
           
        }

        public DataTable FlipDataSet(DataTable dt)
        {
            DataTable table = new DataTable();

            for (int i = 0; i <= dt.Rows.Count; i++)
            { table.Columns.Add(Convert.ToString(i)); }

            DataRow r;
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                r = table.NewRow();
                r[0] = dt.Columns[k].ToString();
                for (int j = 1; j <= dt.Rows.Count; j++)
                { r[j] = dt.Rows[j - 1][k]; }
                table.Rows.Add(r);
            }

            return table;
        }

        private List<KeyValuePair<string, string[]>> CustomColumns()
        {
            var list = new List<KeyValuePair<string, string[]>>();
            list.Add(new KeyValuePair<string, string[]>("Title", new[] { "Title", "Name" }));
            list.Add(new KeyValuePair<string, string[]>("Description", new[] { "Description" }));
            list.Add(new KeyValuePair<string, string[]>("Bullet 1", new[] { "Bullet 1", "Bullet1" }));
            list.Add(new KeyValuePair<string, string[]>("Bullet 2", new[] { "Bullet 2", "Bullet2" }));
            list.Add(new KeyValuePair<string, string[]>("Bullet 3", new[] { "Bullet 3", "Bullet3" }));

            return list;
        }

        private DataTable GetTable(IEnumerable<SearchResult> results)
        {
            DataTable dt = new DataTable("SearchResults");
            dt.Columns.Add("S.No", typeof(int));
            dt.Columns[0].AutoIncrement = true;
            var customColumns = CustomColumns();
            foreach (var item in customColumns)
            {
                dt.Columns.Add(item.Key);
            }

            dt.Columns.Add("File");
            dt.Columns.Add("Row", typeof(int));
            dt.Columns.Add("Col");
            dt.Columns.Add("FilePath");
            dt.Columns.Add("ImageUrl");

            foreach (var result in results)
            {
                var row = dt.NewRow();
                row["File"] = Path.GetFileName(result.File);
                row["Row"] = result.Row;
                row["Col"] = result.ColName;
                row["FilePath"] = result.File;
                row["ImageUrl"] = GetImageUrl(result.DataRow);


                foreach (var item in customColumns)
                {
                    LoadDataFromDataRow(result.DataRow, row, item);
                }

                dt.Rows.Add(row);
            }

            return dt;

        }

        private void LoadDataFromDataRow(DataRow sourceDataRow, DataRow destinationDataRow, KeyValuePair<string, string[]> customColumn)
        {
            foreach (var colName in customColumn.Value)
            {
                foreach (DataColumn column in sourceDataRow.Table.Columns)
                {
                    if (column.ColumnName.Trim().Equals(colName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        destinationDataRow[customColumn.Key] = sourceDataRow[column.ColumnName];
                        return;
                    }
                }
            }
        }
        private string GetSimilarColumnInExcelRow(string columnName, DataRow dataRow)
        {
            if (dataRow.Table.Columns.Contains(columnName))
            {
                return columnName;
            }

            foreach (DataColumn column in dataRow.Table.Columns)
            {
                if (column.ColumnName.IndexOf(columnName, 0, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    return column.ColumnName;
                }
            }

            return string.Empty;
        }

        private void Search_Load(object sender, EventArgs e)
        {
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void AddButton()
        {
            var dgvButton = new DataGridViewButtonColumn();
            dgvButton.FlatStyle = FlatStyle.Flat;
            dgvButton.HeaderText = "Button";
            dgvButton.Name = "Button";
            dgvButton.UseColumnTextForButtonValue = true;
            dgvButton.Text = "Open";
            dgvButton.Name = "Open File";
            dgvButton.Width = 60;
            dataGridView1.Columns.Add(dgvButton);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                OpenFile();
            }
        }

        private void chkShowMetaInfo_CheckedChanged(object sender, EventArgs e)
        {
            ToggleMetaInfo();
        }

        private void ToggleMetaInfo()
        {
            dataGridView1.Columns["Row"].Visible = chkShowMetaInfo.CheckState == CheckState.Checked;
            dataGridView1.Columns["col"].Visible = chkShowMetaInfo.CheckState == CheckState.Checked;
        }
    }
}
