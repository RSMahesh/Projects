using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;


namespace WindowsFormsApplication3
{
    public partial class Form3 : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        public Form3()
        {
            InitializeComponent();
            timer1.Interval = 1000 * 1;
            timer1.Enabled = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void MouseMoveTimer_Tick(object sender, EventArgs e)
        {
            Point cursor = new Point();
            GetCursorPos(ref cursor);

            var c = GetColorAt(cursor);
            this.BackColor = c;

            if (c.R == c.G && c.G < 64 && c.B > 128)
            {
                MessageBox.Show("Blue");
            }
        }

        Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        public Color GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point cursor = new Point();
            GetCursorPos(ref cursor);

            var c = GetColorAt(cursor);
            this.Text = GetColorName(c);
            this.BackColor = c;
            //var tt = c.ToKnownColor();
            //var nn = tt.ToString();
            //this.Text = nn;

            if (c.R == c.G && c.G < 64 && c.B > 128)
            {
                MessageBox.Show("Blue");
            }
        }

        private string GetColorName(Color color)
        {
            string name = "Unknown";
            foreach (KnownColor kc in Enum.GetValues(typeof(KnownColor)))
            {
                Color known = Color.FromKnownColor(kc);
                if (color.ToArgb() == known.ToArgb())
                {
                    name = known.Name;
                    break;
                }
            }

            return name;
        }
    }
}



//namespace FormTest
//    {
//        public partial class Form1 : Form
//        {
//            [DllImport("user32.dll")]
//            static extern bool GetCursorPos(ref Point lpPoint);

//            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
//            public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

//            public Form1()
//            {
//                InitializeComponent();
//            }

//            private void MouseMoveTimer_Tick(object sender, EventArgs e)
//            {
//                Point cursor = new Point();
//                GetCursorPos(ref cursor);

//                var c = GetColorAt(cursor);
//                this.BackColor = c;

//                if (c.R == c.G && c.G < 64 && c.B > 128)
//                {
//                    MessageBox.Show("Blue");
//                }
//            }

//            Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
//            public Color GetColorAt(Point location)
//            {
//                using (Graphics gdest = Graphics.FromImage(screenPixel))
//                {
//                    using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
//                    {
//                        IntPtr hSrcDC = gsrc.GetHdc();
//                        IntPtr hDC = gdest.GetHdc();
//                        int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
//                        gdest.ReleaseHdc();
//                        gsrc.ReleaseHdc();
//                    }
//                }

//                return screenPixel.GetPixel(0, 0);
//            }
//        }
//    }
//}
