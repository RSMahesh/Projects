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
            this.SuspendLayout();
            // 
            // TransparentFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 633);
            this.KeyPreview = true;
            this.Name = "TransparentFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClickThrough Window";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.LocationChanged += new System.EventHandler(this.TransparentFrm_LocationChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransparentFrm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TransparentFrm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TransparentFrm_KeyUp);
            this.Resize += new System.EventHandler(this.TransparentFrm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

    }
}