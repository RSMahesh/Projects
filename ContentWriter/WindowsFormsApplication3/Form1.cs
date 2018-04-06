using System;
using System.Windows.Forms;
using StatusMaker.Data;
using System.Data;
using EventPublisher;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Diagnostics;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
       // bool IsReadOnlyFile;
        bool imageColumnLoaded = false;
        bool _importBackUp;
        bool autoSpellCheckMode;
        UndoRedoStack<CellData> undoRedo;

        OLDBConnection _dataConnection;
        Theme _theme = new Theme();
        WpfRichTextBox wpfRichText;
        ImageLoader imageLoader;
        ContextMenueMultiSelectCell contextMenueMultiSelectCell;
        ContextMenueCurrentCell contextMenueCurrentCell;
        ContextMenueColumnHeader contextMenueColumnHeader;
        FindAndReplace findAndReplace;
        SpellCheckWindow spellCheckWindow;
        PictureViewer frmPrectiureViwer = null;
        VenderWebSiteSerachSetting venderWebSiteSerachSetting;
        Search searchWindow;
        AppContext appContext;
        Statistics statistics;
        Styler styler;
        BackUp backUp;
        UnSavedData unSavedData;

        public Form1(string filePath, bool importBackUp = false, bool isReadOnly = false)
        {
            InitializeComponent();
            _importBackUp = importBackUp;

            wpfRichText = new WpfRichTextBox(wpfRichTextBoxPanel);
            appContext = new AppContext();
            appContext.dataGridView = dataGridView1;
            appContext.ExcelFilePath = filePath;
            appContext.ShowWpfRichTextBox = ShowWpfRichTextBox;
            appContext.wpfRichTextBox = wpfRichText;
            appContext.synonymProvider = new SynonymProvider();

            AddEventHandlersOfGridView();

            undoRedo = new UndoRedoStack<CellData>(new SetCellDataCommand(dataGridView1));
            contextMenueMultiSelectCell = new ContextMenueMultiSelectCell(dataGridView1, undoRedo);
            contextMenueCurrentCell = new ContextMenueCurrentCell(appContext);
            contextMenueColumnHeader = new ContextMenueColumnHeader(appContext);
            imageLoader = new ImageLoader(dataGridView1, appContext.ExcelFilePath);
            findAndReplace = new FindAndReplace(appContext);
            spellCheckWindow = new SpellCheckWindow(appContext);
            styler = new Styler(appContext);
            statistics = new Statistics(appContext, styler);
            backUp = new BackUp(appContext, this.MdiParent);
            searchWindow = new Search(this.MdiParent);
            venderWebSiteSerachSetting = new VenderWebSiteSerachSetting(appContext);
            unSavedData = new UnSavedData(appContext, undoRedo);

            wpfRichTextBoxPanel.Visible = false;
            appContext.IsReadOnlyFile = isReadOnly;
            SetReadOnly();
            SubScribeEvents();

            if (!appContext.IsReadOnlyFile)
            {
                Utility.CheckDataSavedProperly(appContext.ExcelFilePath);
            }
        }
        private void SetReadOnly()
        {
            if (Path.GetExtension(appContext.ExcelFilePath) == ".xml")
            {
                appContext.IsReadOnlyFile = true;
            }
            else
            {
                EventContainer.SubscribeEvent(EventPublisher.Events.StartDataImport.ToString(), backUp.StartBackUpImport);
            }
         
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = Constants.ImageIconSize;
            dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            _dataConnection = new OLDBConnection(appContext.ExcelFilePath);

            dataGridView1.DataSource = _dataConnection.ExecuteDatatable("Select * from [Sheet1$]").DefaultView;

            SetGridProperties();

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            if (appContext.IsReadOnlyFile)
            {
                dataGridView1.RowTemplate.DefaultCellStyle.BackColor = _theme.XmlBackGroundColor;
                dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = _theme.XmlForeColor;
                // dataGridView1.ReadOnly = true;
            }

            styler.LoadTheme(new EventArg(appContext.IsReadOnlyFile ? Theme.GetReadOnlyTheme() : Theme.GetDefaultTheme()));

            //this done because when user clicks on cell first time
            // its moves the cell focus to start as columns width get adusted
            // to avoid that work around is to call IncreaseRowHeight on load
            IncreaseRowHeight(1, 4);
            this.Text = appContext.ExcelFilePath;

            HideColumns();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (imageColumnLoaded)
                return;
            imageLoader.AddImageColumn();
            imageLoader.LoadImageInCell();
            LoadColumnOrder();
            styler.LoadHighLightRowsInfo();
            unSavedData.LoadUnSavedData();

            if (_importBackUp)
            {
                backUp.addCheckBox();
            }

            this.WindowState = FormWindowState.Maximized;
            imageColumnLoaded = true;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_importBackUp)
            {
                var dt = MarkAllDirty();
                EventContainer.PublishEvent(EventPublisher.Events.DataImportSelectionCompleted.ToString(), new EventArg(Guid.NewGuid(), dt));
            }

            this.Dispose();
            UnSubscribeEvents();
            SingleInstance.CloseFileMutex(appContext.ExcelFilePath);
        }
        private void SubScribeEvents()
        {
            if (!appContext.IsReadOnlyFile)
            {
                EventContainer.SubscribeEvent(EventPublisher.Events.CheckColumnsCausingSaveError.ToString(), CheckErrorColumns);
                EventContainer.SubscribeEvent(EventPublisher.Events.Save.ToString(), Save);
                EventContainer.SubscribeEvent(EventPublisher.Events.ShowFilterWindow.ToString(), ShowFilter);
                EventContainer.SubscribeEvent(EventPublisher.Events.FilterDone.ToString(), FilterDone);
                EventContainer.SubscribeEvent(EventPublisher.Events.ClearFilter.ToString(), ClearFilter);
                EventContainer.SubscribeEvent(EventPublisher.Events.Undo.ToString(), UnDo);
                EventContainer.SubscribeEvent(EventPublisher.Events.ReDo.ToString(), ReDo);
                EventContainer.SubscribeEvent(EventPublisher.Events.Statistics.ToString(), statistics.ShowStatistics);
                EventContainer.SubscribeEvent(EventPublisher.Events.Formula.ToString(), Formula);
                EventContainer.SubscribeEvent(EventPublisher.Events.SetenceCountInDescription.ToString(), statistics.SetenceCountInDescription);
                EventContainer.SubscribeEvent(EventPublisher.Events.SetenceCountInBullet.ToString(), statistics.SetenceCountInBullets);
                EventContainer.SubscribeEvent(EventPublisher.Events.FindText.ToString(), findAndReplace.FindText);
                EventContainer.SubscribeEvent(EventPublisher.Events.Relace.ToString(), findAndReplace.ReplaceText);
                EventContainer.SubscribeEvent(EventPublisher.Events.LoadTheme.ToString(), styler.LoadTheme);
                EventContainer.SubscribeEvent(EventPublisher.Events.WordsFrequency.ToString(), statistics.WordsFrequency);
                EventContainer.SubscribeEvent(EventPublisher.Events.ShowCharacterCountForColumn.ToString(), statistics.CharacterCount);

                EventContainer.SubscribeEvent(EventPublisher.Events.RichTextBoxTextChanged.ToString(), RichTextBoxTextChanged);
                EventContainer.SubscribeEvent(EventPublisher.Events.SearchTextInBackUp.ToString(), SearchText);
                EventContainer.SubscribeEvent(EventPublisher.Events.FindWindow.ToString(), ShowFindWindow);
                EventContainer.SubscribeEvent(EventPublisher.Events.ShowHideColumns.ToString(), ShowHideColumns);
                EventContainer.SubscribeEvent(EventPublisher.Events.ChangeBackGroundColor.ToString(), styler.ChangeBackGroundColor);
                EventContainer.SubscribeEvent(EventPublisher.Events.SpellCheck.ToString(), SpellCheck);
                EventContainer.SubscribeEvent(EventPublisher.Events.ToggleAutoSpellCheckMode.ToString(), ToggleAutoSpellCheckMode);
                EventContainer.SubscribeEvent(EventPublisher.Events.VendorWebSiteSearchSetting.ToString(), ShowVendorWebSiteSearchSetting);
            }
        }
        private void UnSubscribeEvents()
        {
            EventPublisher.EventContainer.UnSubscribeAll(this);
            EventPublisher.EventContainer.UnSubscribeAll(findAndReplace);
            EventPublisher.EventContainer.UnSubscribeAll(styler);
            EventPublisher.EventContainer.UnSubscribeAll(statistics);
        }
        private void AddEventHandlersOfGridView()
        {
            dataGridView1.DataError += DataGridView1_DataError;
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.CellLeave += dataGridView1_CellLeave;
            dataGridView1.CellEnter += DataGridView1_CellEnter; ;
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            dataGridView1.RowHeaderMouseClick += DataGridView1_RowHeaderMouseClick;
            dataGridView1.CellValidating += DataGridView1_CellValidating;
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
            dataGridView1.Sorted += DataGridView1_Sorted;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.ColumnWidthChanged += DataGridView1_ColumnWidthChanged;
            dataGridView1.Scroll += DataGridView1_Scroll;
            dataGridView1.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;
            dataGridView1.MouseClick += DataGridView1_MouseClick;
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            spellCheckWindow.Visible = false;
        }
        private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            HideWpfRichTextBox();
        }
        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentCell.OwningColumn.Name.
                Equals(venderWebSiteSerachSetting.CurrentSetting.ColumnName, StringComparison.OrdinalIgnoreCase))
            {
                OpenVendorWebSiteAndSerachItem(dataGridView1.CurrentCell.Value.ToString());
            }
        }
        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Load context menu on right mouse click
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                // contextMenu.Show(dataGridView1, e.Location);
                hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
                Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Point p = new Point(r.X + e.X, r.Y + e.Y);

                contextMenueColumnHeader.ShowMenu(p);
            }

            //if (e.Button == MouseButtons.Right)
            //{
            //    ToggleColumnForzing(e.ColumnIndex);
            //}
        }
        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            wpfRichTextBoxPanel.Visible = false;

            if (_importBackUp)
            {
                backUp.ResetCheckBoxLocation();
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
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!(dataGridView1[e.ColumnIndex, e.RowIndex] is DataGridViewImageCell))
            {
                if (imageColumnLoaded)
                {
                    unSavedData.SaveUnSavedData(new CellData(dataGridView1[e.ColumnIndex, e.RowIndex].Value, new Point(e.ColumnIndex, e.RowIndex)));
                }
            }
        }
        private void DataGridView1_Sorted(object sender, EventArgs e)
        {
            styler.HighLightRows();
        }
        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                styler.HighLightRow(int.Parse(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString()));

                if (dataGridView1.SelectedCells.Count > 1)
                {
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (e.RowIndex != cell.RowIndex)
                        {
                            styler.HighLightRow(int.Parse(dataGridView1.Rows[cell.RowIndex].Cells["ID"].Value.ToString()));
                        }
                    }
                }

                return;
            }

            IncreaseRowHeight(e.RowIndex, e.ColumnIndex);
        }
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

            if (autoSpellCheckMode)
            {
                ShowWpfRichTextBox();
            }
        }
        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!imageColumnLoaded ||
                appContext.IsReadOnlyFile ||
                dataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn)
            {
                return;
            }
       
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != e.FormattedValue.ToString())
            {
                var cellData = new CellData(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value,
                    new Point(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex));

                undoRedo.Do(cellData);
            }
        }
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
        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var data = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();

            var info = "Error : " + e.Exception.Message + Environment.NewLine +
                Environment.NewLine + " Row Number : " + (e.RowIndex + 1).ToString();

            MessageBox.Show(info);
            e.Cancel = true;
        }
        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            imageLoader.LoadImageInCell();
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateRowColorOnRowChange();
            dataGridView1.BeginEdit(false);
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
        void ShowVendorWebSiteSearchSetting(EventArg arg)
        {
            venderWebSiteSerachSetting.Show();
        }
        void ToggleAutoSpellCheckMode(EventArg arg)
        {
            autoSpellCheckMode = !autoSpellCheckMode;
        }
        void SpellCheck(EventArg arg)
        {
            this.Cursor = Cursors.WaitCursor;
            spellCheckWindow.Check(arg);
            this.Cursor = Cursors.Default;
        }

        DataGridViewRow lastCurrentRow;
        Color lastCurrentRowColor;
        private void UpdateRowColorOnRowChange()
        {
            if (lastCurrentRow != null)
            {
                lastCurrentRow.DefaultCellStyle.BackColor = lastCurrentRowColor;
            }

            lastCurrentRow = dataGridView1.CurrentRow;
            lastCurrentRowColor = lastCurrentRow.DefaultCellStyle.BackColor;

            if (dataGridView1.CurrentRow.DefaultCellStyle.BackColor != appContext.Theme.HighlightedRowColor)
            {
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = appContext.Theme.CurrentRowColor;
            }

        }
        private void Formula(EventArg arg)
        {
            FormulaList win = new FormulaList(null);
            win.ShowDialog();
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
        private void Save(EventArg obj)
        {
            if (!this.Visible || appContext.IsReadOnlyFile)
            {
                return;
            }

            var index = dataGridView1.CurrentRow.Index;
            var Colindex = dataGridView1.CurrentCell.ColumnIndex;

            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            dataGridView1.CurrentRow.Selected = false;

            //This heck is needed to saved current cell value which user has cahnged
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
            styler.SaveRowInfo();
            unSavedData.DeleteUnSavedDataFile();
            RaiseEventForChange();
        }
        private void CheckErrorColumns(EventArg obj)
        {
            if (!this.Visible || appContext.IsReadOnlyFile)
            {
                return;
            }

            var index = dataGridView1.CurrentRow.Index;
            var Colindex = dataGridView1.CurrentCell.ColumnIndex;

            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            dataGridView1.CurrentRow.Selected = false;

            //This heck is needed to saved current cell value which user has cahnged
            if (dataGridView1.Rows.Count == index + 1)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[index - 1].Cells[1];
            }
            else
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[index + 1].Cells[1];
            }


            var tbl = ((DataView)dataGridView1.DataSource).Table;
            _dataConnection.CheckForError(tbl.Columns);

            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[Colindex];
            dataGridView1.Rows[index].Selected = true;
            MessageBox.Show("DDDD");
            styler.SaveRowInfo();
            unSavedData.DeleteUnSavedDataFile();
            RaiseEventForChange();
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
        private void CellDataChanged(CellData cellData)
        {
            undoRedo.Do(cellData);

            unSavedData.SaveUnSavedData(cellData);
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
            undoRedo.Undo();
        }
        private void ReDo(EventPublisher.EventArg arg)
        {
            undoRedo.Redo();
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
            wpfRichText.Text = dataGridView1.CurrentCell.Value.ToString();
            wpfRichTextBoxPanel.Size = rec.Size;
            wpfRichTextBoxPanel.Location = rec.Location;
            wpfRichTextBoxPanel.Visible = true;
        }
        private void HideWpfRichTextBox()
        {
            wpfRichTextBoxPanel.Visible = false;
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
        private void IncreaseRowHeight(int row, int col, bool toogle = true)
        {
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

            dataGridView1.Rows[row].Height = dataGridView1.Rows[row].Height > GetRowHeightBasedOnLength(maxLength) ?
                dataGridView1.Rows[row].Height : GetRowHeightBasedOnLength(maxLength);
        }
        private int GetRowHeightBasedOnLength(int maxColumnTextLength)
        {
            return (int)Math.Abs(maxColumnTextLength / 4) + 10;
        }
        private void SetGridProperties()
        {
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
        private void ToggleColumnForzing(int columnIndex)
        {
            if (dataGridView1.Columns[columnIndex].Name == "ID")
            {
                return;
            }

            if (dataGridView1.Columns[columnIndex].Frozen)
            {
                dataGridView1.Columns[columnIndex].Frozen = false;
                dataGridView1.Columns[columnIndex].DisplayIndex = (int)dataGridView1.Columns[columnIndex].Tag;
            }
            else
            {
                dataGridView1.Columns[columnIndex].Tag = dataGridView1.Columns[columnIndex].DisplayIndex;
                dataGridView1.Columns[columnIndex].DisplayIndex = GetNextForzenIndex();
                dataGridView1.Columns[columnIndex].Frozen = true;
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
        private void OpenVendorWebSiteAndSerachItem(string valueToSearch)
        {
            var exlcudeWordsInSearch = venderWebSiteSerachSetting.CurrentSetting.ReplaceWords.Split(',');

            foreach (var excludeWord in exlcudeWordsInSearch)
            {
                valueToSearch = valueToSearch.Replace(excludeWord, "");
            }

            Process.Start(venderWebSiteSerachSetting.CurrentSetting.Url + valueToSearch);
        }
    }
}
