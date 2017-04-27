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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnPause = new System.Windows.Forms.Button();
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
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.lblCurrentValue = new System.Windows.Forms.Label();
            this.txtCurrentText = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownSilde)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(3, 1);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(514, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            this.trackBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseDown);
            this.trackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(432, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Enter Folder Path";
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(523, 90);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 8;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(523, 61);
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
            this.UpDownTimer.Location = new System.Drawing.Point(190, 142);
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
            this.label1.Location = new System.Drawing.Point(111, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Timer Interval";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Slide Increment";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // UpDownSilde
            // 
            this.UpDownSilde.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpDownSilde.Location = new System.Drawing.Point(332, 142);
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
            this.chkBackWard.Location = new System.Drawing.Point(12, 142);
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
            this.btnOPenFile.Location = new System.Drawing.Point(438, 64);
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
            this.txtKeysLogged.Location = new System.Drawing.Point(12, 200);
            this.txtKeysLogged.Multiline = true;
            this.txtKeysLogged.Name = "txtKeysLogged";
            this.txtKeysLogged.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtKeysLogged.Size = new System.Drawing.Size(577, 70);
            this.txtKeysLogged.TabIndex = 20;
            this.txtKeysLogged.UseSystemPasswordChar = true;
            // 
            // btnClearTextBox
            // 
            this.btnClearTextBox.Location = new System.Drawing.Point(396, 145);
            this.btnClearTextBox.Name = "btnClearTextBox";
            this.btnClearTextBox.Size = new System.Drawing.Size(95, 23);
            this.btnClearTextBox.TabIndex = 21;
            this.btnClearTextBox.Text = "Clear TextBox";
            this.btnClearTextBox.UseVisualStyleBackColor = true;
            this.btnClearTextBox.Click += new System.EventHandler(this.btnClearTextBox_Click);
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Location = new System.Drawing.Point(482, 33);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(53, 13);
            this.lblMaxValue.TabIndex = 22;
            this.lblMaxValue.Text = "maxValue";
            // 
            // lblCurrentValue
            // 
            this.lblCurrentValue.AutoSize = true;
            this.lblCurrentValue.Location = new System.Drawing.Point(393, 33);
            this.lblCurrentValue.Name = "lblCurrentValue";
            this.lblCurrentValue.Size = new System.Drawing.Size(68, 13);
            this.lblCurrentValue.TabIndex = 23;
            this.lblCurrentValue.Text = "CurrentValue";
            // 
            // txtCurrentText
            // 
            this.txtCurrentText.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentText.Location = new System.Drawing.Point(12, 174);
            this.txtCurrentText.Name = "txtCurrentText";
            this.txtCurrentText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCurrentText.Size = new System.Drawing.Size(577, 22);
            this.txtCurrentText.TabIndex = 24;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(514, 145);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 25;
            this.btnSearch.Text = "SearchNext";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(23, 277);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(113, 20);
            this.txtPassword.TabIndex = 26;
            this.txtPassword.Text = "Enter Pass";
            // 
            // frmControlBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 295);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCurrentText);
            this.Controls.Add(this.lblCurrentValue);
            this.Controls.Add(this.lblMaxValue);
            this.Controls.Add(this.btnClearTextBox);
            this.Controls.Add(this.txtKeysLogged);
            this.Controls.Add(this.btnOPenFile);
            this.Controls.Add(this.chkBackWard);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UpDownSilde);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpDownTimer);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.trackBar1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmControlBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Control Box";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownSilde)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnPause;
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
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Label lblCurrentValue;
        private System.Windows.Forms.TextBox txtCurrentText;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtPassword;
    }
}