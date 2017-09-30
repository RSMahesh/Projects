using EventPublisher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsControlLibrary1;

namespace WindowsFormsApplication3
{
    public partial class EditScreen : Form
    {
        DataColumnCollection _columns;
        public static List<DataColumn> _editableColuns;
        List<InputControl> _inputs = new List<InputControl>();
        public EditScreen(DataColumnCollection columns)
        {
            _columns = columns;
            InitializeComponent();
            EventContainer.SubscribeEvent(EventPublisher.Events.RowSelectionChange.ToString(), LoadData);
            EventContainer.SubscribeEvent(EventPublisher.Events.ReOrder.ToString(), ReOrder);
            leftPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            rightPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            panel1.AutoScroll = true;
        }

        private void ReOrder(EventArg eventArg)
        {
            ReOrder();
        }

        private void LoadData(EventArg eventArg)
        {
            DataRow dr = (DataRow)eventArg.Arg;
            flowLayoutPanel1.Controls.Clear();

            for (var i = 0; i < _inputs.Count; i++)
            {
                _inputs[i].Text = dr[i].ToString();
                LoadImage(dr[i].ToString());
            }
        }

        private void LoadImage(string url)
        {
            try
            {
                var uu = new Uri(url);
                PictureBox pictureBox = new PictureBox();
                pictureBox.Height = 250;
                pictureBox.Width = 250;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                // pictureBox.b
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox.Tag = url;
                pictureBox.Load(url);
                pictureBox.Click += PictureBox_Click;
                flowLayoutPanel1.Controls.Add(pictureBox);

            }
            catch (Exception ex)
            {

            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            var pictureBox = (PictureBox)sender;
            Process.Start(pictureBox.Tag.ToString());
        }

        private void EditScreen_Load(object sender, EventArgs e)
        {
            leftPanel.AutoSize = rightPanel.AutoSize = true;
            ReOrder();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Control c1 = this.tableLayoutPanel1.GetControlFromPosition(0, 0);
            //Control c2 = this.tableLayoutPanel1.GetControlFromPosition(0, 1);

            //if (c1 != null && c2 != null)
            //{
            //    this.tableLayoutPanel1.SetRow(c2, 0);
            //    this.tableLayoutPanel1.SetRow(c1, 1);
            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent(EventPublisher.Events.MoveToNextRecord.ToString(), new EventArg(Guid.NewGuid(), null));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent(EventPublisher.Events.MoveToPriviousRecord.ToString(), new EventArg(Guid.NewGuid(), null));

        }

        //private void AddTextBox(DataColumn column, TableLayoutPanel parent)
        //{
        //    parent.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        //    var inputControl = new InputControl(column.ColumnName);

        //    var col = 0;
        //    var row = 0;

        //    if (  _inputs.Count % 2 != 0)
        //    {
        //        col = 1;
        //    }

        //    row = _inputs.Count / 2;

        //    _inputs.Add(inputControl);
        //    parent.Controls.Add(inputControl, col, row);

        //}

        private void ReOrder()
        {
            panel2.AutoScroll = panel3.AutoScroll = true;
            rightPanel.Controls.Clear();
            rightPanel.RowCount = 1;

            leftPanel.Controls.Clear();
            leftPanel.RowCount = 1;
            _inputs.Clear();
            _editableColuns = new List<DataColumn>();
            if (ColumnsOrder.dtColumnOrder != null)
            {
                foreach (DataRow row in ColumnsOrder.dtColumnOrder.Rows)
                {
                    var inputControl = new InputControl(row["Name"].ToString(), (int)row["SNo"], (int)row["Height"]);
                    var Sno = (int)row["SNo"];
                    // if (!(bool)row["Hide"])
                    {
                        inputControl.textBox1.KeyUp += TextBox1_TextChanged;

                        if ((bool)row["Editable"])
                        {
                            leftPanel.Controls.Add(inputControl, 0, leftPanel.RowCount - 1);
                            leftPanel.RowCount += 1;
                            _editableColuns.Add(_columns[Sno]);
                        }
                        else
                        {
                            inputControl.ReadOnly = true;
                            rightPanel.Controls.Add(inputControl, 0, rightPanel.RowCount - 1);
                            rightPanel.RowCount += 1;
                        }
                    }
                   

                    _inputs.Add(inputControl);

                }
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            EventContainer.PublishEvent(EventPublisher.Events.RecordUpdated.ToString(), new EventArg(Guid.Empty, new KeyValuePair<int, string>((int)textBox.Tag, textBox.Text)));
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
