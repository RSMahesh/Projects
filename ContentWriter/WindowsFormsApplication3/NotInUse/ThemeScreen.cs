using EventPublisher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WindowsFormsControlLibrary1;

namespace WindowsFormsApplication3
{
    public partial class ThemeScreen : Form
    {
        JavaScriptSerializer _serializer = new JavaScriptSerializer();
        public ThemeScreen()
        {
            InitializeComponent();
        }

        private void ThemeScreen_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.ColumnHeadersBorderStyle =
  DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
          //  dataGridView1.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;

            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.DataSource = GetTable();
            ApplyColorToGrid();
            dataGridView1.Columns[0].Visible = false;
            // dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

        }

        private DataTable GetTable()
        {
            if (File.Exists(GetFile()))
            {
                return ReadFromXml(GetFile());
            }

            DataTable dt = new DataTable("themes");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns[0].AutoIncrement = true;

            foreach (var prop in typeof(Theme).GetProperties())
            {
                var colType = prop.PropertyType;

                if (colType == typeof(Color))
                {
                    colType = typeof(string);
                }
                dt.Columns.Add(prop.Name, colType);
            }
            return dt;

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SetTheme();
        }

        private void SetTheme()
        {
            Theme theme = new Theme();

            foreach (var prop in typeof(Theme).GetProperties())
            {
                var cell = dataGridView1.CurrentRow.Cells[prop.Name];
                if (prop.Name != "Name" && prop.Name != "Default")
                {
                    prop.SetValue(theme, cell.Style.BackColor);
                }
            }

            EventContainer.PublishEvent
             (EventPublisher.Events.LoadTheme.ToString(), new EventArg(theme));

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ReadOnly)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                btnSave.Text = "Save";
            }
            else
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                Save();
                btnSave.Text = "Edit";
            }

        }



        private DataTable ReadFromXml(string file)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(file, XmlReadMode.ReadSchema);
            return dataSet.Tables[0];
        }

        private void ApplyColorToGrid()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                        if (cell.Value != null)
                        {
                            var color = ColorTranslator.FromHtml(cell.Value.ToString());
                            OnColorChange(color, cell);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        void OnColorChange(Color selectedColor, DataGridViewCell cell)
        {
            cell.Style.BackColor = selectedColor;
            cell.Style.SelectionBackColor = selectedColor;
            cell.Value = ColorTranslator.ToHtml(selectedColor);
        }

        private void Save()
        {
            var dt = (DataTable)dataGridView1.DataSource;

            var file = GetFile();

            if (dt.Rows.Count < 1)
            {
                File.Delete(file);
                return;
            }

            dt.WriteXml(file, XmlWriteMode.WriteSchema);

        }

        private string GetFile()
        {
            var dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            return Path.Combine(dir, "themes.xml");
        }


        void f_ColorSelected(object sender, ColorSelectedArg e)
        {
            OnColorChange(e.Color, dataGridView1.CurrentCell);
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || dataGridView1.Columns[e.ColumnIndex].Name == "Name")
            {
                return;
            }

            Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(

          e.ColumnIndex, e.RowIndex, true);
            Point pt = PointToScreen(rect.Location);
            ThemeColorPickerWindow f = new ThemeColorPickerWindow(pt, System.Windows.Forms.FormBorderStyle.FixedToolWindow, ThemeColorPickerWindow.Action.CloseWindow, ThemeColorPickerWindow.Action.CloseWindow);
            f.ActionAfterColorSelected = ThemeColorPickerWindow.Action.CloseWindow;
            f.ColorSelected += new ThemeColorPickerWindow.colorSelected(f_ColorSelected);
            f.Show();

        }
    }
}
