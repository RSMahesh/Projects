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
            _mdiPArent = MDIpARENT;
            this.MdiParent = _mdiPArent;
            //this.TopMost = true;
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
                // MessageBox.Show("Found :" + results.Count.ToString());

                dataGridView1.Columns.Clear();
              dataGridView1.RowTemplate.Height   = Constants.ImageIconSize;
                dataGridView1.DataSource = GetTable(results);
                dataGridView1.Columns["FilePath"].Visible = false;
                dataGridView1.Columns["File"].Width = 350;
                ImageLoader imgeLoader = new ImageLoader(dataGridView1);
                imgeLoader.AddImageColumn();
                imgeLoader.LoadImageInCell();

             //   this.MdiParent.LayoutMdi(MdiLayout.TileHorizontal);

                if (results.Count < 2)
                {
                    checkBox1.Checked = true;
                }

                DisplaySearchedInFiles();

            }
            else
            {
                MessageBox.Show("No found");
            }

            AddButton();
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

            OLDBConnection12 connection = new OLDBConnection12(file);
            var sourceTable = connection.ExecuteDatatable("Select * from [Sheet1$]");
            dataGridView2.DataSource = sourceTable;
            sourceTable.DefaultView.RowFilter = "ID=" + int.Parse(dataGridView1.CurrentRow.Cells["Row"].Value.ToString());
            dataGridView2.Columns[0].Width = 30;
        }

        private DataTable GetTable(IEnumerable<SearchResult> results)
        {

            DataTable dt = new DataTable("SearchResults");

            dt.Columns.Add("S.No", typeof(int));
            dt.Columns[0].AutoIncrement = true;

            dt.Columns.Add("File");
            dt.Columns.Add("Row", typeof(int));
            dt.Columns.Add("Col");
            dt.Columns.Add("FilePath");
            dt.Columns.Add("ImageUrl");
            dt.Columns.Add("Title")
            dt.Columns.Add("Description");


            foreach (var result in results)
            {
                var row = dt.NewRow();
                row["File"] = Path.GetFileName(result.File);
                row["Row"] = result.Row;
                row["Col"] = result.ColName;
                row["FilePath"] = result.File;
                row["ImageUrl"] = GetImageUrl(result.DataRow);
                dt.Rows.Add(row);
            }

            return dt;

        }

        private void Search_Load(object sender, EventArgs e)
        {
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
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
            dataGridView1.Columns.Add(dgvButton);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                OpenFile();
            }
        }

        int groupBoxTop = 0;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Toggle();
        }

        private void Toggle()
        {
            if (checkBox1.Checked)
            {
                //  dataGridView1.Visible = false;
                //  groupBoxTop = groupBox1.Top;
                //  groupBox1.Top = dataGridView1.Top;
            }
            else
            {
                //groupBox1.Top = groupBoxTop;
            }
        }
    }
}
