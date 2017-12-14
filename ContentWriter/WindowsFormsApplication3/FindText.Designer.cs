namespace WindowsFormsApplication3
{
    partial class FindText
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
            this.btnFindNext = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReplaceText = new System.Windows.Forms.TextBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFindNext
            // 
            this.btnFindNext.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindNext.Location = new System.Drawing.Point(24, 73);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(168, 32);
            this.btnFindNext.TabIndex = 1;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(99, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 23);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text To Find";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Replace Text";
            // 
            // txtReplaceText
            // 
            this.txtReplaceText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReplaceText.Location = new System.Drawing.Point(99, 31);
            this.txtReplaceText.Name = "txtReplaceText";
            this.txtReplaceText.Size = new System.Drawing.Size(150, 23);
            this.txtReplaceText.TabIndex = 3;
            // 
            // btnReplace
            // 
            this.btnReplace.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplace.Location = new System.Drawing.Point(239, 73);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(168, 32);
            this.btnReplace.TabIndex = 5;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // FindText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 179);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReplaceText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.textBox1);
            this.Name = "FindText";
            this.Text = "FindText";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReplaceText;
        private System.Windows.Forms.Button btnReplace;
    }
}