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
    public partial class Filter : Form
    {
      
        DataView _dataView;
        public Filter(DataView dataView)
        {
            _dataView = dataView;
            InitializeComponent();
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            cmbOpreator.SelectedIndex = 0;
            foreach(DataColumn col in _dataView.Table.Columns)
            {
                cmbFields.Items.Add(col.ColumnName);
            }
        }

        private  void FilterData()
        {
            if (txtFilterValue.Text.Trim() == string.Empty)
            {
                _dataView.RowFilter = "";
                return;
            }

            _dataView.RowFilter = getFilterData(txtFilterValue.Text);

        }

        private string  getFilterData(string filter)
        {
         filter =   filter.Replace(Environment.NewLine, "");
          var arr =  filter.Split(',');
            var query = "";

            string opreator = "";

            if(cmbOpreator.SelectedItem.ToString() == "Equal")
            {
                opreator = "=";

            }
            else
            {
                opreator = "<>";
            }


            foreach (string item in arr)
            {

                if (cmbOpreator.SelectedItem.ToString() == "Equal")
                {
                    query += " or [" + cmbFields.SelectedItem + "] ='" + item + "'";

                }
                else
                {
                    query += "and [" + cmbFields.SelectedItem + "] <>'" + item + "' ";


                }

            }

            return  query.Substring(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilterData();
            EventContainer.PublishEvent
(EventPublisher.Events.ShowFilterDone.ToString(), new EventArg(Guid.NewGuid(), null));
        }
    }
}
