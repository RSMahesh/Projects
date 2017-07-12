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
            txtCurrentText = new System.Windows.Forms.TextBox();
            txtKeysLogged = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // txtCurrentText
            // 
            txtCurrentText.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtCurrentText.Location = new System.Drawing.Point(12, 12);
            txtCurrentText.Name = "txtCurrentText";
            txtCurrentText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtCurrentText.Size = new System.Drawing.Size(577, 22);
            txtCurrentText.TabIndex = 26;
            // 
            // txtKeysLogged
            // 
            txtKeysLogged.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtKeysLogged.Location = new System.Drawing.Point(12, 38);
            txtKeysLogged.Multiline = true;
            txtKeysLogged.Name = "txtKeysLogged";
            txtKeysLogged.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtKeysLogged.Size = new System.Drawing.Size(577, 70);
            txtKeysLogged.TabIndex = 25;
            txtKeysLogged.UseSystemPasswordChar = true;
            // 
            // CommentsScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(594, 118);
            Controls.Add(txtCurrentText);
            Controls.Add(txtKeysLogged);
            Name = "CommentsScreen";
            Text = "CommentsScreen";
            Load += new System.EventHandler(CommentsScreen_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCurrentText;
        private System.Windows.Forms.TextBox txtKeysLogged;
    }
}