namespace TransparentWindow
{
    partial class TransparentFrm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TransparentFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(392, 141);
            this.KeyPreview = true;
            this.Name = "TransparentFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TransparentFrm_FormClosed);
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResizeBegin += new System.EventHandler(this.TransparentFrm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.TransparentFrm_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.TransparentFrm_LocationChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransparentFrm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TransparentFrm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TransparentFrm_KeyUp);
            this.Resize += new System.EventHandler(this.TransparentFrm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;

    }
}