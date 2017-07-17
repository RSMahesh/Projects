namespace Recorder
{
    partial class Settings
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
            this.trackBarFastSpeed = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFastSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarFastSpeed
            // 
            this.trackBarFastSpeed.Location = new System.Drawing.Point(12, 54);
            this.trackBarFastSpeed.Maximum = 5000;
            this.trackBarFastSpeed.Minimum = 1;
            this.trackBarFastSpeed.Name = "trackBarFastSpeed";
            this.trackBarFastSpeed.Size = new System.Drawing.Size(243, 45);
            this.trackBarFastSpeed.TabIndex = 0;
            this.trackBarFastSpeed.Value = 10;
            this.trackBarFastSpeed.Scroll += new System.EventHandler(this.trackBarFastSpeed_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fast Speed";
            // 
            // lblVal
            // 
            this.lblVal.AutoSize = true;
            this.lblVal.Location = new System.Drawing.Point(71, 102);
            this.lblVal.Name = "lblVal";
            this.lblVal.Size = new System.Drawing.Size(35, 13);
            this.lblVal.TabIndex = 2;
            this.lblVal.Text = "label2";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lblVal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarFastSpeed);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFastSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarFastSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVal;
    }
}