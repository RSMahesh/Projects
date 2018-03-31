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
    partial class SpellCheckWindow : Form
    {
        int startingRowToFind = 0;
        int startingColumnToFind = 0;
        int startingIndexInCellToFindText = 0;
        DataGridView dataGridView;
        WpfRichTextBox wpfRichText;
        Action showWpfRichTextBox;
        AppContext appContext;
        CustomDictionary customDictionary;

        ContextMenu contextMenu = new ContextMenu();
        MenuItem menuItemSynonyms;
        public SpellCheckWindow(AppContext appContext)
        {
            InitializeComponent();
            this.textBox1.ReadOnly = true;
            this.dataGridView = appContext.dataGridView;
            this.wpfRichText = appContext.wpfRichTextBox;
            this.showWpfRichTextBox = appContext.ShowWpfRichTextBox;
            this.FormClosing += SpellCheckWindow_FormClosing;
            this.appContext = appContext;
            customDictionary = new CustomDictionary();
            CustomDictionaryUpdate(null);
            LoadColumns();
            checkedListBox1.MultiColumn = false;
            EventContainer.SubscribeEvent(EventPublisher.Events.CustomDictionaryUpdate.ToString(), CustomDictionaryUpdate);
            listBox1.ContextMenu = contextMenu;
            menuItemSynonyms = contextMenu.MenuItems.Add("Synonyms");
            menuItemSynonyms.Select += MenuItemSynonyms_Select;

        }

        private void MenuItemSynonyms_Select(object sender, EventArgs e)
        {
            appContext.synonymProvider.AddSynoms(listBox1.SelectedItem.ToString(),
                   menuItemSynonyms, OnSynonymClick);
        }

        private void OnSynonymClick(object sender, EventArgs e)
        {
           // var menuIltem = (MenuItem)sender;

         //   listBox1.Items.Remove(listBox1.SelectedItem);

          //  appContext.dataGridViewTextBoxEditing.Paste(menuIltem.Text);
        }

        private void CustomDictionaryUpdate(EventArg obj)
        {
            appContext.wpfRichTextBox.LoadCustomDic(customDictionary.CustomDictionaryUri);
        }
        private void SpellCheckWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void SpellCheckWindow_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        public void Check(EventArg arg)
        {
            LoadColumns();
            //var arr = arg.Arg as string;
            //var columnName = arr;

            //TODO: remove 
            var columnName = "ALL";
            this.Cursor = Cursors.WaitCursor;
            for (var rowIndex = startingRowToFind; rowIndex < dataGridView.Rows.Count; rowIndex++)
            {
                DataGridViewRow row = dataGridView.Rows[rowIndex];

                for (var colIndex = startingColumnToFind; colIndex < dataGridView.Rows[rowIndex].Cells.Count; colIndex++)
                {

                    if (IsNotColumnSelected(dataGridView.Columns[colIndex].Name))
                    {
                        continue;
                    }

                    var cell = dataGridView.Rows[rowIndex].Cells[colIndex];

                    if (!(dataGridView.Columns[colIndex] is DataGridViewImageColumn))
                    {
                        if (cell.Visible && cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                        {

                            if(!wpfRichText.DoesContainSpellErrors123(cell.Value.ToString()))
                            {
                                continue;
                            }
                          //  MessageBox.Show("c" + cell.ColumnIndex + " R:" + cell.RowIndex);
                            dataGridView.CurrentCell = cell;
                            dataGridView.BeginEdit(false);
                            appContext.ShowWpfRichTextBox();
                            if (wpfRichText.IsSpellErrors())
                            {

                                startingRowToFind = rowIndex;
                                startingColumnToFind = colIndex;
                               
                                ShowSpellError();
                                this.Show();
                                var rec = appContext.dataGridView.GetCellDisplayRectangle(dataGridView.CurrentCell.ColumnIndex, dataGridView.CurrentCell.RowIndex, true);

                                var screen = Screen.FromControl(this);
                                

                                if (rec.IntersectsWith(this.Bounds))
                                {
                                    this.Left += 200;
                                    this.Top += 200;

                                    if(this.Right > screen.Bounds.Right)
                                    {
                                      this.Left -=600;
                                    }
                                    if (this.Bottom > screen.Bounds.Bottom)
                                    {
                                        this.Top -= 600;
                                    }
                                }
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            else
                            {
                               // MessageBox.Show("No Error 2");
                            }

                        }
                    }
                }

                startingColumnToFind = 0;
            }

            MessageBox.Show("No Spell Error");
            this.Cursor = Cursors.Default;
            startingColumnToFind = startingRowToFind = startingIndexInCellToFindText = 0;
        }

        private void ShowIfHidden()
        {
           if( this.Visible == false)
            {
                this.Show();
            }
        }

        private bool IsNotColumnSelected(string columnName)
        {
            for(int i =0; i < checkedListBox1.Items.Count; i++)
            {
                if(checkedListBox1.GetItemChecked(i) &&
                    checkedListBox1.Items[i].ToString().Equals(columnName))
                {
                    return false;
                }
            }

            return true;
        }

        public void OnCellBeginEdit(int cellColIndex, int cellRowIndex)
        {
            if(cellColIndex != startingColumnToFind  && cellRowIndex != startingRowToFind)
            {
                btnChange.Enabled = false;
            }
        }

        private void ShowSpellError()
        {
            textBox1.Text = wpfRichText.SpellErrorText;
            listBox1.Items.Clear();

            foreach(string suggestion in wpfRichText.spellingError.Suggestions)
            {
                listBox1.Items.Add(suggestion);
            }

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
                btnChange.Enabled = true;
            }
            else
            {
                btnChange.Enabled = false;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            wpfRichText.CorrectSpellError(listBox1.SelectedItem.ToString());
            Check(null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Check(null);
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            wpfRichText.IgnoreSpellError();
            Check(null);
        }

        private void LoadColumns()
        {
            if (checkedListBox1.Items.Count > 0)
                return;

            foreach (DataGridViewColumn col in appContext.dataGridView.Columns)
            {
                if(col.Visible && !(col is DataGridViewImageColumn) && !col.ReadOnly &&
                    col.ValueType == typeof(string)
                    )
                {
                    checkedListBox1.Items.Add(col.Name);
                    checkedListBox1.SetItemChecked(checkedListBox1.Items.Count - 1, true);
                }
            }
        }

        private void btnAddToDictionary_Click(object sender, EventArgs e)
        {
            customDictionary.AddToDictionary(textBox1.Text);
            Check(null);
        }

        private void btnShowDictionary_Click(object sender, EventArgs e)
        {
            customDictionary.ShowUI();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var checkedState = checkedListBox1.GetItemCheckState(checkedListBox1.SelectedIndex) == CheckState.Checked;
            checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, !checkedState);
        }
        
    }
}
