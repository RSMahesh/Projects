using MB.Core;
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
    public partial class TransparentFrm : Form
    {
        bool topMost = true;

        string backGroundImageFile = "BackGroundImageFile.jpg";

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;

        double _Opacity = 1.0;

        Point LastTransperWindowPoint = new Point();

        Size _sizeBeforeLastTab = new Size();

        private bool controlKeyPressed = false;

        private bool altKeyPressed = false;

        private bool moveWindow = false;

        public bool escapedKepPressed = false;

        AttachedWindow _attachedWindow;
        FavouriteLocation _favouriteLocation;

        public TransparentFrm(AttachedWindow attachedWindow)
        {
        
            InitializeComponent();
            this.Location = Screen.AllScreens[1].WorkingArea.Location;

            DeleteTempImages();
            _attachedWindow = attachedWindow;
            _favouriteLocation = new FavouriteLocation(this, _attachedWindow);
            backGroundImageFile = RuntimeHelper.MapToCurrentLocation(backGroundImageFile);
        }

        private Form topFrm, bottomFrm, leftFrm, rightFrm;

        private Transpainter _transpainter = null;

        private void OnLoad(object sender, EventArgs e)
        {
            this.Location = Screen.AllScreens[1].WorkingArea.Location;

            _transpainter = new Transpainter(this, _attachedWindow);

            Color backColor = Color.Black;

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

            _transpainter.ToggleTransparent();
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

        }

        private void SetBackground()
        {
            if (!File.Exists(backGroundImageFile))
            {
                return;
            }
            string filename = backGroundImageFile;
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


        public void TransparentFrm_KeyDown(object sender, KeyEventArgs e)
        {
            escapedKepPressed = false;

            if (e.Control && !controlKeyPressed)
            {
                _transpainter.ToggleTransparent();
                controlKeyPressed = true;
            }


            if (e.Alt && !altKeyPressed)
            {
                altKeyPressed = true;
            }

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (_transpainter.WindowTransparentAlpha != 100)
                    {
                        _transpainter.WindowTransparentAlpha = 100;
                        _transpainter.MakeTranseparent();
                        return;
                    }
                    escapedKepPressed = true;
                    _attachedWindow.MinizeAttachWindow();
                    //MinimizeAll();
                    MiniZeAllForms();
                    _attachedWindow.MinizeAttachWindow();
                    escapedKepPressed = false;

                    break;
                case Keys.Up:
                    if (e.Shift)
                    {
                        _Opacity += .1;
                         SetOpacity();
                    }
                    else if (e.Alt)
                    {
                        if (_transpainter.WindowTransparentAlpha < 100)
                        {
                            _transpainter.WindowTransparentAlpha++;
                            _transpainter.MakeTranseparent();
                        }
                    }
                    break;
                case Keys.Down:
                    if (e.Shift)
                    {
                        _Opacity -= .1;
                        this.SetOpacity();
                    }
                    else if (e.Alt)
                    {
                        if (_transpainter.WindowTransparentAlpha > 0)
                        {
                            _transpainter.WindowTransparentAlpha--;
                            _transpainter.MakeTranseparent();
                        }
                    }

                    break;

                case Keys.F1:
                    ShowHelp();
                    break;
                case Keys.F4:
                    this.RealignBackGround();
                    break;

                case Keys.F6:
                    SaveCurrentLocation();
                    break;
                case Keys.F7:
                    this.MoveToFavoriteLocation();
                    this.RealignBackGround();
                    break;
                case Keys.F8:

                    _transpainter.DecreaseTransparency(10);
                    break;
                case Keys.F9:
                    _transpainter.IncreaseTransparency(10);
                    break;
                case Keys.F11:
                    ChangeBackgroundImage();
                    break;
                case Keys.F12:
                    _transpainter.MakeTranseparent12();
                    break;
            }
        }

        private void ChangeBackgroundImage()
        {
            ToggleTopMost();
            SetBackGround();
            ToggleTopMost();
            RealignBackGround();
        }

        private void ShowHelp()
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
                _transpainter.ToggleTransparent();
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

            SplitImage.SplitImages(Image.FromFile(backGroundImageFile), splits);

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
                _attachedWindow.MoveAttacheWindow(this.Top - LastTransperWindowPoint.Y, this.Left - LastTransperWindowPoint.X);

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
            _attachedWindow.ShowAttachWindow();
            topFrm.Activate();
        }

        private void SaveCurrentLocationOld()
        {
            DialogResult result1 = MessageBox.Show(
                "Update Favorite Location ",
                "Important Question",
                MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes)
            {
                string hh = this.Left.ToString() + "," + this.Top.ToString() + "," + this.Width.ToString() + ","
                            + this.Height.ToString();

                var attacheWin = _attachedWindow.GetAttacheWindowSize();



                string hh1 = attacheWin.Left.ToString() + "," + attacheWin.Top.ToString() + "," + attacheWin.Width.ToString() + ","
                           + attacheWin.Height.ToString();

                hh += "|" + hh1;

                File.WriteAllText("FavoriteLocation.txt", hh);
            }

            Location location = new TransparentWindow.Location();
            location.TransparentWindowLocation = this.Bounds;
            location.AttachedWindowLocation = _attachedWindow.GetAttacheWindowSize();



        }

        private void SaveCurrentLocation()
        {
            DialogResult result1 = MessageBox.Show(
                "Update Favorite Location ",
                "Important Question",
                MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes)
            {
                // var locationFileName = Microsoft.VisualBasic.Interaction.InputBox("Location File Name", "Save Current Location", "");

                this.TopMost = false;
                var locationFileName = Prompt.ShowDialog("File Name", "Location File Name");
                this.TopMost = true;
                if (string.IsNullOrEmpty(locationFileName))
                {
                    return;
                }

                _favouriteLocation.SaveCurrentLocation(locationFileName);
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
                this.openFileDialog1.Filter = "Files|*.json";
                DialogResult result = this.openFileDialog1.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    _favouriteLocation.MoveToLocation(openFileDialog1.FileName);
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
                this.ResizeImage(openFileDialog1.FileName, Screen.PrimaryScreen.Bounds.Size);
            }
        }

        private void ResizeImage(string sourceImagepath, Size size)
        {
            var imageMagic = @"C:\Media\Tools\Apps\ImageMagick\convert.exe";

            backGroundImageFile = RuntimeHelper.MapToCurrentLocation(Guid.NewGuid().ToString() + "backGroundImageFile.jpg");

            var parameters = string.Format(
                "{0} -resize  {1}x{2}!  {3}",
                 "\"" + sourceImagepath + "\"",
                size.Width,
                size.Height,
               "\"" + backGroundImageFile + "\"");

            var startInfo = new ProcessStartInfo(imageMagic, parameters);

            Clipboard.SetText(parameters);

            var pp = Process.Start(startInfo);
            pp.WaitForExit();
        }

        private void TransparentFrm_ResizeBegin(object sender, EventArgs e)
        {
            LastTransperWindowPoint.Y = this.Top;
            LastTransperWindowPoint.X = this.Left;
        }


    }
}
