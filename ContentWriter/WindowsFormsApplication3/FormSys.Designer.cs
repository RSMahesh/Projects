namespace WindowsFormsApplication3
{
    partial class FormSys
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
            this.lbSynonym = new System.Windows.Forms.ListBox();
            this.tbWord = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbSynonym
            // 
            this.lbSynonym.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSynonym.FormattingEnabled = true;
            this.lbSynonym.ItemHeight = 18;
            this.lbSynonym.Location = new System.Drawing.Point(3, 33);
            this.lbSynonym.Name = "lbSynonym";
            this.lbSynonym.Size = new System.Drawing.Size(225, 292);
            this.lbSynonym.TabIndex = 0;
            this.lbSynonym.SelectedIndexChanged += new System.EventHandler(this.lbSynonym_SelectedIndexChanged);
            // 
            // tbWord
            // 
            this.tbWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWord.Location = new System.Drawing.Point(3, 0);
            this.tbWord.Name = "tbWord";
            this.tbWord.Size = new System.Drawing.Size(225, 24);
            this.tbWord.TabIndex = 1;
            this.tbWord.Text = "Pretty";
            this.tbWord.TextChanged += new System.EventHandler(this.tbWord_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(51, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FormSys
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 326);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbWord);
            this.Controls.Add(this.lbSynonym);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSys";
            this.Text = "FormSys";
            this.Load += new System.EventHandler(this.FormSys_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbSynonym;
        private System.Windows.Forms.TextBox tbWord;
        private System.Windows.Forms.Button button1;
    }
}