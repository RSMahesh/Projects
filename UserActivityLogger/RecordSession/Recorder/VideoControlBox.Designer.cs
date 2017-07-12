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
            trackBar1 = new System.Windows.Forms.TrackBar();
            lblCurrentIndex = new System.Windows.Forms.Label();
            btnPlay = new System.Windows.Forms.Button();
            btnForward = new System.Windows.Forms.Button();
            btnBackward = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(trackBar1)).BeginInit();
            SuspendLayout();
            // 
            // trackBar1
            // 
            trackBar1.BackColor = System.Drawing.SystemColors.Control;
            trackBar1.Location = new System.Drawing.Point(-5, -2);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new System.Drawing.Size(757, 45);
            trackBar1.TabIndex = 0;
            trackBar1.Scroll += new System.EventHandler(trackBar1_Scroll_1);
            // 
            // lblCurrentIndex
            // 
            lblCurrentIndex.AutoSize = true;
            lblCurrentIndex.Location = new System.Drawing.Point(667, 29);
            lblCurrentIndex.Name = "lblCurrentIndex";
            lblCurrentIndex.Size = new System.Drawing.Size(35, 13);
            lblCurrentIndex.TabIndex = 2;
            lblCurrentIndex.Text = "label1";
            // 
            // btnPlay
            // 
            btnPlay.Location = new System.Drawing.Point(361, 19);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new System.Drawing.Size(75, 34);
            btnPlay.TabIndex = 4;
            btnPlay.Text = "Pause";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += new System.EventHandler(btnPlay_Click);
            // 
            // btnForward
            // 
            btnForward.Location = new System.Drawing.Point(432, 19);
            btnForward.Name = "btnForward";
            btnForward.Size = new System.Drawing.Size(75, 34);
            btnForward.TabIndex = 5;
            btnForward.Text = ">>";
            btnForward.UseVisualStyleBackColor = true;
            btnForward.MouseDown += new System.Windows.Forms.MouseEventHandler(btnForward_MouseDown_1);
            btnForward.MouseUp += new System.Windows.Forms.MouseEventHandler(btnForward_MouseUp_1);
            // 
            // btnBackward
            // 
            btnBackward.Location = new System.Drawing.Point(292, 19);
            btnBackward.Name = "btnBackward";
            btnBackward.Size = new System.Drawing.Size(75, 34);
            btnBackward.TabIndex = 6;
            btnBackward.Text = "<<";
            btnBackward.UseVisualStyleBackColor = true;
            btnBackward.Click += new System.EventHandler(btnBackward_Click);
            btnBackward.MouseDown += new System.Windows.Forms.MouseEventHandler(btnBackward_MouseDown);
            btnBackward.MouseUp += new System.Windows.Forms.MouseEventHandler(btnBackward_MouseUp);
            // 
            // VideoControlBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(790, 56);
            Controls.Add(btnBackward);
            Controls.Add(btnForward);
            Controls.Add(btnPlay);
            Controls.Add(lblCurrentIndex);
            Controls.Add(trackBar1);
            KeyPreview = true;
            Name = "VideoControlBox";
            Text = "VideoControlBox";
            Load += new System.EventHandler(VideoControlBox_Load);
            KeyDown += new System.Windows.Forms.KeyEventHandler(VideoControlBox_KeyDown);
            KeyUp += new System.Windows.Forms.KeyEventHandler(VideoControlBox_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(trackBar1)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblCurrentIndex;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBackward;
    }
}