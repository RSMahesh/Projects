using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary1
{
    public partial class ColorPicker : UserControl
    {
        string _name;
        public ColorPicker(string name)
        {
            _name = name;
            InitializeComponent();
            label1.Text = name;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point pt = PointToScreen(this.button1.Location);
            ThemeColorPickerWindow f = new ThemeColorPickerWindow(pt, System.Windows.Forms.FormBorderStyle.FixedToolWindow, ThemeColorPickerWindow.Action.CloseWindow, ThemeColorPickerWindow.Action.CloseWindow);
            f.ActionAfterColorSelected = ThemeColorPickerWindow.Action.CloseWindow;
            f.ColorSelected += new ThemeColorPickerWindow.colorSelected(f_ColorSelected);
            f.Show();
        }

        void f_ColorSelected(object sender, ColorSelectedArg e)
        {
            panel1.BackColor = e.Color;
            //txtHex.Text = e.HexColor;
            //txtR.Text = e.R.ToString();
            //txtG.Text = e.G.ToString();
            //txtB.Text = e.B.ToString();
        }

        public Color SelectedColor { get
            {
                return panel1.BackColor;
            } set {
                panel1.BackColor = value;
            }
        }

        private void ColorPicker_Load(object sender, EventArgs e)
        {

        }
    }
}
