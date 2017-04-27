using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TransparentWindow
{
    public partial class TransparentFrmOld : Form
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

        double _Opacity = 1.0;

        Point LastTransperWindowPoint = new Point();

        Size _sizeBeforeLastTab = new Size();

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

        public TransparentFrmOld()
        {
            InitializeComponent();
            DeleteTempImages();
        }

        private int m_InitialStyle;

        private Form topFrm, bottomFrm, leftFrm, rightFrm;

        private bool transParent = false;
        int WindowTransparentAlpha = 80;

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

        private void MakeTranseparent12()
        {
            var handle = Init.GetAttacheWindowHandle();

            this.FormBorderStyle = FormBorderStyle.None;
            m_InitialStyle = GetWindowLong(handle, (int)WindowLongFlags.GWL_EXSTYLE);
            SetWindowLong(
                handle,
                (int)WindowLongFlags.GWL_EXSTYLE,
                (uint)(m_InitialStyle | (int)WS_EX.Layered | (int)WS_EX.Transparent));
            byte abc = (byte)(255 * ((double)WindowTransparentAlpha / (double)100));

            SetLayeredWindowAttributes(handle, 0, abc, (uint)LWA.Alpha);

        }

        private void OnLoad(object sender, EventArgs e)
        {
            Color backColor = Color.Black;

            var topMost = true;

            this.TopMost = topMost;
            this.Top = 500;
            topFrm = new TopForm1();
            topFrm.BackColor = backColor;
            topFrm.Opacity = Opacity;
            topFrm.TopMost = topMost;
            topFrm.Left = 0;
            topFrm.Top = 0;
            topFrm.Width = 500;
            ((TopForm1)topFrm).OnDubleClick = this.ToggleTopMost;

            topFrm.Show();

            bottomFrm = new Form();
            bottomFrm.BackColor = backColor;
            bottomFrm.TopMost = topMost;
            bottomFrm.Left = 0;
            bottomFrm.Top = topFrm.Top + topFrm.Height + this.Height;
            bottomFrm.Width = 500;
            bottomFrm.Show();

            leftFrm = new Form();
            leftFrm.BackColor = backColor;

            leftFrm.TopMost = topMost;
            leftFrm.Left = 0;
            leftFrm.Top = this.Top;
            leftFrm.Width = Screen.PrimaryScreen.Bounds.Width - this.Width;
            leftFrm.Show();


            rightFrm = new Form();
            rightFrm.BackColor = backColor;

            rightFrm.TopMost = topMost;
            rightFrm.Top = this.Top;
            rightFrm.Left = this.Left + this.Width;
            rightFrm.Width = Screen.PrimaryScreen.Bounds.Width - rightFrm.Left;
            rightFrm.Show();

            topFrm.KeyUp += this.TransparentFrm_KeyUp;
            bottomFrm.KeyUp += this.TransparentFrm_KeyUp;
            rightFrm.KeyUp += this.TransparentFrm_KeyUp;
            leftFrm.KeyUp += this.TransparentFrm_KeyUp;

            topFrm.KeyDown += TransparentFrm_KeyDown;
            bottomFrm.KeyDown += TransparentFrm_KeyDown;
            rightFrm.KeyDown += TransparentFrm_KeyDown;
            leftFrm.KeyDown += TransparentFrm_KeyDown;

            topFrm.MouseDoubleClick += MouseDoubleClick;
            bottomFrm.MouseDoubleClick += MouseDoubleClick;
            rightFrm.MouseDoubleClick += MouseDoubleClick;
            leftFrm.MouseDoubleClick += MouseDoubleClick;

            topFrm.MouseMove += topFrm_MouseMove;
            bottomFrm.MouseMove += topFrm_MouseMove;
            rightFrm.MouseMove += topFrm_MouseMove;
            leftFrm.MouseMove += topFrm_MouseMove;

            topFrm.Activated += topFrm_Activated;
            bottomFrm.Activated += topFrm_Activated;
            rightFrm.Activated += topFrm_Activated;
            leftFrm.Activated += topFrm_Activated;

            topFrm.FormBorderStyle = FormBorderStyle.None;
            bottomFrm.FormBorderStyle = FormBorderStyle.None;
            leftFrm.FormBorderStyle = FormBorderStyle.None;
            rightFrm.FormBorderStyle = FormBorderStyle.None;

            SetBackground();

            SetOpacity();

            this.ChangLocation();

            this.ToggleTransparent();
        }

        private void ToggleTopMost()
        {
            topFrm.TopMost = bottomFrm.TopMost = leftFrm.TopMost = rightFrm.TopMost = this.TopMost = !this.TopMost;

            MessageBox.Show("TopMost is now :" + topFrm.TopMost.ToString());

        }

        void topFrm_Activated(object sender, EventArgs e)
        {
            this.UndoMiniZeAllForms();
        }

        void topFrm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!moveWindow) return;
            var form = (Form)sender;
            var formPointToScreen = form.PointToScreen(e.Location);
            this.Left = formPointToScreen.X + 50;
            this.Top = formPointToScreen.Y + 50;

        }

        private bool moveWindow = false;

        void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            moveWindow = !moveWindow;
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

            // SplitImage.SplitImages(Image.FromFile("Destop.jpg"), new Rectangle(topFrm.Left, topFrm.Top, topFrm.Width, topFrm.Height));
        }

        private void SetBackground()
        {
            string filename = "Destop.jpg";
            //    ServerDLL.ScreenCapture.CaptureScreenToFile(filename, ImageFormat.Jpeg);

            bottomFrm.BackgroundImage = Image.FromFile(filename);
            topFrm.BackgroundImage = Image.FromFile(filename);
            leftFrm.BackgroundImage = Image.FromFile(filename);
            rightFrm.BackgroundImage = Image.FromFile(filename);
        }

        private void SetOpacity()
        {
            try
            {
                leftFrm.Opacity = rightFrm.Opacity = topFrm.Opacity = bottomFrm.Opacity = _Opacity;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool controlKeyPressed = false;

        private bool altKeyPressed = false;


        public bool escapedKepPressed = false;

        public void TransparentFrm_KeyDown(object sender, KeyEventArgs e)
        {
            escapedKepPressed = false;

            if (e.Control && !controlKeyPressed)
            {
                ToggleTransparent();
                controlKeyPressed = true;
            }


            if (e.Alt && !altKeyPressed)
            {
                altKeyPressed = true;
            }


            if (e.KeyCode == Keys.Escape)
            {
                if (WindowTransparentAlpha != 100)
                {
                    WindowTransparentAlpha = 100;
                    this.MakeTranseparent();
                    return;
                }
                //topFrm.WindowState = FormWindowState.Minimized;
                escapedKepPressed = true;
                Init.MinizeAttachWindow();
                MinimizeAll();
                MiniZeAllForms();
                Init.MinizeAttachWindow();
                escapedKepPressed = false;
            }

            if (e.KeyValue == 38)
            {
                if (e.Shift)
                {
                    _Opacity += .1;
                    this.SetOpacity();
                }
                else if (e.Alt)
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
                    _Opacity -= .1;
                    this.SetOpacity();
                }
                else if (e.Alt)
                {
                    if (WindowTransparentAlpha > 0)
                    {
                        WindowTransparentAlpha--;
                        this.MakeTranseparent();
                    }
                }
            }


            HandleHeightDecrease(e);


            if (e.KeyValue == 112)
            {
                string helpMsg = ">To Make Transparent Box moveable/sizeable with mouse Press control Key and move window with mouse" + Environment.NewLine + Environment.NewLine;
                helpMsg += ">To Make Transparent Box along with mouse double click outside Transparent Box" + Environment.NewLine + Environment.NewLine;
                helpMsg += ">To incress/decrease the opacity of transpent box use Arrow Keys with ALT" + Environment.NewLine + Environment.NewLine;
                helpMsg += ">To incress/decrease opacity of background use up and down arrow key with SHIFT" + Environment.NewLine + Environment.NewLine;
                helpMsg += ">To Reduces Bottom use F2 key. To reverse SHIFT F2" + Environment.NewLine + Environment.NewLine;
                helpMsg += ">To Reduces TOP use F3 key. To reverse SHIFT F3" + Environment.NewLine + Environment.NewLine;
                helpMsg += ">To Refresh/Realign use F4 key." + Environment.NewLine + Environment.NewLine;
                helpMsg += ">To Save Favorate Location F6/F7 key." + Environment.NewLine + Environment.NewLine;
                helpMsg += ">To Close use ESC key" + Environment.NewLine + Environment.NewLine;
                helpMsg += ">To Toggle TopMost Property cick on top form" + Environment.NewLine + Environment.NewLine;

                MessageBox.Show(helpMsg);

            }

            if (e.KeyValue == 115)
            {
                this.RealignBackGround();
            }

            if (e.KeyValue == 117)
            {
                SaveCurrentLocation();
            }

            if (e.KeyValue == 118)
            {
                this.MoveToFavoriteLocation();
                this.RealignBackGround();
            }

            if (e.KeyValue == 119)
            {
                //this.MakeTranspranceyLowSlowly();

                if (WindowTransparentAlpha < 100)
                {
                    WindowTransparentAlpha += 10;

                    if (WindowTransparentAlpha > 100)
                    {
                        WindowTransparentAlpha = 100;
                    }

                    this.MakeTranseparent();
                }
            }

            //F9
            if (e.KeyValue == 120)
            {

                if (WindowTransparentAlpha > 0)
                {
                    WindowTransparentAlpha -= 10;

                    if (WindowTransparentAlpha < 0)
                    {
                        WindowTransparentAlpha = 0;
                    }

                    this.MakeTranseparent();
                }
                //WindowTransparentAlpha = 0;
                //this.MakeTranseparent();
            }

            //F11
            if (e.KeyValue == 122)
            {
                ToggleTopMost();
                SetBackGround();
                ToggleTopMost();

                RealignBackGround();

            }

            //F12
            if (e.KeyValue == 123)
            {
                this.MakeTranseparent12();

            }

        }

        private void RealignBackGround()
        {
            Dictionary<string, Rectangle> splits = new Dictionary<string, Rectangle>();

            string topImage = "top" + Guid.NewGuid().ToString();
            string bottomImage = "Bottom" + Guid.NewGuid().ToString();
            string leftImage = "Left" + Guid.NewGuid().ToString();
            string rightImage = "Right" + Guid.NewGuid().ToString();

            splits[topImage] = new Rectangle(topFrm.Left, topFrm.Top, topFrm.Width, topFrm.Height);
            splits[bottomImage] = new Rectangle(bottomFrm.Left, bottomFrm.Top, bottomFrm.Width, bottomFrm.Height);
            splits[leftImage] = new Rectangle(leftFrm.Left, leftFrm.Top, leftFrm.Width, leftFrm.Height);
            splits[rightImage] = new Rectangle(rightFrm.Left, rightFrm.Top, rightFrm.Width, rightFrm.Height);

            SplitImage.SplitImages(Image.FromFile("Destop.jpg"), splits);

            topFrm.BackgroundImage = Image.FromFile(Path.Combine(SplitImage.TransparentWindowImagesDir, topImage + ".jpg"));
            bottomFrm.BackgroundImage = Image.FromFile(Path.Combine(SplitImage.TransparentWindowImagesDir, bottomImage + ".jpg"));
            leftFrm.BackgroundImage = Image.FromFile(Path.Combine(SplitImage.TransparentWindowImagesDir, leftImage + ".jpg"));
            rightFrm.BackgroundImage = Image.FromFile(Path.Combine(SplitImage.TransparentWindowImagesDir, rightImage + ".jpg"));

        }

        private void DeleteTempImages()
        {
            if (!Directory.Exists(SplitImage.TransparentWindowImagesDir))
            {
                return;
            }

            var files = Directory.GetFiles(SplitImage.TransparentWindowImagesDir, "*.jpg");

            foreach (var file in files)
            {
                if (!file.Contains("Destop.jpg"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
        }

        private void HandleHeightDecrease(KeyEventArgs e)
        {
            int decreaseHeight = 50;

            if (e.Shift)
            {
                decreaseHeight *= -1;
            }

            if (e.KeyValue == 113)
            {
                bottomFrm.Height -= decreaseHeight;
            }

            if (e.KeyValue == 114)
            {
                topFrm.Height -= decreaseHeight;
                topFrm.Top += decreaseHeight;
            }

        }

        private void TransparentFrm_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                ChangLocation();
                MoveAttachedProcessWindow();
            }
            catch (Exception)
            {

            }
        }

        private void MoveAttachedProcessWindow()
        {
            if (altKeyPressed)
            {
                //TO D) : Fix this shi
                Init.MoveAttacheWindow(this.Top - LastTransperWindowPoint.Y, this.Left - LastTransperWindowPoint.X);

                // Init.MoveAttacheWindowOld(this.Top, this.Left);

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
        }

        private void MiniZeAllForms()
        {
            topFrm.WindowState = FormWindowState.Minimized;
            bottomFrm.WindowState = FormWindowState.Minimized;
            rightFrm.WindowState = FormWindowState.Minimized;
            leftFrm.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Minimized;
        }

        private void UndoMiniZeAllForms()
        {
            if (escapedKepPressed)
                return;

            topFrm.WindowState = FormWindowState.Normal;
            bottomFrm.WindowState = FormWindowState.Normal;
            rightFrm.WindowState = FormWindowState.Normal;
            leftFrm.WindowState = FormWindowState.Normal;
            this.WindowState = FormWindowState.Normal;
            Init.ShowAttachWindow();
            topFrm.Activate();
        }


        private void TransparentFrm_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab)
            {
                if (Height > 10 && Width > 10)
                {
                    _sizeBeforeLastTab.Width = this.Width;
                    _sizeBeforeLastTab.Height = this.Height;
                    this.Height = 0;
                    this.Width = 0;
                }
                else
                {
                    this.Height = _sizeBeforeLastTab.Height;
                    this.Width = _sizeBeforeLastTab.Width;
                }

                RealignBackGround();
            }

            if (controlKeyPressed)
            {
                this.ToggleTransparent();
                controlKeyPressed = false;
            }

            if (altKeyPressed)
            {
                altKeyPressed = false;
            }

            // I don't know why the hell its stop  working in keyDown
            if (e.KeyValue == 38)
            {
                if (e.Shift)
                {
                    _Opacity += .1;
                    this.SetOpacity();
                }
            }

            if (e.KeyValue == 40)
            {

                if (e.Shift)
                {
                    _Opacity -= .1;
                    this.SetOpacity();
                }
            }
        }

        private void TransparentFrm_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void SaveCurrentLocation()
        {
            DialogResult result1 = MessageBox.Show(
                "Update Favorite Location ",
                "Important Question",
                MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes)
            {
                string hh = this.Left.ToString() + "," + this.Top.ToString() + "," + this.Width.ToString() + ","
                            + this.Height.ToString();

                var attacheWin = Init.GetAttacheWindowSize();



                string hh1 = attacheWin.Left.ToString() + "," + attacheWin.Top.ToString() + "," + attacheWin.Width.ToString() + ","
                           + attacheWin.Height.ToString();

                hh += "|" + hh1;

                File.WriteAllText("FavoriteLocation.txt", hh);
            }
        }

        private void MoveToFavoriteLocation()
        {
            DialogResult result1 = MessageBox.Show(
             "Move To Favorite Location ",
             "Important Question",
             MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes)
            {
                if (!File.Exists("FavoriteLocation.txt"))
                {
                    MessageBox.Show("FavoriteLocation.txt does not exist");
                    return;
                }

                var location = File.ReadAllText("FavoriteLocation.txt").Split('|')[0];
                var dememsions = location.Split(',');

                this.Left = int.Parse(dememsions[0]);
                this.Top = int.Parse(dememsions[1]);
                this.Width = int.Parse(dememsions[2]);
                this.Height = int.Parse(dememsions[3]);

                if (File.ReadAllText("FavoriteLocation.txt").Split('|').Length > 1)
                {
                    var attachedWindowlocation1 = File.ReadAllText("FavoriteLocation.txt").Split('|')[1];

                    dememsions = attachedWindowlocation1.Split(',');


                    Init.MoveResizeAttacheWindow(int.Parse(dememsions[0]), int.Parse(dememsions[1]), int.Parse(dememsions[2]), int.Parse(dememsions[3]));
                }
            }
        }

        private void TransparentFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void SetBackGround()
        {
            this.openFileDialog1.Filter = "Files|*.*";
            DialogResult result = this.openFileDialog1.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                this.ResizeImage(openFileDialog1.FileName, Screen.PrimaryScreen.Bounds.Size, "Destop.jpg");
            }
        }

        private void ResizeImage(string sourceImagepath, Size size, string targetImagepath)
        {
            var imageMagic = @"C:\Media\Tools\Apps\ImageMagick\convert.exe";

            var parameters = string.Format(
                "{0} -resize  {1}x{2}!  {3}",
                sourceImagepath,
                size.Width,
                size.Height,
                targetImagepath);

            var startInfo = new ProcessStartInfo(imageMagic, parameters);

            var pp = Process.Start(startInfo);
            pp.WaitForExit();
        }

        private void TransparentFrm_ResizeBegin(object sender, EventArgs e)
        {
            LastTransperWindowPoint.Y = this.Top;
            LastTransperWindowPoint.X = this.Left;
        }

        private void TransparentFrm_ResizeEnd(object sender, EventArgs e)
        {

        }

    }
}
