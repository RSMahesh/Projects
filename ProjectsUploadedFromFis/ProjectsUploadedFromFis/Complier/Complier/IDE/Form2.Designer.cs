namespace IDE
{
    partial class Form2
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
            this.rchTxtWindow = new System.Windows.Forms.RichTextBox();
            this.listMapping = new System.Windows.Forms.ListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rchTxtWindow
            // 
            this.rchTxtWindow.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtWindow.Location = new System.Drawing.Point(1, 24);
            this.rchTxtWindow.Name = "rchTxtWindow";
            this.rchTxtWindow.Size = new System.Drawing.Size(351, 224);
            this.rchTxtWindow.TabIndex = 1;
            this.rchTxtWindow.Text = "";
            this.rchTxtWindow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            this.rchTxtWindow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPress);
            // 
            // listMapping
            // 
            this.listMapping.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listMapping.Font = new System.Drawing.Font("Shusha", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listMapping.FormattingEnabled = true;
            this.listMapping.ItemHeight = 18;
            this.listMapping.Location = new System.Drawing.Point(135, 115);
            this.listMapping.Name = "listMapping";
            this.listMapping.Size = new System.Drawing.Size(136, 128);
            this.listMapping.TabIndex = 3;
            this.listMapping.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(300, 237);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Location = new System.Drawing.Point(159, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 15);
            this.label1.TabIndex = 12;
            this.label1.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 266);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listMapping);
            this.Controls.Add(this.rchTxtWindow);
            this.Name = "Form2";
            this.Text = "IDE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rchTxtWindow;
        private System.Windows.Forms.ListBox listMapping;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
    }
}