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
using System.ComponentModel;
using System.Text.RegularExpressions;

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

    public class RowInfo
    {
        public int RowId { get; set; }
        public bool Highlighted { get; set; }
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
        List<RowInfo> rowsInfo = new List<RowInfo>();

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
            EventContainer.SubscribeEvent(EventPublisher.Events.Statistics.ToString(), ShowStatistics);
            EventContainer.SubscribeEvent(EventPublisher.Events.Formula.ToString(), Formula);


            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.CellLeave += dataGridView1_CellLeave;
            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            dataGridView1.RowHeaderMouseClick += DataGridView1_RowHeaderMouseClick;
            dataGridView1.CellValidating += DataGridView1_CellValidating;
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
            dataGridView1.Sorted += DataGridView1_Sorted;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

            contextMenu.MenuItems.Add("Copy", OnCopy);
            contextMenu.MenuItems.Add("Past", OnPast);
            contextMenu.MenuItems.Add("Delete", OnDelete);
            contextMenu.MenuItems.Add("ApplyFormula", OnApplyFormula);


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

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SaveUnSavedData(new CellData(dataGridView1[e.ColumnIndex, e.RowIndex].Value, new Point(e.ColumnIndex, e.RowIndex)));
            //  MessageBox.Show(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        private void DataGridView1_Sorted(object sender, EventArgs e)
        {
            ApplyRowInfo();
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
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersWidth = 30;
            dataGridView1.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;


            if (dataGridView1.Columns.Contains("ID"))
            {
                dataGridView1.Columns["ID"].ReadOnly = true;
                dataGridView1.Columns["ID"].Width = 45;
            }

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;

            }

            if (IsXmlFile)
            {
                dataGridView1.RowTemplate.DefaultCellStyle.BackColor = Color.White;
                dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;

                // dataGridView1.ReadOnly = true;
            }

            this.Text = _excelFilePath;


        }


        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int maxLength = 0;

            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                HighLightRow(int.Parse(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString()));

                if (dataGridView1.SelectedCells.Count > 1)
                {
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (e.RowIndex != cell.RowIndex)
                        {
                            HighLightRow(int.Parse(dataGridView1.Rows[cell.RowIndex].Cells["ID"].Value.ToString()));
                        }
                    }
                }

                //  HighLightRow(int.Parse(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString()));
                return;
            }




            IncreaseRowHeight(e.RowIndex, e.ColumnIndex);

        }

        private void HighLightRow(int rowId)
        {
            var rowIndex = GetRowWithId(rowId);

            if (dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor == Color.CadetBlue)
            {
                dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                rowsInfo.Remove(rowsInfo.FirstOrDefault(x => x.RowId == rowIndex));
            }
            else
            {
                ChaneRowColorToCadetBlue(rowIndex);
                RowInfo rowInfo = new RowInfo();
                rowInfo.Highlighted = true;
                rowInfo.RowId = rowId;
                rowsInfo.Add(rowInfo);
            }
        }

        private int GetRowWithId(int rowId)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (int.Parse(row.Cells["ID"].Value.ToString()) == rowId)
                {
                    return row.Index;
                }
            }

            return -1;
        }

        private void ChaneRowColorToCadetBlue(int rowIndex)
        {
            dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.CadetBlue;
        }


        private void IncreaseRowHeight(int row, int col, bool toogle = true)
        {
            int maxLength = 0;

            foreach (DataGridViewCell cell in dataGridView1.Rows[row].Cells)
            {
                if (!(dataGridView1.Columns[cell.ColumnIndex] is DataGridViewImageColumn))
                {
                    maxLength = cell.Value.ToString().Length > maxLength ? cell.Value.ToString().Length : maxLength;

                    if (cell.Value.ToString().Length > 100)
                    {
                        dataGridView1.Columns[cell.ColumnIndex].Width = dataGridView1.Columns[cell.ColumnIndex].Width < 500 ? 500 : dataGridView1.Columns[cell.ColumnIndex].Width;
                    }
                }
            }

            dataGridView1.Rows[row].Height = dataGridView1.Rows[row].Height > GetRowHeightBasedOnLength(maxLength) ? dataGridView1.Rows[row].Height : GetRowHeightBasedOnLength(maxLength);
        }


        private int GetRowHeightBasedOnLength(int maxColumnTextLength)
        {

            return (int)Math.Abs(maxColumnTextLength / 4) + 10;
        }

        #region UndoRedo

        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            if ((ModifierKeys & Keys.Control) != Keys.Control)
            {
                IncreaseRowHeight(e.RowIndex, e.ColumnIndex);
            }
            //dataGridView1.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //  var newHeight =   GetRowHeightBasedOnLength(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length);

            //   dataGridView1.Rows[e.RowIndex].Height = dataGridView1.Rows[e.RowIndex].Height > newHeight ? dataGridView1.Rows[e.RowIndex].Height : newHeight;
            //using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            //{
            //    var text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //    SizeF size = graphics.MeasureString(text, new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Point));

            //  TextRenderer.MeasureText(text, arialBold);


            //    dataGridView1.Rows[e.RowIndex].Height =(int) size.Height;
            //    dataGridView1.Columns[e.ColumnIndex].Width = (int)size.Width;
            //}
        }


        bool attachedEventFroKepUp = false;
        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!attachedEventFroKepUp)
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                //tb.KeyDown += Tb_KeyDown;
                //tb.KeyUp += Tb_KeyUp;
                attachedEventFroKepUp = true;
            }
        }

        private void Tb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Z))
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)sender;
                tb.Undo();
                // dataGridView1.EndEdit();
                return;
            }
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
                // MessageBox.Show(e.FormattedValue.ToString());
                var cellData = new CellData(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, new Point(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex));
                _undoRedo.Do(cellData);


            }
        }

        private void CellDataChanged(CellData cellData)
        {
            _undoRedo.Do(cellData);

            SaveUnSavedData(cellData);
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
            if (dataGridView1.IsCurrentCellInEditMode)
            {
                var currentValue = dataGridView1.CurrentCell.Value;
                dataGridView1.EndEdit();
                dataGridView1.CurrentCell.Value = currentValue;
                return;
            }
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


        private void OnApplyFormula(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FormulaWindow.Formula))
            {
                MessageBox.Show("No Fromula Found");
                return;
            }

          

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                var formulaOutPut = ExecuteFromula(cell);

                  _undoRedo.Do(new CellData(cell.Value, new Point(cell.ColumnIndex, cell.RowIndex)));

                cell.Value = formulaOutPut;
            }
        }

        private string ExecuteFromula(DataGridViewCell cell)
        {
          var replaceOutPut =  ReplacePlaceHolders(cell);
            return MakeProperCase( RemoveIfRequired(replaceOutPut));
        }

        private string MakeProperCase(string replaceOutPut)
        {
            const string remove = "%p%";
            var indx = replaceOutPut.IndexOf(remove, StringComparison.OrdinalIgnoreCase);

           

            var arr = replaceOutPut.Split(new string[] { remove }, StringSplitOptions.None);
            if (arr.Length > 2)
            {
             var one =   FirstCharToUpper(arr[1].ToLowerInvariant());

                replaceOutPut = one + arr[2];
                //arr[0].Replace(arr[1], string.Empty);

              //  replaceOutPut = Regex.Replace(arr[0], arr[1], string.Empty, RegexOptions.IgnoreCase);
            }
            return replaceOutPut;
        }

        string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
        private string RemoveIfRequired(string replaceOutPut)
        {
            const string remove = "%-%";
            var indx = replaceOutPut.IndexOf(remove, StringComparison.OrdinalIgnoreCase);

            if (indx > 0)
            {
                var arr = replaceOutPut.Split(new string[] { remove }, StringSplitOptions.None);
                if (arr.Length > 1)
                {
                  //arr[0].Replace(arr[1], string.Empty);

                    replaceOutPut = Regex.Replace(arr[0], arr[1], string.Empty, RegexOptions.IgnoreCase);
                }
            }

            return replaceOutPut;
        }


        private string ReplacePlaceHolders(DataGridViewCell cell)
        {
            var regex = new Regex("{.*?}");
            var matches = regex.Matches(FormulaWindow.Formula);
         //  var formula = FormulaWindow.Formula;
            string formulaOutPut = FormulaWindow.Formula;

            foreach (Match match in matches)
            {
                var columnName = match.Value.Replace("{", "").Replace("}", "");
                var replaceValue = dataGridView1.Rows[cell.RowIndex].Cells[columnName].Value.ToString();
                formulaOutPut = formulaOutPut.Replace("{" + columnName + "}", replaceValue);
            }

            return formulaOutPut;
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
        private void OnDelete(object sender, EventArgs e)
        {
            var listSelectedCells = new List<CellData>();

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                _undoRedo.Do(new CellData(dataGridView1[cell.ColumnIndex, cell.RowIndex].Value, new Point(cell.ColumnIndex, cell.RowIndex)));

                cell.Value = string.Empty;
            }

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
            var Colindex = dataGridView1.CurrentCell.ColumnIndex;

            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            dataGridView1.CurrentRow.Selected = false;

            if (dataGridView1.Rows.Count == index + 1)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[index - 1].Cells[1];
            }
            else
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[index + 1].Cells[1];
            }

            var tbl = ((DataView)dataGridView1.DataSource).Table;
            _dataConnection.SetUpdateCommand(tbl.Columns);

            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[Colindex];
            dataGridView1.Rows[index].Selected = true;
            MessageBox.Show("Saved");
            SaveRowInfo();
            DeleteUnSavedDataFile();



            RaiseEventForChange();
        }

        string UnSavedDelimiter = Environment.NewLine + "_;;;_";
        private void SaveUnSavedData(CellData cellData)
        {
            if (!imageColumnLoaded)
            {
                return;
            }
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(cellData);
            File.AppendAllText(UnSavedDataFile, serializedResult + UnSavedDelimiter);

        }

        private void SaveUnSavedDataCurrentCell(string value)
        {
            if (!imageColumnLoaded)
            {
                return;
            }

            var cellData = new CellData(value, new Point(
                dataGridView1.CurrentCell.ColumnIndex,
                dataGridView1.CurrentCell.RowIndex
                ));
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(cellData);
            File.AppendAllText(UnSavedDataCurrentCellFile, serializedResult);

        }

        private void LoadUnSavedData()
        {

            if (!File.Exists(UnSavedDataFile) && !File.Exists(UnSavedDataCurrentCellFile))
            {
                return;

            }

            DialogResult dialogResult = MessageBox.Show("Do you want to laod unsaved data", "UnSaved Data Found", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }


            if (File.Exists(UnSavedDataFile))
            {
                var data = File.ReadAllText(UnSavedDataFile);
                string[] stringSeparators = new string[] { UnSavedDelimiter };

                var result = data.Split(stringSeparators, StringSplitOptions.None);
                var serializer = new JavaScriptSerializer();


                foreach (string cellInfo in result)
                {
                    if (!string.IsNullOrEmpty(cellInfo))
                    {
                        var cellData = serializer.Deserialize<CellData>(cellInfo);
                        _undoRedo.Do(new CellData(dataGridView1[cellData.Location.X, cellData.Location.Y].Value, cellData.Location));
                        dataGridView1[cellData.Location.X, cellData.Location.Y].Value = cellData.Value;
                    }
                }
            }

            if (File.Exists(UnSavedDataCurrentCellFile))
            {
                var data = File.ReadAllText(UnSavedDataCurrentCellFile);
                var serializer = new JavaScriptSerializer();
                var cellData = serializer.Deserialize<CellData>(data);
                dataGridView1[cellData.Location.X, cellData.Location.Y].Value = cellData.Value;
            }

        }

        private void DeleteUnSavedDataFile()
        {
            if (File.Exists(UnSavedDataFile))
            {
                File.Delete(UnSavedDataFile);
            }

            if (File.Exists(UnSavedDataCurrentCellFile))
            {
                File.Delete(UnSavedDataCurrentCellFile);
            }
        }

        private string UnSavedDataFile
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FreeLance", Path.GetFileNameWithoutExtension(_excelFilePath) + "_unsaved.txt");
            }
        }
        private string UnSavedDataCurrentCellFile
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FreeLance", Path.GetFileNameWithoutExtension(_excelFilePath) + "_unsaved_currentcell.txt");
            }
        }

        private void SaveRowInfo()
        {
            if (!rowsInfo.Any())
            {
                // return;
            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(rowsInfo);
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FreeLance", Path.GetFileNameWithoutExtension(_excelFilePath) + "_style.txt");
            File.WriteAllText(filePath, serializedResult);

        }

        private void ReadRowInfo()
        {
            var serializer = new JavaScriptSerializer();
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FreeLance", Path.GetFileNameWithoutExtension(_excelFilePath) + "_style.txt");

            if (File.Exists(filePath))
            {
                rowsInfo = serializer.Deserialize<List<RowInfo>>(File.ReadAllText(filePath));
                ApplyRowInfo();
            }
        }

        private void ApplyRowInfo()
        {
            foreach (var info in rowsInfo)
            {
                var rowIndex = GetRowWithId(info.RowId);
                ChaneRowColorToCadetBlue(rowIndex);
            }
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
            List<DataGridViewImageColumn> lst = new List<DataGridViewImageColumn>();

            foreach (var cell in imageColIndexs)
            {
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.HeaderText = "Image";
                imgColumn.Width = _imageSize + 20;

                dataGridView1.Columns.Insert(1, imgColumn);
                imgcolumns.Add(imgColumn);
                lst.Add(imgColumn);
              // imgColumn.Frozen = true;
            }

            foreach(DataGridViewImageColumn col in lst)
            {
                col.Frozen = true;
            }

        }

        private void LoadImageInCell()
        {
           //return;

            WebClient wc = new WebClient();
            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
            {

                for (var i = 0; i < imageColIndexs.Count; i++)
                {
                    if (dataGridView1.Rows[rowIndex].Cells[imageColIndexs[i]].Value != null)
                    {
                        var url = dataGridView1.Rows[rowIndex].Cells[imageColIndexs[i] + imageColIndexs.Count].Value.ToString();

                        if(string.IsNullOrEmpty(url))
                        {
                            continue;
                        }

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
            //Leave sorting for now
            //Sort2(e);
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



        private void Form1_Activated(object sender, EventArgs e)
        {
            if (imageColumnLoaded)
                return;

            GetImageColumns();
            AddImageColumn();
            LoadImageInCell();
            LoadColumnOrder();
            ReadRowInfo();
            LoadUnSavedData();

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
            SingleInstance.CloseFileMutex(_excelFilePath);
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

        private void Formula(EventArg arg)
        {
            FormulaWindow win = new FormulaWindow(dataGridView1);
            win.ShowDialog();
        }

        private void ShowStatistics(EventArg arg)
        {
            Dictionary<int, int> statics = new Dictionary<int, int>();
            var highLighted = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.CadetBlue)

                {
                    highLighted++;
                }

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!(dataGridView1.Columns[cell.ColumnIndex] is DataGridViewImageColumn))
                    {

                        if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                        {
                            if (statics.ContainsKey(cell.ColumnIndex))
                            {
                                statics[cell.ColumnIndex]++;
                            }
                            else
                            {
                                statics[cell.ColumnIndex] = 1;
                            }

                        }
                    }
                }
            }

            var tt = "";
            tt += Environment.NewLine + "HighLighted : " + highLighted.ToString();

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (!(col is DataGridViewImageColumn))
                {
                    if (statics.ContainsKey(col.Index))
                    {

                        tt += Environment.NewLine + col.Name + ": " + statics[col.Index];
                    }
                    else
                    {
                        tt += Environment.NewLine + col.Name + " : 0";
                    }
                }

            }

            MessageBox.Show(tt);
        }
    }
}
