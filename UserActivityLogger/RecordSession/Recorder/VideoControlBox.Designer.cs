namespace Recorder
{
    partial class VideoControlBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkForward = new System.Windows.Forms.CheckBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.lblCurrentIndex = new System.Windows.Forms.Label();
            this.chkBackward = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar1.Location = new System.Drawing.Point(-5, -2);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(757, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBackward);
            this.groupBox1.Controls.Add(this.chkForward);
            this.groupBox1.Controls.Add(this.btnPlay);
            this.groupBox1.Location = new System.Drawing.Point(178, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 47);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // chkForward
            // 
            this.chkForward.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkForward.AutoSize = true;
            this.chkForward.Location = new System.Drawing.Point(287, 19);
            this.chkForward.Name = "chkForward";
            this.chkForward.Size = new System.Drawing.Size(29, 23);
            this.chkForward.TabIndex = 2;
            this.chkForward.Text = ">>";
            this.chkForward.UseVisualStyleBackColor = true;
            this.chkForward.CheckedChanged += new System.EventHandler(this.chkForward_CheckedChanged);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(170, 19);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // lblCurrentIndex
            // 
            this.lblCurrentIndex.AutoSize = true;
            this.lblCurrentIndex.Location = new System.Drawing.Point(665, 29);
            this.lblCurrentIndex.Name = "lblCurrentIndex";
            this.lblCurrentIndex.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentIndex.TabIndex = 2;
            this.lblCurrentIndex.Text = "label1";
            // 
            // chkBackward
            // 
            this.chkBackward.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBackward.AutoSize = true;
            this.chkBackward.Location = new System.Drawing.Point(97, 18);
            this.chkBackward.Name = "chkBackward";
            this.chkBackward.Size = new System.Drawing.Size(29, 23);
            this.chkBackward.TabIndex = 3;
            this.chkBackward.Text = "<<";
            this.chkBackward.UseVisualStyleBackColor = true;
            this.chkBackward.CheckedChanged += new System.EventHandler(this.chkBackward_CheckedChanged);
            // 
            // VideoControlBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 83);
            this.Controls.Add(this.lblCurrentIndex);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.trackBar1);
            this.KeyPreview = true;
            this.Name = "VideoControlBox";
            this.Text = "VideoControlBox";
            this.Load += new System.EventHandler(this.VideoControlBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VideoControlBox_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VideoControlBox_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lblCurrentIndex;
        private System.Windows.Forms.CheckBox chkForward;
        private System.Windows.Forms.CheckBox chkBackward;
    }
}