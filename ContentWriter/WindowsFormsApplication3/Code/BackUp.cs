using EventPublisher;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class BackUp
    {
        AppContext appContext;
        CheckBox HeaderCheckBox;
        Form mdiParent;
        public BackUp(AppContext appContext, Form mdiParent)
        {
            this.appContext = appContext;
            this.mdiParent = mdiParent;
        }
        public void addCheckBox()
        {
            DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
            dgvCmb.ValueType = typeof(bool);
            dgvCmb.Name = "Chk";
            dgvCmb.HeaderText = "";
            dgvCmb.Width = 60;
            appContext.dataGridView.Columns.Add(dgvCmb);

            HeaderCheckBox = new CheckBox();
            HeaderCheckBox.Size = new Size(15, 15);
            HeaderCheckBox.CheckedChanged += HeaderCheckBox_CheckedChanged;

            appContext.dataGridView.Controls.Add(HeaderCheckBox);
            ResetCheckBoxLocation();
        }
        public void ResetCheckBoxLocation()
        {
            //Get the column header cell bounds
            Rectangle oRectangle =
            appContext.dataGridView.GetCellDisplayRectangle(appContext.dataGridView.Columns.Count - 1, -1, true);
            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1;

            if (oPoint.X < 1)
            {
                return;
            }

            //Change the location of the CheckBox to make it stay on the header
            HeaderCheckBox.Location = oPoint;
        }

        private void HeaderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in appContext.dataGridView.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells[appContext.dataGridView.Columns.Count - 1]).Value = HeaderCheckBox.Checked;

            }
        }

        public void StartBackUpImport(EventArg arg)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "XML (*.xml)|*.xml";

            openFileDialog.InitialDirectory =
                Path.GetDirectoryName(appContext.ExcelFilePath) + "\\" +
                        Path.GetFileNameWithoutExtension(appContext.ExcelFilePath)
                        + "_dataBackup\\Full";

            string fileName = openFileDialog.FileName;

            if (arg.Arg != null)
            {
                fileName = arg.Arg.ToString();
                EventContainer.SubscribeEvent(EventPublisher.Events.DataImportSelectionCompleted.ToString(), OnDataImportSelectionCompleted);
            }
            else
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    EventContainer.SubscribeEvent(EventPublisher.Events.DataImportSelectionCompleted.ToString(), OnDataImportSelectionCompleted);
                }
            }

            var form1 = new Form1(fileName, true);
            form1.MdiParent = this.mdiParent;
            form1.Show();
        }
        public void OnDataImportSelectionCompleted(EventArg arg)
        {
            DataTable backUpDt = (DataTable)arg.Arg;

            var currentDt = ((DataView)appContext.dataGridView.DataSource).Table;

            foreach (DataRow backUpdataRow in backUpDt.Rows)
            {
                var row = int.Parse(backUpdataRow[0].ToString());

                //--row as index start from 0
                var currentDataRow = currentDt.Rows[--row];

                //Start from one to avoid ID column
                for (var i = 1; i < backUpDt.Columns.Count; i++)
                {
                    var colName = backUpDt.Columns[i].ColumnName;
                    currentDataRow[colName] = backUpdataRow[colName];
                }
            }
        }
    }
}
