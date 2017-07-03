namespace Recorder
{
    partial class CommentsScreen
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
            this.txtCurrentText = new System.Windows.Forms.TextBox();
            this.txtKeysLogged = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCurrentText
            // 
            this.txtCurrentText.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentText.Location = new System.Drawing.Point(12, 12);
            this.txtCurrentText.Name = "txtCurrentText";
            this.txtCurrentText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCurrentText.Size = new System.Drawing.Size(577, 22);
            this.txtCurrentText.TabIndex = 26;
            // 
            // txtKeysLogged
            // 
            this.txtKeysLogged.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeysLogged.Location = new System.Drawing.Point(12, 38);
            this.txtKeysLogged.Multiline = true;
            this.txtKeysLogged.Name = "txtKeysLogged";
            this.txtKeysLogged.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtKeysLogged.Size = new System.Drawing.Size(577, 70);
            this.txtKeysLogged.TabIndex = 25;
            this.txtKeysLogged.UseSystemPasswordChar = true;
            // 
            // CommentsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 118);
            this.Controls.Add(this.txtCurrentText);
            this.Controls.Add(this.txtKeysLogged);
            this.Name = "CommentsScreen";
            this.Text = "CommentsScreen";
            this.Load += new System.EventHandler(this.CommentsScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCurrentText;
        private System.Windows.Forms.TextBox txtKeysLogged;
    }
}