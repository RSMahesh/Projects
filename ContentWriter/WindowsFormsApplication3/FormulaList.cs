using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class FormulaList : Form
    {
        Regex regex = new Regex("{.*?}");
        DataGridView _formulaTargetgridView;
        public string CurrentFormula;
        public FormulaList(DataGridView formulaTargetgridView)
        {
            InitializeComponent();
            _formulaTargetgridView = formulaTargetgridView;
        }

        private void FormulaList_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
    

            dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.ColumnHeadersBorderStyle =
  DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =  new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           // return;

            dataGridView1.DataSource = GetTable();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

        }

        private DataTable GetTable()
        {
            if (File.Exists(GetFile()))
            {
                return ReadFromXml(GetFile());
            }

            DataTable dt = new DataTable("formula");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns[0].AutoIncrement = true;

            dt.Columns.Add("Name");
            dt.Columns.Add("Formula");

            dt.Columns.Add("Discription");



            return dt;

        }

        private DataTable ReadFromXml(string file)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(file, XmlReadMode.ReadSchema);
            return dataSet.Tables[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ReadOnly)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                btnSave.Text = "Save";
            }
            else
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                Save();
                btnSave.Text = "Edit";
            }

        }

        private void Save()
        {
            var dt = (DataTable)dataGridView1.DataSource;

            dt.WriteXml(GetFile(), XmlWriteMode.WriteSchema);

        }

        private string GetFile()
        {
            var dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            return Path.Combine(dir, "formula.xml");
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(_formulaTargetgridView == null || !dataGridView1.ReadOnly)
            {
                return;
            }
            CurrentFormula = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
           if(ValidateFormula(CurrentFormula))
            {
                this.Close();
            }
        }


        bool ValidateFormula(string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                return true;
            }

            var matches = regex.Matches(formula);

            foreach (Match match in matches)
            {
                var columnName = match.Value.Replace("{", "").Replace("}", "");

                if (!DoesColumnExist(columnName))
                {
                    MessageBox.Show("Column :" + columnName + " does not exist." +
                       Environment.NewLine +
                       "Please make sure column name does not conatin space at start and end");
                    return false;
                }

            }

            return true;
        }



        private bool DoesColumnExist(string colName)
        {
            foreach (DataGridViewColumn col in _formulaTargetgridView.Columns)
            {
                if (col.Name.Equals(colName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
