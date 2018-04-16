using EventPublisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class FindAndReplace
    {
        int startingRowToFind = 0;
        int startingColumnToFind = 0;
        int startingIndexInCellToFindText = 0;
        DataGridView dataGridView;
        WpfRichTextBox wpfRichText;
        Action showWpfRichTextBox;

        public FindAndReplace(AppContext appContext, WpfRichTextBox wpfRichTextBox)
        {
            this.dataGridView = appContext.dataGridView;
            this.wpfRichText = wpfRichTextBox;
            this.showWpfRichTextBox = appContext.ShowWpfRichTextBox;
        }
        public void FindText(EventArg arg)
        {
            var arr = arg.Arg as string[];
            var textToFind = arr[0];
            var columnName = arr[1];
            
            for (var rowIndex = startingRowToFind; rowIndex < dataGridView.Rows.Count; rowIndex++)
            {
                DataGridViewRow row = dataGridView.Rows[rowIndex];

                for (var colIndex = startingColumnToFind; colIndex < dataGridView.Rows[rowIndex].Cells.Count; colIndex++)
                {

                    if(columnName != "ALL" && dataGridView.Columns[colIndex].Name != columnName)
                    {
                        continue;
                    }

                    var cell = dataGridView.Rows[rowIndex].Cells[colIndex];

                    if (!(dataGridView.Columns[colIndex] is DataGridViewImageColumn))
                    {

                        if (cell.Visible && cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                        {

                            if(cell.Value.ToString().Length -1 < startingIndexInCellToFindText)
                            {
                                continue;
                            }

                            var indx = cell.Value.ToString().IndexOf(textToFind, startingIndexInCellToFindText, StringComparison.OrdinalIgnoreCase);

                            if (indx != -1)
                            {
                                //heck to make mutiselect work in cell
                                dataGridView.CurrentCell = dataGridView[0, rowIndex];

                                dataGridView.CurrentCell = cell;
                                dataGridView.BeginEdit(false);
                                //HighlightText(editingTextBox, textToFind, startingIndexInCellToFindText);
                                showWpfRichTextBox();
                                wpfRichText.SelectText(textToFind, startingIndexInCellToFindText);
                                startingIndexInCellToFindText = 0;
                                startingRowToFind = rowIndex;
                                startingColumnToFind = colIndex;

                                indx = cell.Value.ToString().IndexOf(textToFind, indx + textToFind.Length, StringComparison.OrdinalIgnoreCase);

                                if (indx != -1)
                                {
                                    startingIndexInCellToFindText = indx;
                                }
                                else
                                {

                                    startingColumnToFind = colIndex + 1;
                                }

                                if (startingColumnToFind >= dataGridView.Rows[rowIndex].Cells.Count)
                                {
                                    startingColumnToFind = 0;
                                    startingIndexInCellToFindText = 0;
                                    startingRowToFind++;
                                }

                                return;
                            }
                        }
                    }
                }

                startingColumnToFind = 0;
            }

            MessageBox.Show("All Match Found");
            startingColumnToFind = startingRowToFind = startingIndexInCellToFindText = 0;
        }
        public void ReplaceText(EventArg arg)
        {
            var arr = arg.Arg as string[];
            var textToReplace = arr[1];
            var textToFind = arr[0];
            var columnName = arr[2];

            if (wpfRichText.ReplaceText(textToReplace))
            {
                //adjust startingIndexInCellToFindText based on length changed due to repalcement
                var diff = textToReplace.Length - textToFind.Length;

                if (startingIndexInCellToFindText > 0)
                {
                    startingIndexInCellToFindText += diff;
                }
            }

            EventContainer.PublishEvent
       (EventPublisher.Events.FindText.ToString(), new EventArg(new[] { textToFind, columnName } ));

          
        }
    }
}
