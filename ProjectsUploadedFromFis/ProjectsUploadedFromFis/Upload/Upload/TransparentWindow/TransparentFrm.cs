using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TransparentWindow
{
    public partial class TransparentFrm : Form
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;
        const int MIN_ALL_UNDO = 416;

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;

        private enum WS_EX : int
        {
            Transparent = 0X20,
            Layered = 0x80000
        }

        enum WindowLongFlags : int
        {
            GWL_EXSTYLE = -20,
            GWLP_HINSTANCE = -6,
            GWLP_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4,
            DWLP_USER = 0x8,
            DWLP_MSGRESULT = 0x0,
            DWLP_DLGPROC = 0x4
        }


        private enum LWA : int
        {
            ColorKey = 0X1,
            Alpha = 0X2
        }

        public TransparentFrm()
        {
            InitializeComponent();
        }

        private int m_InitialStyle;

        private Form topFrm, bottomFrm, leftFrm, rightFrm;

        private bool transParent = false;
        int WindowTransparentAlpha = 0;

        private void ToggleTransparent()
        {
            if (!transParent)
            {
                this.MakeTranseparent();
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                SetWindowLong(this.Handle, (int)WindowLongFlags.GWL_EXSTYLE, (uint)(m_InitialStyle | (int)WS_EX.Layered));
                int tempWindowTransparentAlpha = 70;
                byte abc = (byte)(255 * ((double)tempWindowTransparentAlpha / (double)100));
                SetLayeredWindowAttributes(this.Handle, 0, abc, (uint)LWA.Alpha);
            }

            transParent = !transParent;
        }

        private void MakeTranseparent()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            m_InitialStyle = GetWindowLong(this.Handle, (int)WindowLongFlags.GWL_EXSTYLE);
            SetWindowLong(
                this.Handle,
                (int)WindowLongFlags.GWL_EXSTYLE,
                (uint)(m_InitialStyle | (int)WS_EX.Layered | (int)WS_EX.Transparent));
            byte abc = (byte)(255 * ((double)WindowTransparentAlpha / (double)100));
            SetLayeredWindowAttributes(this.Handle, 0, abc, (uint)LWA.Alpha);

        }

        private void Form3_Load(object sender, EventArgs e)
        {

            Color backColor = Color.Black;
            double Opacity = 0.8;


            this.TopMost = true;
            this.Top = 500;
            //  this.FormBorderStyle = FormBorderStyle.None;
            //  this.BackColor = backColor;
            //  this.Opacity = Opacity;

            topFrm = new TopFrm();
            topFrm.FormBorderStyle = FormBorderStyle.None;
            topFrm.BackColor = backColor;
            topFrm.Opacity = Opacity;
            topFrm.TopMost = true;
            topFrm.Left = 0;
            topFrm.Top = 0;
            topFrm.Width = 500;
            topFrm.Show();

            // topFrm.Resize += topFrm_Resize;
            topFrm.LocationChanged += topFrm_LocationChanged;


            bottomFrm = new Form();
            topFrm.KeyDown += TransparentFrm_KeyDown;
            topFrm.KeyDown += topFrm_KeyDown;

            bottomFrm.BackColor = backColor;
            bottomFrm.Opacity = Opacity;

            bottomFrm.FormBorderStyle = FormBorderStyle.None;
            bottomFrm.KeyDown += TransparentFrm_KeyDown;
            bottomFrm.TopMost = true;
            bottomFrm.Left = 0;
            bottomFrm.Top = topFrm.Top + topFrm.Height + this.Height;
            bottomFrm.Width = 500;
            bottomFrm.Show();


            leftFrm = new Form();
            leftFrm.KeyDown += TransparentFrm_KeyDown;
            leftFrm.BackColor = backColor;
            leftFrm.Opacity = Opacity;

            leftFrm.FormBorderStyle = FormBorderStyle.None;
            leftFrm.TopMost = true;
            leftFrm.Left = 0;
            leftFrm.Top = this.Top;
            leftFrm.Width = Screen.PrimaryScreen.Bounds.Width - this.Width;
            leftFrm.Show();


            rightFrm = new Form();
            rightFrm.KeyDown += TransparentFrm_KeyDown;
            rightFrm.BackColor = backColor;
            rightFrm.Opacity = Opacity;

            rightFrm.FormBorderStyle = FormBorderStyle.None;
            rightFrm.TopMost = true;
            rightFrm.Top = this.Top;
            rightFrm.Left = this.Left + this.Width;
            rightFrm.Width = Screen.PrimaryScreen.Bounds.Width - rightFrm.Left;
            rightFrm.Show();

            topFrm.KeyUp += this.TransparentFrm_KeyUp;
            bottomFrm.KeyUp += this.TransparentFrm_KeyUp;
            rightFrm.KeyUp += this.TransparentFrm_KeyUp;
            leftFrm.KeyUp += this.TransparentFrm_KeyUp;

            this.ChangLocation();

            this.ToggleTransparent();
        }

        private void OtherFormOpcatity()
        {

        }

        void topFrm_LocationChanged(object sender, EventArgs e)
        {
            ChangLocation();
        }

        void topFrm_Resize(object sender, EventArgs e)
        {
            ChangLocation();
        }

        void topFrm_KeyDown(object sender, KeyEventArgs e)
        {
            TransparentFrm_KeyDown(sender, e);
        }

        private void ChangLocation()
        {
            this.topFrm.Top = 0;
            this.topFrm.Height = this.Top;
            topFrm.Width = Screen.PrimaryScreen.Bounds.Width;
            bottomFrm.Top = this.Top + this.Height;
            bottomFrm.Height = Screen.PrimaryScreen.Bounds.Height - bottomFrm.Top;
            bottomFrm.Width = Screen.PrimaryScreen.Bounds.Width;
            topFrm.Left = 0;
            bottomFrm.Left = 0;

            leftFrm.Top = this.Top;
            leftFrm.Left = 0;
            leftFrm.Width = this.Left;
            leftFrm.Height = this.Height;

            rightFrm.Top = this.Top;
            rightFrm.Height = this.Height;
            rightFrm.Left = this.Left + this.Width;
            rightFrm.Width = Screen.PrimaryScreen.Bounds.Width - rightFrm.Left;


        }

        private void Plus()
        {
            try
            {
                //   textBox1.Text = (double.Parse((textBox1.Text)) + 0.01).ToString();
                SetOpacity();
            }
            catch (Exception ex)
            {
            }
        }

        private void Minus()
        {
            //  textBox1.Text = (double.Parse((textBox1.Text)) - 0.01).ToString();
            SetOpacity();
        }

        private void SetOpacity()
        {
            try
            {
                //  this.Opacity = double.Parse((textBox1.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool controlKeyPressed = false;

        private void TransparentFrm_KeyDown(object sender, KeyEventArgs e)
        {

            // SetWindowLong(this.Handle, (int)WindowLongFlags.GWL_EXSTYLE, (uint)(m_InitialStyle | (int)WS_EX.Layered));

            if (e.Control && !controlKeyPressed)
            {
                ToggleTransparent();
                controlKeyPressed = true;
            }

            if (e.KeyCode == Keys.Escape)
            {
                MinimizeAll();
            }

            if (e.KeyValue == 38)
            {
                if (e.Shift)
                {
                    topFrm.Height -= 2;
                    this.Height += 2;
                }
                else if (e.Control)
                {
                    if (WindowTransparentAlpha < 100)
                    {
                        WindowTransparentAlpha++;
                        this.MakeTranseparent();
                    }
                }
            }

            if (e.KeyValue == 40)
            {
                if (e.Shift)
                {
                    topFrm.Height += 2;
                    this.Height -= 2;
                }
                else if (e.Control)
                {
                    if (WindowTransparentAlpha > 0)
                    {
                        WindowTransparentAlpha--;
                        this.MakeTranseparent();
                    }
                }
            }

            if (e.KeyValue == 39)
            {
                if (e.Shift)
                {
                    // panel1.Width += 2;
                }

            }
            if (e.KeyValue == 37)
            {
                if (e.Shift)
                {
                    // panel1.Width += -2;
                }

            }
            if (e.KeyValue == 116)
            {
                //  textBox1.Text = System.Configuration.ConfigurationManager.AppSettings["InitOpacity"];
                //   SetOpacity();
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

        private void TransparentFrm_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                ChangLocation();
            }
            catch (Exception)
            {


            }

        }

        private void TransparentFrm_Resize(object sender, EventArgs e)
        {
            try
            {
                ChangLocation();
            }
            catch (Exception)
            {


            }

        }


        public static void MinimizeAll()
        {
           
            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
            //    System.Threading.Thread.Sleep(2000);
            //  SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL_UNDO, IntPtr.Zero);

            Application.Exit();
        }

        private void TransparentFrm_KeyUp(object sender, KeyEventArgs e)
        {
            if (controlKeyPressed)
            {
                this.ToggleTransparent();
                controlKeyPressed = false;
            }
        }

        private void TransparentFrm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
