using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.IO;
using System.Threading;

using FileSystem;

using UserActivityLogger;
using Core;

namespace RecordSession
{
    public partial class frmPictureViewer : Form
    {
        private int currentImageIndex = 0;
        public Action<int> DisplayChange;
        public int incrementCount = 1;
        IActivityRepositary _activityRepositary;
        ActivityReader _activityReader;
        public Action<string> OnCommentsFetched;
        public Action<int> OnIndexChanged;

        public int Index
        {
            get
            {
                return currentImageIndex;
            }

            set
            {
                currentImageIndex = value;
            }
        }

        public frmPictureViewer()
        {
            InitializeComponent();
        }

        public void ChangeNextImagePostion(int index)
        {
            timer2.Enabled = false;
            _activityReader.ChangePostion(index);
            timer2.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.WindowState = FormWindowState.Normal;
        }


        public int Play(string folder)
        {
            if (timer2.Enabled)
                timer2.Enabled = false;
            else
            {
                timer2.Enabled = true;

                _activityRepositary = new ActivityRepositary(new JarFileFactory(), new ImageCommentEmbedder(), folder);

                if (_activityReader != null)
                {
                    _activityReader.Dispose();
                }

                _activityReader = _activityRepositary.GetReader();
                return _activityReader.FileCount();
            }

            return 0;
        }

        public void MinimizeWindow()
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (!_activityReader.GetEnumerator().MoveNext())
            {
                timer2.Enabled = false;
                _activityReader.Dispose();
                return;
            }

            var activity = _activityReader.GetEnumerator().Current;
            pictureBox1.Image = activity.ScreenShot;
            OnCommentsFetched?.Invoke(activity.KeyPressedData);
            DisplayChange(Index);
            Index += incrementCount;

            this.Text = "Playing " + Index.ToString();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void GetComments(Stream stream)
        {
            if (stream == null)
                return;

            var comments = new ImageCommentEmbedder().GetComments(stream);

            OnCommentsFetched?.Invoke(comments);
        }


        private void frmPictureViewer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 39 || e.KeyValue == 40)
                {
                    Index++;
                    OnIndexChanged(Index);
                }
                if (e.KeyValue == 37 || e.KeyValue == 38)
                {
                    if (Index < 1)
                        return;
                    Index--;
                    OnIndexChanged(Index);
                }

                if (e.KeyCode == Keys.Space)
                {
                    TogglePausePlay();
                }

                if (e.KeyCode == Keys.Escape)
                {
                    MinimizeWindow();
                }
            }

            catch { }
        }

        private void frmPictureViewer_MaximumSizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.None;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

        }

        private void frmPictureViewer_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                pictureBox1.Dock = DockStyle.Fill;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.Dock = DockStyle.None;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TogglePausePlay();
        }

        private void TogglePausePlay()
        {
            timer2.Enabled = !timer2.Enabled;


            if (timer2.Enabled)
            {
                this.Text = "Playing" + Index.ToString();
            }
            else
            {
                this.Text = "Paused " + Index.ToString();
            }
        }
    }
}
