using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CompDLL;

namespace IDE
{
    public partial class Form3 : Form
    {
        CompileException _ex;
        DataTable tbl;
        public Form3(CompileException ex)
        {
            _ex = ex;
            tbl = new DataTable();
            InitializeComponent();
        }

        private void CreateTable()
        {
            tbl.Columns.Add(new DataColumn("SNo"));
            tbl.Columns.Add(new DataColumn("Discription"));
            tbl.Columns.Add(new DataColumn("File"));
            tbl.Columns.Add(new DataColumn("Line"));
            tbl.Columns.Add(new DataColumn("Column"));
            tbl.Columns.Add(new DataColumn("Project"));
        }

        private void FillTable()
        {
            DataRow dr = tbl.NewRow();
            dr["SNo"] = tbl.Rows.Count + 1;
            dr["Discription"] = _ex.Message;
            dr["File"] = _ex.File;
            dr["Line"] = _ex.LineNumber;
            dr["Column"] = "";
            dr["Project"] = "";
            tbl.Rows.Add(dr);
            dr = tbl.NewRow();
            dr["SNo"] = tbl.Rows.Count + 1;
            dr["Discription"] = _ex.Message;
            dr["File"] = _ex.File;
            dr["Line"] = _ex.LineNumber;
            dr["Column"] = "";
            dr["Project"] = "";
            tbl.Rows.Add(dr);
            dataGridView1.DataSource = tbl;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            CreateTable();
            FillTable();
        }

    
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
         string  file =   dataGridView1[2, e.RowIndex].Value as string;
            int lineNumber =Convert.ToInt32(dataGridView1[3, e.RowIndex].Value);
         MDIParent1 parent = (MDIParent1)this.ParentForm;
         parent.GoToLineNumber(file, lineNumber);

        }


    }
}