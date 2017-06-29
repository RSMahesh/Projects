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
        public VideoControlBox(frmPictureViewer pictureViewerFrom, int count)
        {
            _pictureViewerFrom = pictureViewerFrom;
            _pictureViewerFrom.OnIndexChanged = new Action<int>(OnIndexChange);
            _pictureViewerFrom.DisplayChange = new Action<int>(DisplayChange);
            InitializeComponent();
            trackBar1.Maximum = count;
           
        }
        public VideoControlBox()
        {
            
        }

        private void VideoControlBox_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;


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
    }
}
