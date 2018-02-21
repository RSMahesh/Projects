namespace WindowsFormsApplication3
{
    partial class Filter
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
            this.cmbFields = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbOpreator = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbFields
            // 
            this.cmbFields.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFields.FormattingEnabled = true;
            this.cmbFields.Location = new System.Drawing.Point(129, 9);
            this.cmbFields.Name = "cmbFields";
            this.cmbFields.Size = new System.Drawing.Size(300, 26);
            this.cmbFields.TabIndex = 0;
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(129, 73);
            this.txtFilterValue.Multiline = true;
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(300, 54);
            this.txtFilterValue.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(318, 35);
            this.button1.TabIndex = 2;
            this.button1.Text = "Filter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbOpreator
            // 
            this.cmbOpreator.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOpreator.FormattingEnabled = true;
            this.cmbOpreator.Items.AddRange(new object[] {
            "Equal",
            "Contains",
            "Not Contains",
            "Not Equal"});
            this.cmbOpreator.Location = new System.Drawing.Point(129, 41);
            this.cmbOpreator.Name = "cmbOpreator";
            this.cmbOpreator.Size = new System.Drawing.Size(94, 26);
            this.cmbOpreator.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Column";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Opreation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Filter Text";
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 265);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbOpreator);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.cmbFields);
            this.Name = "Filter";
            this.Text = "Filter";
            this.Load += new System.EventHandler(this.Filter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFields;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbOpreator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}