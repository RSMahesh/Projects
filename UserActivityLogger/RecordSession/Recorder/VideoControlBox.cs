using EventPublisher;
using RecordSession;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransparentWindow;

namespace Recorder
{
    public partial class VideoControlBox : Form
    {
        frmPictureViewer _pictureViewerFrom = null;
        int originalInterval;
        public VideoControlBox(frmPictureViewer pictureViewerFrom, int count)
        {
            _pictureViewerFrom = pictureViewerFrom;
            _pictureViewerFrom.OnIndexChanged = new Action<int>(OnIndexChange);
            _pictureViewerFrom.DisplayChange = new Action<int>(DisplayChange);
            InitializeComponent();
            trackBar1.Maximum = count;
            EventContainer.SubscribeEvent(RecordSession.Events.OnPictureViwerResize.ToString(), OnPictureViwerResize);

        }
        private void VideoControlBox_Load(object sender, EventArgs e)
        {
           // this.BackColor = Color.LimeGreen;
          //  this.TransparencyKey = Color.LimeGreen;
            this.TopMost = true;
           // this.trackBar1.BackColor = Color.LimeGreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Top = _pictureViewerFrom.MdiParent.Bottom - this.Height;
            this.Opacity = 0.5;
         
            // var transpainter = new Transpainter(this);
            // transpainter.MakeTranseparent();
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
            this.Top = _pictureViewerFrom.MdiParent.Bottom  - this.Height;
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            
        }

        private void btnForward_MouseDown(object sender, MouseEventArgs e)
        {
            originalInterval = _pictureViewerFrom.timer2.Interval;
            _pictureViewerFrom.timer2.Interval = 1;
        }

        private void btnForward_MouseUp(object sender, MouseEventArgs e)
        {
            _pictureViewerFrom.timer2.Interval = originalInterval;
        }

        private void chkForward_CheckedChanged(object sender, EventArgs e)
        {
            if(chkForward.Checked)
            {
                originalInterval = _pictureViewerFrom.timer2.Interval;
                _pictureViewerFrom.timer2.Interval = 1;
            }
            else
            {
                _pictureViewerFrom.timer2.Interval = originalInterval;
            }

        }
    }
}
