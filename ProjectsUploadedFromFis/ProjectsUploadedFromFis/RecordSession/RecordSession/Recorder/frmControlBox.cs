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

namespace RecordSession
{
    public partial class frmControlBox : Form
    {
        frmPictureViewer form1 = null;
        public frmControlBox()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            form1.index = trackBar1.Value;
            form1.pictureBox1.ImageLocation = form1.files[trackBar1.Value].FullName;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (form1 != null)
            {
                try
                {
                    form1.Close();
                }
                catch { }
            }
            trackBar1.Value = 0;

            form1 = new frmPictureViewer();
            form1.DisplayChange = new Action<int>(DisplayChange);
            form1.WindowState = FormWindowState.Maximized;
            form1.Show();

            trackBar1.Maximum = form1.Play(textBox1.Text);
            trackBar1.Minimum = 0;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            Application.ThreadException += new ThreadExceptionEventHandler(MyCommonExceptionHandlingMethod);
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            form1.timer2.Enabled = false;
        }

        private void DisplayChange(int index)
        {
            
            trackBar1.Value = index;
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            form1.timer2.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (form1.timer2.Enabled)
            {
                btnPause.Text = "Ressume";
                form1.timer2.Enabled = false;
            }
            else
            {
                btnPause.Text = "Pause";
                form1.timer2.Enabled = true;
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
                    form1.index = trackBar1.Value;

                }
                if (e.KeyValue == 37)
                {
                    trackBar1.Value -= 1;
                    form1.index = trackBar1.Value;
                }
            }
            catch { }
        }

       

        private void UpDownTimer_ValueChanged(object sender, EventArgs e)
        {
            form1.timer2.Interval =(int) UpDownTimer.Value;
        }

        private void UpDownSilde_ValueChanged(object sender, EventArgs e)
        {
            form1.incrementCount = (int) UpDownSilde.Value;
        }

        private void chkBackWard_CheckedChanged(object sender, EventArgs e)
        {
            if(chkBackWard.Checked)
            form1.incrementCount = -System.Math.Abs(form1.incrementCount);
            else
            form1.incrementCount = System.Math.Abs(form1.incrementCount);

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
    }
}
