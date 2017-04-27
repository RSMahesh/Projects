using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TransparentWindow
{
    public partial class Form1 : Form
    {
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        static readonly IntPtr HWND_TOP = new IntPtr(0);

        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        const UInt32 SWP_NOSIZE = 0x0001;

        const UInt32 SWP_NOMOVE = 0x0002;

        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        bool controlKey = false;
 
        public Form1()
        {
            this.TransparencyKey = Color.Red;

            InitializeComponent();
            this.KeyPreview = true;
            textBox1.Text = System.Configuration.ConfigurationManager.AppSettings["InitOpacity"];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            SetOpacity();
           
        }

      
        private void Plus()
        {
            try
            {
                textBox1.Text = (double.Parse((textBox1.Text)) + 0.01).ToString();
                SetOpacity();
            }
            catch (Exception ex)
            {
            }
        }

        private void Minus()
        {
            textBox1.Text = (double.Parse((textBox1.Text)) - 0.01).ToString();
            SetOpacity();
        }
        
        private void SetOpacity()
        {
            try
            {
                this.Opacity = double.Parse((textBox1.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecress_Click(object sender, EventArgs e)
        {
            Minus();
     
        }

       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
           
            if (e.KeyValue == 38)
            {
                if (e.Shift)
                {
                    panel1.Height += -2;
                }
                else
                {
                    Plus();
                }
            }

            if (e.KeyValue == 40)
            {
                if (e.Shift)
                {
                    panel1.Height += 2;
                }
                else
                {
                  Minus();
                }
            }

            if (e.KeyValue == 39)
            {
                if (e.Shift)
                {
                    panel1.Width += 2;
                }
              
            }
            if (e.KeyValue == 37)
            {
                if (e.Shift)
                {
                    panel1.Width += -2;
                }
              
            }
            if (e.KeyValue == 116)
            {
                textBox1.Text = System.Configuration.ConfigurationManager.AppSettings["InitOpacity"];
                SetOpacity();
            }

            if (e.KeyValue == 112)
            {
                string helpMsg = "To Make Transparent Box moveable with mouse Press control Key once and move mouse" + Environment.NewLine;
                helpMsg += "To Make Transparent Box Static Press control Key once again" + Environment.NewLine + Environment.NewLine;
                helpMsg += "To incress/decrease the size of transpent box use Arrow Keys with SHIFT" + Environment.NewLine + Environment.NewLine;
                helpMsg += "To incress/decrease opacity use up and down arrow key" + Environment.NewLine + Environment.NewLine;

                MessageBox.Show(helpMsg);

            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.ControlKey)
            {
                controlKey = !controlKey;
            }
           
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (controlKey)
            {
                panel1.Top = e.Y + 20;
                panel1.Left = e.X;
            }
        }
    }
}
