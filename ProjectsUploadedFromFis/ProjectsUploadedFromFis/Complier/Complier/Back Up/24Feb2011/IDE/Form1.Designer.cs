namespace IDE
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.rchTxtWindow.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtWindow.Location = new System.Drawing.Point(1, 0);
            this.rchTxtWindow.Name = "richTextBox1";
            this.rchTxtWindow.Size = new System.Drawing.Size(351, 224);
            this.rchTxtWindow.TabIndex = 1;
            this.rchTxtWindow.Text = "";
            this.rchTxtWindow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            this.rchTxtWindow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPress);
            // 
            // listMapping
            // 
            this.listMapping.Font = new System.Drawing.Font("Shusha", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listMapping.FormattingEnabled = true;
            this.listMapping.ItemHeight = 18;
            this.listMapping.Location = new System.Drawing.Point(135, 115);
            this.listMapping.Name = "listMapping";
            this.listMapping.Size = new System.Drawing.Size(136, 130);
            this.listMapping.TabIndex = 3;
            this.listMapping.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 266);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listMapping);
            this.Controls.Add(this.rchTxtWindow);
            this.Name = "Form1";
            this.Text = "IDE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rchTxtWindow;
        private System.Windows.Forms.ListBox listMapping;
        private System.Windows.Forms.Button button1;
    }
}

