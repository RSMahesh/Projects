using EventPublisher;
using RecordSession;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Recorder
{
    public partial class VideoControlBox : Form
    {
        frmPictureViewer _pictureViewerFrom = null;
        int _originalInterval = 1000;
        const int _fastSpeed = 10;
        public VideoControlBox(frmPictureViewer pictureViewerFrom, int count)
        {
            _pictureViewerFrom = pictureViewerFrom;
            _pictureViewerFrom.OnIndexChanged = new Action<int>(OnIndexChange);
            _pictureViewerFrom.DisplayChange = new Action<int>(DisplayChange);
            InitializeComponent();
            trackBar1.Minimum = 0;
            trackBar1.Maximum = count-1;
            EventContainer.SubscribeEvent(RecordSession.Events.OnPictureViwerResize.ToString(), OnPictureViwerResize);
            EventContainer.SubscribeEvent(RecordSession.Events.CloseCurrentSession.ToString(), OnCloseCurrentSession);
            EventContainer.SubscribeEvent(RecordSession.Events.VideoPaused.ToString(), SetPlayButtonText);
        }

        private void VideoControlBox_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;

            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            OnPictureViwerResize(null);
            this.Opacity = 0.8;
            this.trackBar1.BackColor = Color.LimeGreen;

        }

        private void OnIndexChange(int index)
        {
            trackBar1.Value = index;
            _pictureViewerFrom.ChangeNextImagePostion(trackBar1.Value);
        }

        private void DisplayChange(int index)
        {
            try
            {
                trackBar1.Value = index;
                lblCurrentIndex.Text = index.ToString();
            }
            catch (Exception ex)
            {
               
                MessageBox.Show(ex.ToString());
            }
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            _pictureViewerFrom.Index = trackBar1.Value;
            _pictureViewerFrom.ChangeNextImagePostion(trackBar1.Value);
        }

        private void VideoControlBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        private void VideoControlBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
        }

        private void OnPictureViwerResize(EventArg eventArg)
        {
            if (_pictureViewerFrom.MdiParent != null)
            {
                this.Top = _pictureViewerFrom.MdiParent.Bottom - (this.Height + 50);
                this.Left = _pictureViewerFrom.MdiParent.Left + (_pictureViewerFrom.MdiParent.Width / 2) - (this.Width / 2);
            }
        }

        private void OnCloseCurrentSession(EventArg eventArg)
        {
            this.Close();
            this.Dispose();
        }


        private void SetPlayButtonText(EventArg eventArg)
        {
            if (!_pictureViewerFrom.timer2.Enabled)
            {
                btnPlay.Text = "Play";
            }
            else
            {
                btnPlay.Text = "Pause";
            }
        }
        private void btnForward_MouseDown(object sender, MouseEventArgs e)
        {
            _originalInterval = _pictureViewerFrom.timer2.Interval;
            _pictureViewerFrom.timer2.Interval = 1;
        }

        private void btnForward_MouseUp(object sender, MouseEventArgs e)
        {
            _pictureViewerFrom.timer2.Interval = _originalInterval;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _pictureViewerFrom.timer2.Interval = _originalInterval;
            _pictureViewerFrom.timer2.Enabled = !_pictureViewerFrom.timer2.Enabled;
            SetPlayButtonText(null);
        }

        private void btnBackward_MouseDown(object sender, MouseEventArgs e)
        {
            _pictureViewerFrom.incrementCount = -1;
            _originalInterval = _pictureViewerFrom.timer2.Interval;

            _pictureViewerFrom.FastMode = true;
            _pictureViewerFrom.timer2.Interval = _fastSpeed;

        }

        private void btnBackward_MouseUp(object sender, MouseEventArgs e)
        {
            _pictureViewerFrom.incrementCount = 1;
            _pictureViewerFrom.FastMode = false;
            _pictureViewerFrom.timer2.Interval = _originalInterval;
        }

        private void btnForward_MouseDown_1(object sender, MouseEventArgs e)
        {
            _originalInterval = _pictureViewerFrom.timer2.Interval;
            _pictureViewerFrom.FastMode = true;
            _pictureViewerFrom.timer2.Interval = _fastSpeed;
        }

        private void btnForward_MouseUp_1(object sender, MouseEventArgs e)
        {
            _pictureViewerFrom.FastMode = false;
            _pictureViewerFrom.timer2.Interval = _originalInterval;
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {

        }
    }
}
