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
using System.Configuration;
using System.Diagnostics;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        string _excelFilePath;
        OLDBConnection12 _dataConnection;

        bool IsReadOnlyFile;
        bool imageColumnLoaded = false;
        // ContextMenu contextMenu = new ContextMenu();
        UndoRedoStack<CellData> _undoRedo;
        bool _importBackUp;
        List<RowInfo> rowsInfo = new List<RowInfo>();
        Theme _theme = new Theme();
        const string HighLighted = "highlighted";
        WpfRichTextBox wpfRichText;
        ImageLoader imageLoader;
        ContextMenueMultiSelectCell contextMenueMultiSelectCell;
        ContextMenueCurrentCell contextMenueCurrentCell;
        FindAndReplace findAndReplace;
        PictureViewer frmPrectiureViwer = null;
        Search searchWindow;
        AppContext appContext;
        string urlToSerach = "https://www.foagroup.com/catalogsearch/result/?q=";
        string[] exlcudeWordsInSearch = new[] { "FOA-", "xyz" };
        string columnNameToSearch = "Vendor SKU";

        public Form1(string filePath, bool importBackUp = false, bool isReadOnly = false)
        {

            urlToSerach = ConfigurationManager.AppSettings["urlToSerach"];
            exlcudeWordsInSearch = ConfigurationManager.AppSettings["exlcudeWordsInSearch"].Split(',');
            columnNameToSearch = ConfigurationManager.AppSettings["columnNameToSearch"];

            InitializeComponent();

            appContext = new AppContext();
            appContext.dataGridView = dataGridView1;
            appContext.ExcelFilePath = filePath;


            wpfRichText = new WpfRichTextBox(wpfRichTextBoxPanel);

            _importBackUp = importBackUp;
            _undoRedo = new UndoRedoStack<CellData>(new SetCellDataCommand(dataGridView1));

            AddEventsHandlersOfUIControls();

            contextMenueMultiSelectCell = new ContextMenueMultiSelectCell(dataGridView1, _undoRedo);
            contextMenueCurrentCell = new ContextMenueCurrentCell(appContext);

            _excelFilePath = filePath;

            imageLoader = new ImageLoader(dataGridView1, _excelFilePath);
            findAndReplace = new FindAndReplace(dataGridView1, wpfRichText, ShowWpfRichTextBox);

            wpfRichTextBoxPanel.Visible = false;

            IsReadOnlyFile = isReadOnly;

            if (Path.GetExtension(_excelFilePath) == ".xml")
            {
                IsReadOnlyFile = true;
            }
            else
            {
                EventContainer.SubscribeEvent(EventPublisher.Events.StartDataImport.ToString(), StartBackUpImport);
            }
            SubScribeEvents();

            if (!IsReadOnlyFile)
            {

                Utility.CheckDataSavedProperly(_excelFilePath);
            }

            searchWindow = new Search(this.MdiParent);

           
        }

        private void HideColumns()
        {
            try
            {
                new ColumnCustomization(appContext).ShowHideGridColums();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Show Hide Column Faild" + Environment.NewLine + "Error:" + ex);
            }
        }
        private void AddEventsHandlersOfUIControls()
        {
            dataGridView1.DataError += DataGridView1_DataError;
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.CellLeave += dataGridView1_CellLeave;
            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            dataGridView1.RowHeaderMouseClick += DataGridView1_RowHeaderMouseClick;
            dataGridView1.CellValidating += DataGridView1_CellValidating;
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
            dataGridView1.Sorted += DataGridView1_Sorted;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.ColumnWidthChanged += DataGridView1_ColumnWidthChanged;
            dataGridView1.RowHeightChanged += DataGridView1_RowHeightChanged;
            dataGridView1.Scroll += DataGridView1_Scroll;
            dataGridView1.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;
            dataGridView1.MouseClick += DataGridView1_MouseClick;
        }
        private void ShowFindWindow(EventArg arg)
        {
            FindText frm = new FindText(appContext);
            frm.TopMost = true;
            frm.Show();
        }

        private void ShowHideColumns(EventArg arg)
        {
            ColumnCustomization frm = new ColumnCustomization(appContext);
            frm.TopMost = true;
            frm.Show();
        }

        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (
                dataGridView1.CurrentCell.OwningColumn.Name.Equals(columnNameToSearch, StringComparison.OrdinalIgnoreCase)
                )
            {
                string tt = urlToSerach;
                var vv = dataGridView1.CurrentCell.Value.ToString();

                foreach (var word in exlcudeWordsInSearch)
                {
                    vv = vv.Replace(word, "");
                }
                tt += vv;
                Process.Start(tt);
            }
        }

        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (
                //dataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn ||
                dataGridView1.Columns[e.ColumnIndex].Name == "ID")
                {
                    return;
                }

                if (dataGridView1.Columns[e.ColumnIndex].Frozen)
                {
                    dataGridView1.Columns[e.ColumnIndex].Frozen = false;
                    dataGridView1.Columns[e.ColumnIndex].DisplayIndex = (int)dataGridView1.Columns[e.ColumnIndex].Tag;
                    //   dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Black;
                }
                else
                {

                    dataGridView1.Columns[e.ColumnIndex].Tag = dataGridView1.Columns[e.ColumnIndex].DisplayIndex;

                    dataGridView1.Columns[e.ColumnIndex].DisplayIndex = GetNextForzenIndex();
                    //       dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.AliceBlue;
                    dataGridView1.Columns[e.ColumnIndex].Frozen = true;

                }

                //dataGridView1.Columns[e.ColumnIndex].Frozen = !dataGridView1.Columns[e.ColumnIndex].Frozen;
            }
        }

        private int GetNextForzenIndex()
        {
            int index = 0;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Frozen)
                {
                    index++;
                }
            }

            return index;
        }

        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            wpfRichTextBoxPanel.Visible = false;

            if (_importBackUp)
            {
                ResetCheckBoxLocation();
            }
        }

        private void DataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.Name == Constants.WordFrequencyColumnName)
            {
                if (e.Column.Width < 30)
                {
                    e.Column.Visible = false;
                }
            }

            // ShowWpfRichTextBox();
        }

        private void DataGridView1_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            //ShowWpfRichTextBox();
        }


        private void SubScribeEvents()
        {

            if (!IsReadOnlyFile)
            {

                EventContainer.SubscribeEvent(EventPublisher.Events.Save.ToString(), Save);
                EventContainer.SubscribeEvent(EventPublisher.Events.DescriptionCount.ToString(), DescriptionCount);
                EventContainer.SubscribeEvent(EventPublisher.Events.ShowFilterWindow.ToString(), ShowFilter);
                EventContainer.SubscribeEvent(EventPublisher.Events.FilterDone.ToString(), FilterDone);
                EventContainer.SubscribeEvent(EventPublisher.Events.ClearFilter.ToString(), ClearFilter);
                EventContainer.SubscribeEvent(EventPublisher.Events.Undo.ToString(), UnDo);
                EventContainer.SubscribeEvent(EventPublisher.Events.ReDo.ToString(), ReDo);
                EventContainer.SubscribeEvent(EventPublisher.Events.Statistics.ToString(), ShowStatistics);
                EventContainer.SubscribeEvent(EventPublisher.Events.Formula.ToString(), Formula);
                EventContainer.SubscribeEvent(EventPublisher.Events.SetenceCountInDescription.ToString(), SetenceCountInDescription);
                EventContainer.SubscribeEvent(EventPublisher.Events.SetenceCountInBullet.ToString(), SetenceCountInBullets);
                EventContainer.SubscribeEvent(EventPublisher.Events.FindText.ToString(), findAndReplace.FindText);
                EventContainer.SubscribeEvent(EventPublisher.Events.Relace.ToString(), findAndReplace.ReplaceText);
                EventContainer.SubscribeEvent(EventPublisher.Events.LoadTheme.ToString(), LoadTheme);
                EventContainer.SubscribeEvent(EventPublisher.Events.WordsFrequency.ToString(), ShowSetenceCount);
                EventContainer.SubscribeEvent(EventPublisher.Events.RichTextBoxTextChanged.ToString(), RichTextBoxTextChanged);
                EventContainer.SubscribeEvent(EventPublisher.Events.SearchTextInBackUp.ToString(), SearchText);
                EventContainer.SubscribeEvent(EventPublisher.Events.FindWindow.ToString(), ShowFindWindow);
                EventContainer.SubscribeEvent(EventPublisher.Events.ShowHideColumns.ToString(), ShowHideColumns);
                EventContainer.SubscribeEvent(EventPublisher.Events.ChangeBackGroundColor.ToString(), ChangeBackGroundColor);

            }

        }

        void LoadTheme(EventArg arg)
        {
            _theme = (Theme)arg.Arg;
            appContext.Theme = _theme;
            this.dataGridView1.RowTemplate.DefaultCellStyle.BackColor = _theme.BackGroundColor;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 11F);
            this.dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = _theme.ForeColor;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = _theme.SelectionBackColor;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = _theme.SelectionForeColor;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.DefaultCellStyle.BackColor = _theme.BackGroundColor;
                row.DefaultCellStyle.ForeColor = _theme.ForeColor;

                if (row.Tag == HighLighted)
                {
                    row.DefaultCellStyle.BackColor = _theme.HighlightedRowColor;
                }

            }

            wpfRichText.BackgroundColor = Utility.ToMediaColor(_theme.BackGroundColor);
            wpfRichText.BorderColor = Utility.ToMediaColor(_theme.CurrentCellBorderColor);
            wpfRichText.ForegroundColor = Utility.ToMediaColor(_theme.ForeColor);
        }

        void ChangeBackGroundColor(EventArg arg)
        {
            var bgColor = (Color)arg.Arg;
            _theme.BackGroundColor = bgColor;
            _theme.HighlightedRowColor = _theme.SelectionBackColor = _theme.CurrentRowColor = bgColor;

            LoadTheme(new EventArg(_theme));
        }


        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!(dataGridView1[e.ColumnIndex, e.RowIndex] is DataGridViewImageCell))
            {
                SaveUnSavedData(new CellData(dataGridView1[e.ColumnIndex, e.RowIndex].Value, new Point(e.ColumnIndex, e.RowIndex)));
            }
        }

        private void DataGridView1_Sorted(object sender, EventArgs e)
        {
            ApplyRowInfo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            dataGridView1.RowTemplate.Height = Constants.ImageIconSize;
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



            if (IsReadOnlyFile)
            {
                dataGridView1.RowTemplate.DefaultCellStyle.BackColor = _theme.XmlBackGroundColor;
                dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = _theme.XmlForeColor;
                // dataGridView1.ReadOnly = true;
            }

            LoadTheme(new EventArg(IsReadOnlyFile ? Theme.GetReadOnlyTheme() : Theme.GetDefaultTheme()));

            //this done because when user clicks on cell first time
            // its moves the cell focus to start as columns width get adusted
            // to avoid that work around is to call IncreaseRowHeight on load
            IncreaseRowHeight(1, 4);
            this.Text = _excelFilePath;

            HideColumns();
        }

        private string GetSheetName()
        {
            var sheets = _dataConnection.GetSheets();

            if (sheets.Count < 2)
            {
                return sheets.FirstOrDefault();
            }

            MessageBox.Show("More then on Sheets. Name : " + string.Join(",", sheets));


            foreach (string sheet in sheets)
            {

                DialogResult dialogResult =
                    MessageBox.Show("Do you want to Open " + sheet, "Open Sheet", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    return sheet;
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }

            throw new Exception("No Sheet Selected");
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

                return;
            }

            IncreaseRowHeight(e.RowIndex, e.ColumnIndex);

        }

        private void HighLightRow(int rowId)
        {
            var rowIndex = GetRowWithId(rowId);

            if (dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor == _theme.HighlightedRowColor)
            {
                UnHighlightRow(rowId);
            }
            else
            {
                HighLightRowColor(rowIndex);
                RowInfo rowInfo = new RowInfo();
                rowInfo.Highlighted = true;
                rowInfo.RowId = rowId;
                rowsInfo.Add(rowInfo);
            }
        }

        private void UnHighlightRow(int rowId)
        {
            var rowIndex = GetRowWithId(rowId);
            dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = _theme.BackGroundColor;
            dataGridView1.Rows[rowIndex].Tag = string.Empty;
            dataGridView1.Rows[rowIndex].Cells["ColorCode"].Value = string.Empty;
            rowsInfo.Remove(rowsInfo.FirstOrDefault(x => x.RowId == rowId));
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

        private void HighLightRowColor(int rowIndex)
        {
            dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = _theme.HighlightedRowColor;
            dataGridView1.Rows[rowIndex].Tag = HighLighted;
            dataGridView1.Rows[rowIndex].Cells["ColorCode"].Value = _theme.HighlightedRowColor.ToString();
        }

        private void IncreaseRowHeight(int row, int col, bool toogle = true)
        {
            //  return;
            int maxLength = 0;

            foreach (DataGridViewCell cell in dataGridView1.Rows[row].Cells)
            {
                if (cell.Value != null && !(dataGridView1.Columns[cell.ColumnIndex] is DataGridViewImageColumn))
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
                dataGridView1.Columns[e.ColumnIndex].Width = dataGridView1.Columns[e.ColumnIndex].Width + 1;
                dataGridView1[e.ColumnIndex, e.RowIndex].Selected = true;
            }
        }


        bool attachedEventFroKepUp = false;
        DataGridViewTextBoxEditingControl editingTextBox;
        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!attachedEventFroKepUp)
            {
                editingTextBox = (DataGridViewTextBoxEditingControl)e.Control;
                editingTextBox.MouseClick += EditingTextBox_MouseClick;
                attachedEventFroKepUp = true;
                appContext.dataGridViewTextBoxEditing = editingTextBox;
            }

            wpfRichTextBoxPanel.Visible = false;
        }

        private void EditingTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            StartHighlightingWords();
        }

        private void ShowWpfRichTextBox()
        {
            if (!imageColumnLoaded || editingTextBox == null || dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == Constants.WordFrequencyColumnName)
                return;
            var rec = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true);
            wpfRichText.Text = editingTextBox.Text;
            wpfRichTextBoxPanel.Size = rec.Size;
            wpfRichTextBoxPanel.Location = rec.Location;
            wpfRichTextBoxPanel.Visible = true;
        }

        private void StartHighlightingWords()
        {
            try
            {
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == Constants.WordFrequencyColumnName)
                {
                    int start = 0;

                    if (editingTextBox.SelectionStart > 0)
                    {
                        start = editingTextBox.Text.LastIndexOf(Environment.NewLine, editingTextBox.SelectionStart);
                        if (start == -1)
                        {
                            start = 0;
                        }
                    }

                    var end = editingTextBox.Text.IndexOf(' ', start);

                    var searchText = editingTextBox.Text.Substring(start, (end - start) + 1);

                    searchText = searchText.Replace(Environment.NewLine, string.Empty);



                    var colIndex = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].DisplayIndex - 1;
                    var cell = dataGridView1[colIndex, dataGridView1.CurrentCell.RowIndex];
                    dataGridView1.CurrentCell = cell;
                    dataGridView1.BeginEdit(false);
                    ShowWpfRichTextBox();

                    wpfRichText.HighlightWholeWordAll(searchText);

                }
            }
            catch (Exception ex)
            { }

        }

        private void Tb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Z))
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)sender;
                tb.Undo();
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

            if (IsReadOnlyFile)
                return;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != e.FormattedValue.ToString())
            {
                var cellData = new CellData(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, new Point(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex));
                _undoRedo.Do(cellData);
            }
        }

        private void CellDataChanged(CellData cellData)
        {
            _undoRedo.Do(cellData);

            SaveUnSavedData(cellData);
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
                using (Pen p = new Pen(_theme.CurrentCellBorderColor, 2))
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(p, rect);
                }
                e.Handled = true;
            }
            else if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index == e.RowIndex)
            {
                return;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All
 & ~DataGridViewPaintParts.Border);


                using (Pen p = new Pen(dataGridView1.GridColor, 1))
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    Rectangle rect = e.CellBounds;
                    rect.X -= 1;
                    // rect.Width += 1;
                    //  rect.Height -= 1;

                    e.Graphics.DrawLine(p, new Point(rect.Left, rect.Top), new Point(rect.Left, rect.Bottom));
                    e.Graphics.DrawLine(p, new Point(rect.Right, rect.Top), new Point(rect.Right, rect.Bottom));
                }

                using (Pen p = new Pen(Color.Red, 1))
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    Rectangle rect = e.CellBounds;
                    //rect.Y -= 1;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    //  e.Graphics.DrawRectangle(p, rect);
                    e.Graphics.DrawLine(p, new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Top));
                    e.Graphics.DrawLine(p, new Point(rect.Left, rect.Bottom), new Point(rect.Right, rect.Bottom));
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

                var currentCellHited = e.ColumnIndex == dataGridView1.CurrentCell.ColumnIndex
                        && e.RowIndex == dataGridView1.CurrentCell.RowIndex;

                if (dataGridView1.SelectedCells.Count > 1)
                {
                    contextMenueMultiSelectCell.ShowMenu(p);
                }
                else if (currentCellHited)
                {
                    contextMenueCurrentCell.ShowMenu(p);
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

            if (IsReadOnlyFile)
            {
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
            _dataConnection.SaveData(tbl.Columns);

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
            if ((!File.Exists(UnSavedDataFile) && !File.Exists(UnSavedDataCurrentCellFile))
                || IsReadOnlyFile
                || ConfigurationManager.AppSettings["AlertForUnSavedState"] == "false")
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
                return Path.Combine(Utility.FreeLanceAppDataFolder, Path.GetFileNameWithoutExtension(_excelFilePath) + "_unsaved.txt");
            }
        }
        private string UnSavedDataCurrentCellFile
        {
            get
            {
                return Path.Combine(Utility.FreeLanceAppDataFolder, Path.GetFileNameWithoutExtension(_excelFilePath) + "_unsaved_currentcell.txt");
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
            var filePath = Path.Combine(Utility.FreeLanceAppDataFolder, Path.GetFileNameWithoutExtension(_excelFilePath) + "_style.txt");
            //  MessageBox.Show(serializedResult);
            File.WriteAllText(filePath, serializedResult);

        }

        private void ReadRowInfo()
        {
            var serializer = new JavaScriptSerializer();
            var filePath = Path.Combine(Utility.FreeLanceAppDataFolder, Path.GetFileNameWithoutExtension(_excelFilePath) + "_style.txt");

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
                HighLightRowColor(rowIndex);
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
                int rowId;
                if (int.TryParse(row.Cells["ID"].Value.ToString(), out rowId))
                {
                    var rowNumber = rowId;
                    var desRow = dt2.NewRow();
                    var sourceRow = dt.Rows[--rowNumber];
                    desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
                    dt2.Rows.Add(desRow);
                }
            });

            return dt2;
        }

        CheckBox HeaderCheckBox;
        private void addCheckBox()
        {
            DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
            dgvCmb.ValueType = typeof(bool);
            dgvCmb.Name = "Chk";
            dgvCmb.HeaderText = "";
            dgvCmb.Width = 60;
            dataGridView1.Columns.Add(dgvCmb);

            HeaderCheckBox = new CheckBox();

            HeaderCheckBox.Size = new Size(15, 15);

            HeaderCheckBox.CheckedChanged += HeaderCheckBox_CheckedChanged;

            //Add the CheckBox into the DataGridView
            dataGridView1.Controls.Add(HeaderCheckBox);

            ResetCheckBoxLocation();
        }

        private void HeaderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells[dataGridView1.Columns.Count - 1]).Value = HeaderCheckBox.Checked;

            }
        }

        private void ResetCheckBoxLocation()
        {
            //Get the column header cell bounds
            Rectangle oRectangle =
             dataGridView1.GetCellDisplayRectangle(dataGridView1.Columns.Count - 1, -1, true);
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

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var data = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();

            var info = "Error : " + e.Exception.Message + Environment.NewLine +
                Environment.NewLine + " Row Number : " + (e.RowIndex + 1).ToString();

            MessageBox.Show(info);
            e.Cancel = true;
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
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (imageColumnLoaded)
                return;
            imageLoader.AddImageColumn();
            imageLoader.LoadImageInCell();
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
            Filter filter = new Filter(appContext);
            filter.WindowState = FormWindowState.Normal;
            filter.Show();
        }

        private void ClearFilter(EventPublisher.EventArg arg)
        {
            ((DataView)dataGridView1.DataSource).RowFilter = string.Empty;
            imageLoader.LoadImageInCell();
        }

        private void FilterDone(EventPublisher.EventArg arg)
        {
            imageLoader.LoadImageInCell();
        }

        DataGridViewRow lastCurrentRow;
        Color lastCurrentRowColor;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lastCurrentRow != null)
            {
                lastCurrentRow.DefaultCellStyle.BackColor = lastCurrentRowColor;
            }


            lastCurrentRow = dataGridView1.CurrentRow;
            lastCurrentRowColor = lastCurrentRow.DefaultCellStyle.BackColor;

            if (dataGridView1.CurrentRow.DefaultCellStyle.BackColor != _theme.HighlightedRowColor)
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = _theme.CurrentRowColor;

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

            openFileDialog.InitialDirectory =
                Path.GetDirectoryName(_excelFilePath) + "\\" +
                        Path.GetFileNameWithoutExtension(_excelFilePath)
                        + "_dataBackup\\Full";

            string fileName = openFileDialog.FileName;

            if (arg.Arg != null)
            {
                fileName = arg.Arg.ToString();
                EventContainer.SubscribeEvent(EventPublisher.Events.DataImportSelectionCompleted.ToString(), OnDataImportSelectionCompleted);

            }

            else
            {
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    EventContainer.SubscribeEvent(EventPublisher.Events.DataImportSelectionCompleted.ToString(), OnDataImportSelectionCompleted);

                }
            }


            var form1 = new Form1(fileName, true);
            form1.MdiParent = this.MdiParent;
            form1.Show();



        }
        private void OnDataImportSelectionCompleted(EventArg arg)
        {
            DataTable backUpDt = (DataTable)arg.Arg;

            var currentDt = ((DataView)dataGridView1.DataSource).Table;

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


        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            imageLoader.LoadImageInCell();
        }

        private void Formula(EventArg arg)
        {
            FormulaList win = new FormulaList(null);
            win.ShowDialog();
        }
        private void SetenceCountInDescription(EventArg arg)
        {

            var rowsWithLessSentenceCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                UnHighlightRow(int.Parse(row.Cells["ID"].Value.ToString()));


                var count = GetSetenceCount(row.Cells["Description"].Value.ToString());

                if (count < 3)
                {
                    HighLightRow(int.Parse(row.Cells["ID"].Value.ToString()));
                    rowsWithLessSentenceCount++;
                }

            }
            MessageBox.Show("Description with less then 3 sentence are :" + rowsWithLessSentenceCount);
        }

        private void ShowSetenceCount()
        {
            var colIndex = dataGridView1.CurrentCell.ColumnIndex;
            dataGridView1.Columns[Constants.WordFrequencyColumnName].Visible = true;
            dataGridView1.Columns[Constants.WordFrequencyColumnName].DisplayIndex = colIndex + 1;

            dataGridView1.Columns[Constants.WordFrequencyColumnName].Width = 200;

            var dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            var exludeWordsFrequencyCheckFile = Path.Combine(dir, "ExludeWordsFrequencyCheck.txt");
            IEnumerable<string> exludeWords = new List<string>();
            if (File.Exists(exludeWordsFrequencyCheckFile))
            {
                exludeWords = File.ReadAllText(exludeWordsFrequencyCheckFile).Replace(Environment.NewLine, "").Split(',');
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                row.Cells[Constants.WordFrequencyColumnName].Value = Utility.CountWordsInString(row.Cells[colIndex].Value.ToString(), exludeWords);
            }
        }

        private void ShowSetenceCount(EventArg arg)
        {
            ShowSetenceCount();
        }

        private void RichTextBoxTextChanged(EventArg arg)
        {
            dataGridView1.CurrentCell.Value = arg.Arg.ToString();
        }

        private void SearchText(EventArg arg)
        {
            if (searchWindow.IsDisposed)
            {
                searchWindow = new Search(this.MdiParent);
            }
            searchWindow.MdiParent = this.MdiParent;
            searchWindow.Show();
            searchWindow.ShowResult(arg.Arg.ToString());
        }
        private void SetenceCountInBullets(EventArg arg)
        {
            int threshold = 0;

            var columsToCheck = GetColumsToCheckForSentenceCount();

            var rowsWithLessSentenceCount = 0;
            var fixedCell = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                UnHighlightRow(int.Parse(row.Cells["ID"].Value.ToString()));

                foreach (var columnName in columsToCheck)
                {
                    var cell = row.Cells[columnName];

                    if (cell.Value.ToString().StartsWith("Dimensions:"))
                    {
                        continue;
                    }

                    var count = GetSetenceCount(cell.Value.ToString());

                    if (count == 1 && cell.Value.ToString().EndsWith("."))
                    {
                        cell.Value = cell.Value.ToString().TrimEnd('.');
                        fixedCell++;
                        continue;
                    }

                    if (count > threshold)
                    {
                        HighLightRow(int.Parse(row.Cells["ID"].Value.ToString()));
                        cell.Style.BackColor = Color.AntiqueWhite;
                        rowsWithLessSentenceCount++;
                    }
                }

            }

            var msg = "Bullets with more then 0 sentence are :" + rowsWithLessSentenceCount;
            msg += Environment.NewLine + "Fixed Bullets : " + fixedCell;

            MessageBox.Show(msg);
        }

        private IEnumerable<string> GetColumsToCheckForSentenceCount()
        {
            List<string> lst = new List<string>();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name.StartsWith("Bullet", StringComparison.OrdinalIgnoreCase))
                {
                    lst.Add(column.Name);
                }
            }

            return lst.AsEnumerable();
        }

        private int GetSetenceCount(string text)
        {
            var c = text.Count(f => f == '.');
            return c;
        }

        private void ShowStatistics(EventArg arg)
        {
            Dictionary<int, int> statics = new Dictionary<int, int>();
            var highLighted = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.DefaultCellStyle.BackColor == _theme.HighlightedRowColor)
                {
                    highLighted++;
                }

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!(dataGridView1.Columns[cell.ColumnIndex] is DataGridViewImageColumn))
                    {
                        if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString().Trim()))
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
