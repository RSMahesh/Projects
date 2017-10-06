using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StatusMaker.Data;
using System.Data;
using EventPublisher;
using System.Net;
using System.IO;
using Utility;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Script.Serialization;
using System.Linq;

namespace WindowsFormsApplication3
{
    public class CellData
    {
        public CellData(object value, Point location)
        {
            Value = value;
            Location = location;
        }

        public CellData() { }
        public object Value { get; set; }
        public Point Location { get; set; }
    }
    public partial class Form1 : Form
    {
        string _excelFilePath;
        OLDBConnection12 _dataConnection;
        private int _imageSize = 100;
        bool IsXmlFile;
        bool imageColumnLoaded = false;
        ContextMenu contextMenu = new ContextMenu();
        UndoRedoStack<CellData> _undoRedo;
        bool _importBackUp;

        public Form1(string filePath, bool importBackUp = false)
        {

            InitializeComponent();

            _importBackUp = importBackUp;
            _undoRedo = new UndoRedoStack<CellData>(new SetCellDataCommand(dataGridView1));
            EventContainer.SubscribeEvent(EventPublisher.Events.MoveToNextRecord.ToString(), MoveToNextRecord);
            EventContainer.SubscribeEvent(EventPublisher.Events.MoveToPriviousRecord.ToString(), MoveToPriviousRecord);
            EventContainer.SubscribeEvent(EventPublisher.Events.RecordUpdated.ToString(), RecordUpdated);
            EventContainer.SubscribeEvent(EventPublisher.Events.Save.ToString(), Save);
            EventContainer.SubscribeEvent(EventPublisher.Events.WordDocClosed.ToString(), WordDocClosed);
            EventContainer.SubscribeEvent(EventPublisher.Events.OpenWord.ToString(), OpenWord);
            EventContainer.SubscribeEvent(EventPublisher.Events.DescriptionCount.ToString(), DescriptionCount);
            EventContainer.SubscribeEvent(EventPublisher.Events.ShowFilter.ToString(), ShowFilter);
            EventContainer.SubscribeEvent(EventPublisher.Events.ShowFilterDone.ToString(), FilterDone);
            EventContainer.SubscribeEvent(EventPublisher.Events.Undo.ToString(), UnDo);
            EventContainer.SubscribeEvent(EventPublisher.Events.ReDo.ToString(), ReDo);
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.CellLeave += dataGridView1_CellLeave;
            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            dataGridView1.RowHeaderMouseClick += DataGridView1_RowHeaderMouseClick;
            dataGridView1.CellValidating += DataGridView1_CellValidating;

            contextMenu.MenuItems.Add("Copy", OnCopy);
            contextMenu.MenuItems.Add("Past", OnPast);


            _excelFilePath = filePath;

            if (Path.GetExtension(_excelFilePath) == ".xml")
            {
                IsXmlFile = true;
            }
            else
            {
                EventContainer.SubscribeEvent(EventPublisher.Events.StartDataImport.ToString(), StartBackUpImport);

            }

            dataGridView1.ContextMenu = contextMenu;
        }


        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int maxLength = 0;

            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.CadetBlue;
                return;
            }
           

            foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
            {
                if (!(dataGridView1.Columns[cell.ColumnIndex] is DataGridViewImageColumn))
                {
                    maxLength = cell.Value.ToString().Length > maxLength ? cell.Value.ToString().Length : maxLength;

                    if (cell.Value.ToString().Length > 100)
                    {
                        dataGridView1.Columns[cell.ColumnIndex].Width = dataGridView1.Columns[cell.ColumnIndex].Width < 400 ? 400 : dataGridView1.Columns[cell.ColumnIndex].Width;
                    }
                }
            }

            dataGridView1.Rows[e.RowIndex].Height = dataGridView1.Rows[e.RowIndex].Height > 100 ? 100 : GetRowHeightBasedOnLength(maxLength);


        }

        private int GetRowHeightBasedOnLength(int maxColumnTextLength)
        {
            return (int)Math.Abs(maxColumnTextLength / 2.5) + 10;
        }

        #region UndoRedo

        bool attachedEventFroKepUp = false;
        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!attachedEventFroKepUp)
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                tb.KeyDown += Tb_KeyDown;
                attachedEventFroKepUp = true;
            }
        }

        private void Tb_KeyDown(object sender, KeyEventArgs e)
        {
            //skip if Ctrl+Z
            if (e.Control)
                return;

            DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)sender;
            object value = ConvertTo(dataGridView1.CurrentCell.ValueType, tb.Text);

            // for now skipping key level undo may be in future
            //   _undoRedo.Do(new CellData(value, new Point(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex)));
        }

        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!imageColumnLoaded)
                return;

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn)
            {
                return;
            }

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != e.FormattedValue.ToString())
            {
                _undoRedo.Do(new CellData(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, new Point(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex)));
            }
        }

        private object ConvertTo(Type type, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Double:
                    return double.Parse(value);
                default:
                    return value;
            }

        }

        private void UnDo(EventPublisher.EventArg arg)
        {
            _undoRedo.Undo();
        }

        private void ReDo(EventPublisher.EventArg arg)
        {
            _undoRedo.Redo();
        }

        #endregion

        #region MltiCopy

        void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.Invalidate();
        }


        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (IsCellSelected(e.ColumnIndex, e.RowIndex))
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All
 & ~DataGridViewPaintParts.Border);
                using (Pen p = new Pen(Color.White, 2))
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(p, rect);
                }
                e.Handled = true;
            }

        }

        private bool IsCellSelected(int col, int row)
        {
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                if (cell.ColumnIndex == col && cell.RowIndex == row)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnCopy(object sender, EventArgs e)
        {
            var listSelectedCells = new List<CellData>();

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                listSelectedCells.Add(new CellData(cell.Value.ToString(), new Point(cell.ColumnIndex, cell.RowIndex)));
            }

            var selctionInfo = GetSelectedRowAndColumnCount(listSelectedCells);

            if (selctionInfo.X > 1 && selctionInfo.Y > 1)
            {
                MessageBox.Show("Invalid Selection Only One Row or One Column can be selected." +
                  Environment.NewLine + "Selected Row :" + selctionInfo.Y.ToString() + " Selected Columns :" + selctionInfo.X.ToString());
                return;
            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(listSelectedCells);
            Clipboard.SetText(serializedResult);
        }

        private void OnPast(object sender, EventArgs e)
        {
            var text = Clipboard.GetText();
            var pastCells = new List<CellData>();
            List<CellData> copyedCells;

            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                pastCells.Add(new CellData(string.Empty, new Point(cell.ColumnIndex, cell.RowIndex)));
            }

            try
            {
                var serializer = new JavaScriptSerializer();
                copyedCells = serializer.Deserialize<List<CellData>>(text);
            }
            catch (ArgumentException ex)
            {
                PasteSingleCellData(text, pastCells);
                return;
            }

            PastCellData(copyedCells, pastCells);
        }

        private void PasteSingleCellData(string copyedText, List<CellData> targetCells)
        {
            for (var indx = 0; indx < targetCells.Count; indx++)
            {
                _undoRedo.Do(new CellData(dataGridView1.Rows[targetCells[indx].Location.Y].Cells[targetCells[indx].Location.X].Value, targetCells[indx].Location));
                dataGridView1.Rows[targetCells[indx].Location.Y].Cells[targetCells[indx].Location.X].Value =
                   copyedText;
            }
        }


        private void PastCellData(List<CellData> sourceCells, List<CellData> targetCells)
        {
            var sourceInfo = GetSelectedRowAndColumnCount(sourceCells);
            var targetInfo = GetSelectedRowAndColumnCount(targetCells);

            if (sourceInfo != targetInfo)
            {
                MessageBox.Show("Invalid Selection for Past"
                    + Environment.NewLine + "Correct Row :" + sourceInfo.Y.ToString() + " Correct Columns :" + sourceInfo.X.ToString());
                return;
            }

            if (sourceInfo.X > 1 && targetInfo.X > 1)
            {
                sourceCells = sourceCells.OrderBy(x => x.Location.X).ToList();
                targetCells = targetCells.OrderBy(x => x.Location.X).ToList();
            }
            else if (sourceInfo.Y > 1 && targetInfo.Y > 1)
            {
                sourceCells = sourceCells.OrderBy(x => x.Location.Y).ToList();
                targetCells = targetCells.OrderBy(x => x.Location.Y).ToList();
            }

            for (var indx = 0; indx < sourceCells.Count; indx++)
            {
                _undoRedo.Do(new CellData(dataGridView1.Rows[targetCells[indx].Location.Y].Cells[targetCells[indx].Location.X].Value, targetCells[indx].Location));

                dataGridView1.Rows[targetCells[indx].Location.Y].Cells[targetCells[indx].Location.X].Value =
                    sourceCells[indx].Value;
            }
        }

        private Point GetSelectedRowAndColumnCount(List<CellData> cellsData)
        {
            var listColumns = new List<int>();
            var listRows = new List<int>();

            foreach (var p in cellsData)
            {
                if (!listColumns.Contains(p.Location.X))
                {
                    listColumns.Add(p.Location.X);
                }

                if (!listRows.Contains(p.Location.Y))
                {
                    listRows.Add(p.Location.Y);
                }
            }

            return new Point(listColumns.Count, listRows.Count);
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Load context menu on right mouse click
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                // contextMenu.Show(dataGridView1, e.Location);
                hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
                Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Point p = new Point(r.X + e.X, r.Y + e.Y);


                if (dataGridView1.SelectedCells.Count > 1)
                {
                    contextMenu.Show(dataGridView1, p);
                }
            }
        }

        #endregion

        private void Save(EventArg obj)
        {
            if (!this.Visible)
            {
                return;
            }

            if (IsXmlFile)
            {
                MessageBox.Show("Can not save backup file", "Save Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var index = dataGridView1.CurrentRow.Index;
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            dataGridView1.CurrentRow.Selected = false;
            dataGridView1.CurrentCell = dataGridView1.Rows[index + 1].Cells[0];

            var tbl = ((DataView)dataGridView1.DataSource).Table;
            _dataConnection.SetUpdateCommand(tbl.Columns);

            MessageBox.Show("Saved");

            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[0];
            dataGridView1.Rows[index].Selected = true;
            RaiseEventForChange();
        }

        private DataTable MarkAllDirty()
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];

            var allCheckedRows = dataGridView1.Rows.Cast<DataGridViewRow>()
                            .Where(row => (bool?)row.Cells[dataGridView1.Columns.Count - 1].Value == true)
                            .ToList();

            var dt = ((DataView)dataGridView1.DataSource).Table;
            var dt2 = dt.Clone();

            allCheckedRows.ForEach(row =>
            {
                var rowNumber = int.Parse(row.Cells["ID"].Value.ToString());
                var desRow = dt2.NewRow();
                var sourceRow = dt.Rows[--rowNumber];
                desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];

                dt2.Rows.Add(desRow);
            });

            return dt2;
        }

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
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataError += DataGridView1_DataError;
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            _dataConnection = new OLDBConnection12(_excelFilePath);
            var dt = _dataConnection.ExecuteDatatable("Select * from [Sheet1$]").DefaultView;
            dataGridView1.DataSource = dt;
            //  dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.AllowUserToResizeRows = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.MultiSelect = true;
            dataGridView1.RowHeadersWidth = 60;
            dataGridView1.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;


            if (dataGridView1.Columns.Contains("ID"))
            {
                dataGridView1.Columns["ID"].Visible = false;
            }

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                //   col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (IsXmlFile)
            {
                dataGridView1.RowTemplate.DefaultCellStyle.BackColor = Color.White;
                dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;

                // dataGridView1.ReadOnly = true;
            }

            this.Text = _excelFilePath;


        }


        private void addCheckBox()
        {
            DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
            dgvCmb.ValueType = typeof(bool);
            dgvCmb.Name = "Chk";
            dgvCmb.HeaderText = "CheckBox";
            dataGridView1.Columns.Add(dgvCmb);
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var data = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();

            var info = "Error : " + e.Exception.Message + Environment.NewLine +
                Environment.NewLine + " Row Number : " + (e.RowIndex + 1).ToString();

            MessageBox.Show(info);
            e.Cancel = true;
        }


        List<int> imageColIndexs = new List<int>();
        List<DataGridViewColumn> imgcolumns = new List<DataGridViewColumn>();

        private void GetImageColumns()
        {

            for (var i = 0; i < dataGridView1.Columns.Count; i++)
            {
                try
                {
                    var uu = new Uri(dataGridView1.Rows[1].Cells[i].Value.ToString());
                    if (uu.Scheme.StartsWith("http"))
                    {
                        imageColIndexs.Add(i);
                        dataGridView1.Columns[i].Visible = false;
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }
        private void AddImageColumn()
        {

            foreach (var cell in imageColIndexs)
            {
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.HeaderText = "Image";
                imgColumn.Width = _imageSize + 20;
               
                dataGridView1.Columns.Insert(1, imgColumn);
                imgcolumns.Add(imgColumn);
                imgColumn.Frozen = true;
            }

        }

        private void LoadImageInCell()
        {
            WebClient wc = new WebClient();
            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
            {
                dataGridView1.Rows[rowIndex].HeaderCell.Value = (rowIndex + 1).ToString();
                for (var i = 0; i < imageColIndexs.Count; i++)
                {
                    if (dataGridView1.Rows[rowIndex].Cells[imageColIndexs[i]].Value != null)
                    {
                        var url = dataGridView1.Rows[rowIndex].Cells[imageColIndexs[i] + imageColIndexs.Count].Value.ToString();

                        var uri = new Uri(url);
                        var filePAth = GetLocalImagePath(uri);

                        if (!Directory.Exists(Path.GetDirectoryName(filePAth)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(filePAth));
                        }

                        if (!File.Exists(filePAth))
                        {
                            wc.DownloadFile(url, filePAth);
                        }

                        var thumbFilePath = Path.Combine(Path.GetDirectoryName(filePAth), Path.GetFileNameWithoutExtension(filePAth) + "_thumb" + ".ico");
                        Image thumNailImage = null;

                        if (File.Exists(thumbFilePath))
                        {
                            thumNailImage = Image.FromFile(thumbFilePath);
                        }
                        else
                        {
                            var img = Image.FromFile(filePAth);
                            thumNailImage = ImageThumbnailDataGridView.Helper.ResizeImage(img, _imageSize, _imageSize, false);
                            thumNailImage.Save(thumbFilePath, ImageFormat.Icon);
                            img.Dispose();
                        }

                        dataGridView1.Rows[rowIndex].Cells[imgcolumns[i].Index].Value = thumNailImage;
                        dataGridView1.Rows[rowIndex].Cells[imgcolumns[i].Index].Tag = filePAth;
                    }
                }
            }

        }

        private string GetLocalImagePath(Uri uri)
        {
            var tt = _excelFilePath.Split(new string[] { "_dataBackup" }, StringSplitOptions.None)[0];
            return Path.GetDirectoryName(tt) + "\\" +
                       Path.GetFileNameWithoutExtension(tt) + "-images"
                       + uri.LocalPath.Replace("/", "\\");
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            //RaiseEventForChange();
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            dataGridView1.BeginEdit(false);
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Tag != null)
            {
                if (frmPrectiureViwer == null)
                {
                    frmPrectiureViwer = new PictureViewer(dataGridView1[e.ColumnIndex, e.RowIndex].Tag.ToString());
                }

                try
                {
                    frmPrectiureViwer.SetImage(dataGridView1[e.ColumnIndex, e.RowIndex].Tag.ToString());
                    frmPrectiureViwer.TopMost = true;
                    frmPrectiureViwer.Show();
                }
                catch
                {
                    frmPrectiureViwer = new PictureViewer(dataGridView1[e.ColumnIndex, e.RowIndex].Tag.ToString());
                    frmPrectiureViwer.SetImage(dataGridView1[e.ColumnIndex, e.RowIndex].Tag.ToString());
                    frmPrectiureViwer.TopMost = true;
                    frmPrectiureViwer.Show();
                }
            }

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;

            if (dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.WrapMode == DataGridViewTriState.True)
            {

                dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
            else
            {
                // dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;


                dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // var width = dataGridView1.Columns[e.ColumnIndex].Width;

                // dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                // dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                // dataGridView1.Columns[e.ColumnIndex].Width = width;

            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (imageColumnLoaded)
                return;

            GetImageColumns();
            AddImageColumn();
            LoadImageInCell();
            LoadColumnOrder();

            if (_importBackUp)
            {
                addCheckBox();
            }

            this.WindowState = FormWindowState.Maximized;
            imageColumnLoaded = true;
        }


        PictureViewer frmPrectiureViwer = null;
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        bool alreadyOpend = false;

        private void OpenWord(EventPublisher.EventArg arg)
        {
            try
            {
                if (!alreadyOpend)
                {
                    OutProcessDocument.OpenDocument(@"D:\1.docx");
                    OutProcessDocument.ReSize(10, 10, 300, 300);
                    alreadyOpend = true;
                }

                OutProcessDocument.Text = dataGridView1.CurrentCell.Value.ToString();
                OutProcessDocument.Visible = true;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                alreadyOpend = false;

                OutProcessDocument.Close();
                MessageBox.Show("Try Again");
            }

        }



        private void DescriptionCount(EventPublisher.EventArg arg)
        {
            var count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Description"].Value != null &&
                     !string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["Description"].Value.ToString()))
                {
                    count++;

                }
            }

            MessageBox.Show(count.ToString());
        }

        private void ShowFilter(EventPublisher.EventArg arg)
        {
            Filter filter = new Filter((DataView)dataGridView1.DataSource);

            filter.WindowState = FormWindowState.Normal;
            filter.Show();


        }

        private void FilterDone(EventPublisher.EventArg arg)
        {
            LoadImageInCell();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.BeginEdit(false);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_importBackUp)
            {
                // MarkAllDirty();
                var dt = MarkAllDirty();
                EventContainer.PublishEvent(EventPublisher.Events.DataImportSelectionCompleted.ToString(), new EventArg(Guid.NewGuid(), dt));

            }


            this.Dispose();

            EventPublisher.EventContainer.UnSubscribeAll(this);
        }

        private void StartBackUpImport(EventArg arg)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "XML (*.xml)|*.xml";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                EventContainer.SubscribeEvent(EventPublisher.Events.DataImportSelectionCompleted.ToString(), OnDataImportSelectionCompleted);

                var form1 = new Form1(openFileDialog.FileName, true);
                form1.MdiParent = this.MdiParent;
                form1.Show();
            }


        }
        private void OnDataImportSelectionCompleted(EventArg arg)
        {
            DataTable backUpDt = (DataTable)arg.Arg;

            var currentDt = ((DataView)dataGridView1.DataSource).Table;

            foreach (DataRow backUpdataRow in backUpDt.Rows)
            {
                var row = int.Parse(backUpdataRow[0].ToString());
                var currentDataRow = currentDt.Rows[--row];
                //Start from one to avoiid ID column
                for (var i = 1; i < backUpDt.Columns.Count; i++)
                {
                    currentDataRow[i] = backUpdataRow[i];
                }

            }

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (dataGridView1.CurrentCell == null)
                {
                    return;
                }

                if (!alreadyOpend)
                {
                    OutProcessDocument.OpenDocument(@"D:\1.docx");
                    OutProcessDocument.ReSize(50, 50, 200, 200);
                    alreadyOpend = true;
                }
                else
                {
                    // OutProcessDocument.Visible = false;
                    //  return;
                }

                OutProcessDocument.Text = dataGridView1.CurrentCell.Value.ToString();
                OutProcessDocument.Visible = true;
            }
        }


        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }


        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            LoadImageInCell();
        }
    }
}
