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
    public partial class GridDataDisplayer : Form
    {
        public GridDataDisplayer(DataView dataView)
        {
            InitializeComponent();
            SetGridProperties();
            dataGridView1.DataSource = dataView;
            SetGridProperties();
        }

        private void GridDataDisplayer_Load(object sender, EventArgs e)
        {
           
        }

        private void SetGridProperties()
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.RowTemplate.Height = Constants.ImageIconSize;
            dataGridView1.RowTemplate.DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 30;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
