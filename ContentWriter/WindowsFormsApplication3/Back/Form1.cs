using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StatusMaker.Data;
using System.Data;
using System.Linq;
using EventPublisher;
using System.Drawing;
using System.Net;
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        string _filePath;
        OLDBConnection12 _dataConnection;
        private int _imageSize = 50;
        public Form1(string filePath)
        {
            InitializeComponent();
            EventContainer.SubscribeEvent(EventPublisher.Events.MoveToNextRecord.ToString(), MoveToNextRecord);
            EventContainer.SubscribeEvent(EventPublisher.Events.MoveToPriviousRecord.ToString(), MoveToPriviousRecord);
            EventContainer.SubscribeEvent(EventPublisher.Events.RecordUpdated.ToString(), RecordUpdated);
            EventContainer.SubscribeEvent(EventPublisher.Events.Save.ToString(), Save);

            _filePath = filePath;
        }

        private void Save(EventArg obj)
        {
            var index = dataGridView1.CurrentRow.Index;
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            dataGridView1.CurrentRow.Selected = false;
            dataGridView1.CurrentCell = dataGridView1.Rows[index + 1].Cells[0];

            _dataConnection.SetUpdateCommand(EditScreen._editableColuns);

            MessageBox.Show("Saved");
            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[0];

            dataGridView1.Rows[index].Selected = true;

            RaiseEventForChange();


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
        private void Form1_Load(object sender, EventArgs e)
        {
            _dataConnection = new OLDBConnection12(_filePath);
            var dt = _dataConnection.ExecuteDatatable("Select * from [Sheet1$]");
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            var dataTable = (DataTable)dataGridView1.DataSource;
            EditScreen editScreen = new EditScreen(dataTable.Columns);
            editScreen.MdiParent = this.MdiParent;
            editScreen.Show();
            dataGridView1.Rows[0].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            editScreen.FormBorderStyle = FormBorderStyle.None;
            //this.FormBorderStyle = FormBorderStyle.None;
            editScreen.Top = 0;
            this.Top = editScreen.Top + editScreen.Height + 10;
            this.Width = this.MdiParent.Width - 20;
            // this.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            // this.Dock = DockStyle.Fill;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            ColumnsOrder._columns = dataTable.Columns;
            ColumnsOrder frm = new ColumnsOrder();
            frm.MdiParent = this.MdiParent;

            frm.Show();

            AddImageColumn(GetImageColumns());
        }
        private List<int> GetImageColumns()
        {
            List<int> cols = new List<int>();

            for (var i = 0; i < dataGridView1.Columns.Count; i++)
            {
                try
                {
                    var uu = new Uri(dataGridView1.Rows[1].Cells[i].Value.ToString());
                    cols.Add(i);

                }
                catch (Exception ex)
                {

                }
            }

            return cols;
        }


        private void AddImageColumn(List<int> imageCells)
        {
            List<DataGridViewColumn> imgcolumns = new List<DataGridViewColumn>();

            foreach (var cell in imageCells)
            {
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.HeaderText = "Image Column";
                imgColumn.Width = _imageSize + 20;
                //img.ImageLayout = DataGridViewImageCellLayout.;
                dataGridView1.Columns.Add(imgColumn);
                imgcolumns.Add(imgColumn);
            }

            for (int rowIndex =0; rowIndex <  dataGridView1.Rows.Count; rowIndex++)
            {
                var tt = @"C:\Users\mahesh.bailwal\Documents\Lamp.jpg";
                for (var i = 0; i < imageCells.Count; i++)
                {
                    //var url = row.Cells[imageCells[i]].Value.ToString();
                    // WebClient wc = new WebClient();
                    //byte[] bytes = wc.DownloadData(url);
                    //MemoryStream ms = new MemoryStream(bytes);
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                    // var img = Image.FromFile(tt);

                    dataGridView1.Rows[rowIndex].Cells[imgcolumns[i].Index].Value = ImageThumbnailDataGridView.Helper.ResizeImage(tt,50, 50, false);
                   
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            RaiseEventForChange();
        }

        private void RaiseEventForChange()
        {
            if (dataGridView1.CurrentRow == null)
                return;
            var dr = ((System.Data.DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;
            EventContainer.PublishEvent(EventPublisher.Events.RowSelectionChange.ToString(), new EventArg(Guid.NewGuid(), dr));

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var tt = dataGridView1.CurrentRow.DataBoundItem;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.WrapMode == DataGridViewTriState.True)
            {

                dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
            else
            {
                dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            }
            MessageBox.Show(e.ColumnIndex.ToString());
        }
    }
}
