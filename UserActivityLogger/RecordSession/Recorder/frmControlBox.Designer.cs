namespace RecordSession
{
    partial class frmControlBox
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.UpDownTimer = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UpDownSilde = new System.Windows.Forms.NumericUpDown();
            this.chkBackWard = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOPenFile = new System.Windows.Forms.Button();
            this.txtKeysLogged = new System.Windows.Forms.TextBox();
            this.btnClearTextBox = new System.Windows.Forms.Button();
            this.txtCurrentText = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownSilde)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(432, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Enter Folder Path";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(488, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // UpDownTimer
            // 
            this.UpDownTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpDownTimer.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.UpDownTimer.Location = new System.Drawing.Point(190, 38);
            this.UpDownTimer.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.UpDownTimer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownTimer.Name = "UpDownTimer";
            this.UpDownTimer.Size = new System.Drawing.Size(47, 23);
            this.UpDownTimer.TabIndex = 14;
            this.UpDownTimer.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.UpDownTimer.ValueChanged += new System.EventHandler(this.UpDownTimer_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Timer Interval";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Slide Increment";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // UpDownSilde
            // 
            this.UpDownSilde.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpDownSilde.Location = new System.Drawing.Point(332, 38);
            this.UpDownSilde.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownSilde.Name = "UpDownSilde";
            this.UpDownSilde.Size = new System.Drawing.Size(47, 23);
            this.UpDownSilde.TabIndex = 16;
            this.UpDownSilde.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownSilde.ValueChanged += new System.EventHandler(this.UpDownSilde_ValueChanged);
            // 
            // chkBackWard
            // 
            this.chkBackWard.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBackWard.AutoSize = true;
            this.chkBackWard.Location = new System.Drawing.Point(12, 38);
            this.chkBackWard.Name = "chkBackWard";
            this.chkBackWard.Size = new System.Drawing.Size(83, 23);
            this.chkBackWard.TabIndex = 18;
            this.chkBackWard.Text = "<< BackWard";
            this.chkBackWard.UseVisualStyleBackColor = true;
            this.chkBackWard.CheckedChanged += new System.EventHandler(this.chkBackWard_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOPenFile
            // 
            this.btnOPenFile.Location = new System.Drawing.Point(438, 12);
            this.btnOPenFile.Name = "btnOPenFile";
            this.btnOPenFile.Size = new System.Drawing.Size(44, 23);
            this.btnOPenFile.TabIndex = 19;
            this.btnOPenFile.Text = "...";
            this.btnOPenFile.UseVisualStyleBackColor = true;
            this.btnOPenFile.Click += new System.EventHandler(this.btnOPenFile_Click);
            // 
            // txtKeysLogged
            // 
            this.txtKeysLogged.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeysLogged.Location = new System.Drawing.Point(12, 96);
            this.txtKeysLogged.Multiline = true;
            this.txtKeysLogged.Name = "txtKeysLogged";
            this.txtKeysLogged.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtKeysLogged.Size = new System.Drawing.Size(577, 70);
            this.txtKeysLogged.TabIndex = 20;
            this.txtKeysLogged.UseSystemPasswordChar = true;
            // 
            // btnClearTextBox
            // 
            this.btnClearTextBox.Location = new System.Drawing.Point(396, 41);
            this.btnClearTextBox.Name = "btnClearTextBox";
            this.btnClearTextBox.Size = new System.Drawing.Size(95, 23);
            this.btnClearTextBox.TabIndex = 21;
            this.btnClearTextBox.Text = "Clear TextBox";
            this.btnClearTextBox.UseVisualStyleBackColor = true;
            this.btnClearTextBox.Click += new System.EventHandler(this.btnClearTextBox_Click);
            // 
            // txtCurrentText
            // 
            this.txtCurrentText.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentText.Location = new System.Drawing.Point(12, 70);
            this.txtCurrentText.Name = "txtCurrentText";
            this.txtCurrentText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCurrentText.Size = new System.Drawing.Size(577, 22);
            this.txtCurrentText.TabIndex = 24;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(497, 37);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 25;
            this.btnSearch.Text = "SearchNext";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(12, 172);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(113, 20);
            this.txtPassword.TabIndex = 26;
            this.txtPassword.Text = "1234test!";
            // 
            // frmControlBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 194);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCurrentText);
            this.Controls.Add(this.btnClearTextBox);
            this.Controls.Add(this.txtKeysLogged);
            this.Controls.Add(this.btnOPenFile);
            this.Controls.Add(this.chkBackWard);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UpDownSilde);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpDownTimer);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.textBox1);
            this.KeyPreview = true;
            this.Name = "frmControlBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Control Box";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownSilde)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.NumericUpDown UpDownTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown UpDownSilde;
        private System.Windows.Forms.CheckBox chkBackWard;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOPenFile;
        private System.Windows.Forms.TextBox txtKeysLogged;
        private System.Windows.Forms.Button btnClearTextBox;
        private System.Windows.Forms.TextBox txtCurrentText;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtPassword;
    }
}