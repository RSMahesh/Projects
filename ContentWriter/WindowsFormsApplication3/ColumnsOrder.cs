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
    public partial class ColumnsOrder : Form
    {
        public ColumnsOrder()
        {
            InitializeComponent();
        }

        public static DataColumnCollection _columns;
        public static DataTable dtColumnOrder;
        private void ColumnsOrder_Load(object sender, EventArgs e)
        {
            if (dtColumnOrder == null)
                dtColumnOrder = GetTable(_columns);

            dataGridView1.DataSource = dtColumnOrder;
        }

        private DataTable GetTable( DataColumnCollection columns)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(int)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Editable", typeof(bool)));
            dt.Columns.Add(new DataColumn("Height", typeof(int)));
            dt.Columns.Add(new DataColumn("Hide", typeof(bool)));
            dt.Columns[0].AutoIncrement = true;
            //dt.Columns[0].

            foreach (DataColumn col in columns)
            {
                var dr = dt.NewRow();
                dr["Name"] = col.ColumnName;
                dr["Editable"] = false;
                dr["Height"] = 40;
                dt.Rows.Add(dr);
            }

            return dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ColumnsOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                EventContainer.PublishEvent(EventPublisher.Events.ReOrder.ToString(), new EventArg(Guid.NewGuid(), null));
            }
            catch(Exception ex)
            {

            }
        }
    }
}
