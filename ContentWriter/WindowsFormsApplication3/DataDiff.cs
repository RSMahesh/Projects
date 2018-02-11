using EventPublisher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class DataDiff : Form
    {
        string backUpFile;
        public DataDiff(string backUpFile)
        {
            GetTable();
            InitializeComponent();
            this.backUpFile = backUpFile;
        }

        private void DataDiff_Load(object sender, EventArgs e)
        {
            TopMost = true;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AllowUserToAddRows = false;




            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 11F);
            this.dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowTemplate.Height = 200;


            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Red;

            dataGridView1.ColumnHeadersBorderStyle =
             DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.DataSource = dt;
            dataGridView1.ColumnHeadersHeight = 500;
            dataGridView1.Columns[2].Width =
                 dataGridView1.Columns[3].Width = 300;
            MessageBox.Show("Excel and Backup mismatch. " + Environment.NewLine + "Please import from backup if you think data is not updated.", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void AreTablesTheSame(DataTable xmlDataTable, DataTable excelDataTable)
        {
            var unique_items = new HashSet<string>();
            if (xmlDataTable.Columns.Contains("ColorCode"))
            {
                xmlDataTable.Columns.Remove("ColorCode");
            }

            if (xmlDataTable.Columns.Contains("Word Frequency"))
            {
                xmlDataTable.Columns.Remove("Word Frequency");
            }


            if (xmlDataTable.Rows.Count != excelDataTable.Rows.Count)
            {
                unique_items.Add("Rows count mismatch");
            }

            var columnsName = "";
            var columnsName12 = "";

            for (int c = 0; c < xmlDataTable.Columns.Count; c++)
            {
                columnsName += Environment.NewLine + xmlDataTable.Columns[c].ColumnName;
                columnsName12 += Environment.NewLine + excelDataTable.Columns[c].ColumnName;

                if (xmlDataTable.Columns[c].ColumnName != excelDataTable.Columns[c].ColumnName)
                {
                    var tt = "fdfd";
                }
            }



            if (xmlDataTable.Columns.Count != excelDataTable.Columns.Count)
            {
                List<string> unMatchedColumns = new List<string>();
                //if there is no value in any row for a column the column won't
                // be available in xml data file. So we need to remove that column 
                // for compariosion
                foreach (DataColumn col in excelDataTable.Columns)
                {
                    if (!xmlDataTable.Columns.Contains(col.ColumnName))
                    {
                        unMatchedColumns.Add(col.ColumnName);
                    }
                }

                foreach (var col in unMatchedColumns)
                {
                    excelDataTable.Columns.Remove(col);
                }
            }

            for (int i = 0; i < xmlDataTable.Rows.Count; i++)
            {
                for (int c = 0; c < xmlDataTable.Columns.Count; c++)
                {
                    var xmlValue = RemoveMetaCharacter(xmlDataTable.Rows[i][c].ToString());
                    var excelValue = RemoveMetaCharacter(excelDataTable.Rows[i][c].ToString());


                    if (!EqualString(xmlValue, excelValue))
                    {
                        AddRow(i + 1, xmlDataTable.Columns[c].ColumnName, xmlValue, excelValue);
                    }
                }
            }

            if (dt.Rows.Count > 0)
            {

                this.Show();
            }
        }

        private bool EqualString(string one, string two)
        {

            if (!one.Equals(two))
            {
                if (one.Length != two.Length)
                {
                    var tt = "dsad";
                }
            }
            return one.Equals(two);
        }

        private static string RemoveMetaCharacter(string text)
        {
            return text.Trim(Environment.NewLine.ToCharArray()).Trim();
        }

        string GetDiff(string text1, string text2)
        {
            List<string> diff;
            IEnumerable<string> set1 = text1.Split(' ').Distinct();
            IEnumerable<string> set2 = text2.Split(' ').Distinct();

            if (set2.Count() > set1.Count())
            {
                diff = set2.Except(set1).ToList();
            }
            else
            {
                diff = set1.Except(set2).ToList();
            }

            return string.Join(";", diff);
        }


        DataTable dt;
        private void GetTable()
        {
            dt = new DataTable("Diff");
            dt.Columns.Add("Row", typeof(int));


            dt.Columns.Add("Column");
            dt.Columns.Add("BackupData");
            dt.Columns.Add("ExcelData");
            dt.Columns.Add("Diff");


        }

        private void AddRow(int rowNumber, string columnName, string backupData, string excelData)
        {
            var row = dt.NewRow();
            row["Row"] = rowNumber;
            row["Column"] = columnName;
            row["BackupData"] = backupData;
            row["ExcelData"] = excelData;
            row["Diff"] = GetDiff(backupData, excelData);
            dt.Rows.Add(row);
        }

        private void btnOpenBackUp_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
(EventPublisher.Events.StartDataImport.ToString(), new EventArg(Guid.NewGuid(), backUpFile));
            this.Close();
        }
    }
}
