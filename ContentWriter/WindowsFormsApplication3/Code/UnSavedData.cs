using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    class UnSavedData
    {
        AppContext appContext;
        UndoRedoStack<CellData> undoRedo;
        public UnSavedData(AppContext appContext, UndoRedoStack<CellData> undoRedo)
        {
            this.appContext = appContext;
            this.undoRedo = undoRedo;
        }


        string UnSavedDelimiter = Environment.NewLine + "_;;;_";

        private string UnSavedDataFile
        {
            get
            {
                return Path.Combine(Utility.FreeLanceAppDataFolder, Path.GetFileNameWithoutExtension(appContext.ExcelFilePath) + "_unsaved.txt");
            }
        }
        private string UnSavedDataCurrentCellFile
        {
            get
            {
                return Path.Combine(Utility.FreeLanceAppDataFolder, Path.GetFileNameWithoutExtension(appContext.ExcelFilePath) + "_unsaved_currentcell.txt");
            }
        }
        public void SaveUnSavedData(CellData cellData)
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(cellData);
            File.AppendAllText(UnSavedDataFile, serializedResult + UnSavedDelimiter);
        }
        private void SaveUnSavedDataCurrentCell(string value)
        {

            var cellData = new CellData(value, new Point(
                appContext.dataGridView.CurrentCell.ColumnIndex,
                 appContext.dataGridView.CurrentCell.RowIndex
                ));
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(cellData);
            File.AppendAllText(UnSavedDataCurrentCellFile, serializedResult);

        }
        public void LoadUnSavedData()
        {
            if ((!File.Exists(UnSavedDataFile) && !File.Exists(UnSavedDataCurrentCellFile))
                || appContext.IsReadOnlyFile
                || ConfigurationManager.AppSettings["AlertForUnSavedState"] == "false")
            {
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Do you want to laod unsaved data",
                                                        "UnSaved Data Found",
                                                         MessageBoxButtons.YesNo);


            var cellsData = GetCellDataFromFile();
            var dt = ConvertCellDataToDataTable(cellsData);

            GridDataDisplayer gridDataDisplayer = new GridDataDisplayer(dt.DefaultView);
            gridDataDisplayer.ShowDialog();


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
                        undoRedo.Do(new CellData(appContext.dataGridView[cellData.Location.X, cellData.Location.Y].Value, cellData.Location));
                        appContext.dataGridView[cellData.Location.X, cellData.Location.Y].Value = cellData.Value;
                    }
                }
            }

            if (File.Exists(UnSavedDataCurrentCellFile))
            {
                var data = File.ReadAllText(UnSavedDataCurrentCellFile);
                var serializer = new JavaScriptSerializer();
                var cellData = serializer.Deserialize<CellData>(data);
                appContext.dataGridView[cellData.Location.X, cellData.Location.Y].Value = cellData.Value;
            }
        }
        public void DeleteUnSavedDataFile()
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

        private IEnumerable<CellData> GetCellDataFromFile()
        {
            var data = File.ReadAllText(UnSavedDataFile);
            string[] stringSeparators = new string[] { UnSavedDelimiter };

            var result = data.Split(stringSeparators, StringSplitOptions.None);
            var serializer = new JavaScriptSerializer();

            List<CellData> cellsData = new List<CellData>();

            foreach (string cellInfo in result)
            {
                if (!string.IsNullOrEmpty(cellInfo))
                {
                    cellsData.Add(serializer.Deserialize<CellData>(cellInfo));
                }
            }

            return cellsData;
        }

        private DataTable ConvertCellDataToDataTable(IEnumerable<CellData> cellsData)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Column");
            dt.Columns.Add("Content");

            foreach (var cellData in cellsData)
            {
                var row = dt.NewRow();
                row["ID"] = cellData.Location.Y.ToString();
                row["Column"] = appContext.dataGridView.Columns[cellData.Location.X].Name;
                row["Content"] = cellData.Value;
                dt.Rows.Add(row);
            }
            return dt;
        }

    }
}
