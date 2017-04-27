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
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownSilde)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(3, 28);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(510, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseDown);
            this.trackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(432, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Enter Folder Path";
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(523, 28);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 8;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(523, 2);
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
            this.UpDownTimer.Location = new System.Drawing.Point(227, 69);
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
            300,
            0,
            0,
            0});
            this.UpDownTimer.ValueChanged += new System.EventHandler(this.UpDownTimer_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Timer Interval";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Slide Increment";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // UpDownSilde
            // 
            this.UpDownSilde.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpDownSilde.Location = new System.Drawing.Point(369, 69);
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
            this.chkBackWard.Location = new System.Drawing.Point(33, 80);
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
            this.btnOPenFile.Location = new System.Drawing.Point(438, 2);
            this.btnOPenFile.Name = "btnOPenFile";
            this.btnOPenFile.Size = new System.Drawing.Size(44, 23);
            this.btnOPenFile.TabIndex = 19;
            this.btnOPenFile.Text = "...";
            this.btnOPenFile.UseVisualStyleBackColor = true;
            this.btnOPenFile.Click += new System.EventHandler(this.btnOPenFile_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 107);
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
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Control Box";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
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
    }
}