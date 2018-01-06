using EventPublisher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1
    {
        private void SaveColumnOrder()
        {

            JavaScriptSerializer jj = new JavaScriptSerializer();
            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                // cols.Add(col.Clone());
            }

            var json = jj.Serialize(cols);
            var jsonFile = Path.Combine(Path.GetDirectoryName(_excelFilePath), Path.GetFileNameWithoutExtension(_excelFilePath) + ".json");
            File.AppendAllText(jsonFile, json);

        }
        private void LoadColumnOrder()
        {
            var jsonFile = Path.Combine(Path.GetDirectoryName(_excelFilePath), Path.GetFileNameWithoutExtension(_excelFilePath) + ".json");
            if (File.Exists(jsonFile))
            {
                JavaScriptSerializer jj = new JavaScriptSerializer();
                var colunsInfo = jj.Deserialize<List<DataGridViewColumn>>(File.ReadAllText(jsonFile));

                foreach (DataGridViewColumn col in colunsInfo)
                {
                    dataGridView1.Columns[col.Name].DisplayIndex = col.DisplayIndex;
                }

            }
        }
        private void WordDocClosed(EventArg obj)
        {
            //dataGridView1.CurrentCell.Value = obj.Arg.ToString();
            //dataGridView1.EndEdit();
            dataGridView1.Invoke((Action)delegate
            {

                dataGridView1.CurrentCell.Value = obj.Arg.ToString();
                dataGridView1.EndEdit();
                //MessageBox.Show("Mahesh" + dataGridView1.CurrentCell.Value.ToString());

            });
        }
        private void MoveToNextRecord(EventArg obj)
        {
            var index = dataGridView1.CurrentRow.Index + 1;
            dataGridView1.CurrentRow.Selected = false;
            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[0];
            dataGridView1.Rows[index].Selected = true;
            RaiseEventForChange();
        }
        private void RecordUpdated(EventArg obj)
        {
            var keyVal = (KeyValuePair<int, string>)obj.Arg;
            dataGridView1.CurrentRow.Cells[keyVal.Key].Value = keyVal.Value;
        }

        private void MoveToPriviousRecord(EventArg obj)
        {
            var index = dataGridView1.CurrentRow.Index;
            if (index > 0)
                index--;
            dataGridView1.CurrentRow.Selected = false;
            dataGridView1.Rows[index].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[0];
            RaiseEventForChange();
        }

        private void RaiseEventForChange()
        {
            //for now
            return;

            if (dataGridView1.CurrentRow == null)
                return;
            var dr = ((System.Data.DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;
            EventContainer.PublishEvent(EventPublisher.Events.RowSelectionChange.ToString(), new EventArg(Guid.NewGuid(), dr));

        }


        private SortOrder _lastSortOrder;
        private int _lastColumnSortedIndex;
        private void Sort2(DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn SORT_ORDER;

            if (dataGridView1.Columns[e.ColumnIndex].ValueType == typeof(string))
            {
                SORT_ORDER = dataGridView1.Columns["TextSortData"];
            }

            else
            {
                SORT_ORDER = dataGridView1.Columns["NumberSortData"];
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name != "ID")
            {
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            }
            else
            {
                dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
            }


            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.Cells[SORT_ORDER.Index].Value = r.Cells[e.ColumnIndex].Value;
                //int tt;
                // if(int.TryParse(r.Cells[SORT_ORDER.Index].Value.ToString(),out tt))
                //{
                //    var yy = "dffdsff";
                //}
            }

            // return;
            switch (_lastSortOrder)
            {
                case System.Windows.Forms.SortOrder.None:
                    dataGridView1.Sort(SORT_ORDER, ListSortDirection.Ascending);
                    break;
                case System.Windows.Forms.SortOrder.Ascending:
                    dataGridView1.Sort(SORT_ORDER, ListSortDirection.Descending);
                    break;
                case System.Windows.Forms.SortOrder.Descending:
                    dataGridView1.Sort(SORT_ORDER, ListSortDirection.Ascending);
                    break;
            }

            _lastSortOrder = dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = dataGridView1.SortOrder;

            //  dataGridView1.Refresh();

            dataGridView1.CurrentCell = dataGridView1.Rows[1].Cells[e.ColumnIndex];
            var val = dataGridView1.CurrentCell.Value;
            dataGridView1.BeginEdit(false);
            dataGridView1.EndEdit();
        }

    }
}
