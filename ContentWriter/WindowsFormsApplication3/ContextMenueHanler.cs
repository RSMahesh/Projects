﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class ContextMenueHanler
    {

        FormulaRunner formulaRunner;
        DataGridView dataGridView;
        UndoRedoStack<CellData> undoRedo;
        public ContextMenueHanler(DataGridView dataGridView, UndoRedoStack<CellData> undoRedo )
        {
            this.dataGridView = dataGridView;
            this.undoRedo = undoRedo;
            formulaRunner = new FormulaRunner(dataGridView);
        }

        public void OnApplyFormula(object sender, EventArgs e)
        {
            FormulaList frm = new FormulaList(dataGridView);
            frm.ShowDialog();


            if (string.IsNullOrEmpty(frm.CurrentFormula))
            {
                MessageBox.Show("No Fromula Found");
                return;
            }
       
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                var formulaOutPut = formulaRunner.ExecuteFromula(cell, frm.CurrentFormula);

                undoRedo.Do(new CellData(cell.Value, new Point(cell.ColumnIndex, cell.RowIndex)));

                cell.Value = formulaOutPut;
            }
        }
        public void OnCopy(object sender, EventArgs e)
        {
            var listSelectedCells = new List<CellData>();

            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
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
        public void OnPast(object sender, EventArgs e)
        {
            var text = Clipboard.GetText();
            var pastCells = new List<CellData>();
            List<CellData> copyedCells;

            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
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
        public void OnDelete(object sender, EventArgs e)
        {
            var listSelectedCells = new List<CellData>();

            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                undoRedo.Do(new CellData(dataGridView[cell.ColumnIndex, cell.RowIndex].Value, new Point(cell.ColumnIndex, cell.RowIndex)));

                cell.Value = string.Empty;
            }

        }
        private void PasteSingleCellData(string copyedText, List<CellData> targetCells)
        {
            for (var indx = 0; indx < targetCells.Count; indx++)
            {
                undoRedo.Do(new CellData(dataGridView.Rows[targetCells[indx].Location.Y].Cells[targetCells[indx].Location.X].Value, targetCells[indx].Location));
                dataGridView.Rows[targetCells[indx].Location.Y].Cells[targetCells[indx].Location.X].Value =
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
                undoRedo.Do(new CellData(dataGridView.Rows[targetCells[indx].Location.Y].Cells[targetCells[indx].Location.X].Value, targetCells[indx].Location));

                dataGridView.Rows[targetCells[indx].Location.Y].Cells[targetCells[indx].Location.X].Value =
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

    }
}
