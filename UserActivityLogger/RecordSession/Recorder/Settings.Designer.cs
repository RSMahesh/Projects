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
            trackBarFastSpeed = new System.Windows.Forms.TrackBar();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(trackBarFastSpeed)).BeginInit();
            SuspendLayout();
            // 
            // trackBarFastSpeed
            // 
            trackBarFastSpeed.Location = new System.Drawing.Point(12, 54);
            trackBarFastSpeed.Maximum = 5000;
            trackBarFastSpeed.Minimum = 1;
            trackBarFastSpeed.Name = "trackBarFastSpeed";
            trackBarFastSpeed.Size = new System.Drawing.Size(243, 45);
            trackBarFastSpeed.TabIndex = 0;
            trackBarFastSpeed.Value = 10;
            trackBarFastSpeed.Scroll += new System.EventHandler(trackBarFastSpeed_Scroll);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(23, 38);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(61, 13);
            label1.TabIndex = 1;
            label1.Text = "Fast Speed";
            // 
            // Settings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(284, 262);
            Controls.Add(label1);
            Controls.Add(trackBarFastSpeed);
            Name = "Settings";
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(trackBarFastSpeed)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarFastSpeed;
        private System.Windows.Forms.Label label1;
    }
}