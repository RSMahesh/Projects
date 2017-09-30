namespace StatusMaker.UI
{
    partial class MainForm
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnOpenInOutlook = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnShowTodaysStatus = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCCRecipients = new System.Windows.Forms.TextBox();
            this.chkValidateJiraStatus = new System.Windows.Forms.CheckBox();
            this.btnSetStatusFilePath = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMemberName = new System.Windows.Forms.TextBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBackWard = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnCommitExcel = new System.Windows.Forms.Button();
            this.btnShowExcel = new System.Windows.Forms.Button();
            this.txtTORecipients = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnUnlockForeFully = new System.Windows.Forms.Button();
            this.btnCleanSetting = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenInOutlook
            // 
            this.btnOpenInOutlook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenInOutlook.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenInOutlook.Location = new System.Drawing.Point(226, 97);
            this.btnOpenInOutlook.Name = "btnOpenInOutlook";
            this.btnOpenInOutlook.Size = new System.Drawing.Size(173, 44);
            this.btnOpenInOutlook.TabIndex = 1;
            this.btnOpenInOutlook.Text = "Open in Outlook / Copy";
            this.btnOpenInOutlook.UseVisualStyleBackColor = true;
            this.btnOpenInOutlook.Click += new System.EventHandler(this.btnOpenInOutlook_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1463, 449);
            this.webBrowser1.TabIndex = 2;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // btnShowTodaysStatus
            // 
            this.btnShowTodaysStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowTodaysStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowTodaysStatus.Location = new System.Drawing.Point(30, 97);
            this.btnShowTodaysStatus.Name = "btnShowTodaysStatus";
            this.btnShowTodaysStatus.Size = new System.Drawing.Size(173, 44);
            this.btnShowTodaysStatus.TabIndex = 3;
            this.btnShowTodaysStatus.Text = "Show Todays Status";
            this.btnShowTodaysStatus.UseVisualStyleBackColor = true;
            this.btnShowTodaysStatus.Click += new System.EventHandler(this.btnShowTodaysStatus_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCCRecipients);
            this.groupBox1.Controls.Add(this.chkValidateJiraStatus);
            this.groupBox1.Controls.Add(this.btnSetStatusFilePath);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnCommitExcel);
            this.groupBox1.Controls.Add(this.btnShowExcel);
            this.groupBox1.Controls.Add(this.txtTORecipients);
            this.groupBox1.Controls.Add(this.btnShowTodaysStatus);
            this.groupBox1.Controls.Add(this.btnOpenInOutlook);
            this.groupBox1.Location = new System.Drawing.Point(59, 547);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1387, 169);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(461, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "CC";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "TO";
            // 
            // txtCCRecipients
            // 
            this.txtCCRecipients.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCRecipients.Location = new System.Drawing.Point(488, 22);
            this.txtCCRecipients.Multiline = true;
            this.txtCCRecipients.Name = "txtCCRecipients";
            this.txtCCRecipients.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCCRecipients.Size = new System.Drawing.Size(408, 38);
            this.txtCCRecipients.TabIndex = 11;
            this.txtCCRecipients.Text = resources.GetString("txtCCRecipients.Text");
            this.txtCCRecipients.TextChanged += new System.EventHandler(this.txtCCRecipients_TextChanged);
            // 
            // chkValidateJiraStatus
            // 
            this.chkValidateJiraStatus.AutoSize = true;
            this.chkValidateJiraStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkValidateJiraStatus.Location = new System.Drawing.Point(32, 74);
            this.chkValidateJiraStatus.Name = "chkValidateJiraStatus";
            this.chkValidateJiraStatus.Size = new System.Drawing.Size(151, 18);
            this.chkValidateJiraStatus.TabIndex = 10;
            this.chkValidateJiraStatus.Text = "Validate Jira Status ";
            this.chkValidateJiraStatus.UseVisualStyleBackColor = true;
            this.chkValidateJiraStatus.CheckedChanged += new System.EventHandler(this.chkValidateJiraStatus_CheckedChanged);
            // 
            // btnSetStatusFilePath
            // 
            this.btnSetStatusFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetStatusFilePath.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetStatusFilePath.Location = new System.Drawing.Point(814, 97);
            this.btnSetStatusFilePath.Name = "btnSetStatusFilePath";
            this.btnSetStatusFilePath.Size = new System.Drawing.Size(156, 44);
            this.btnSetStatusFilePath.TabIndex = 9;
            this.btnSetStatusFilePath.Text = "Set Status File Path";
            this.btnSetStatusFilePath.UseVisualStyleBackColor = true;
            this.btnSetStatusFilePath.Click += new System.EventHandler(this.btnSetStatusFilePath_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtMemberName);
            this.groupBox2.Controls.Add(this.btnForward);
            this.groupBox2.Controls.Add(this.btnBackWard);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(978, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(403, 122);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Date Wise Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "Filter By Member";
            // 
            // txtMemberName
            // 
            this.txtMemberName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemberName.Location = new System.Drawing.Point(123, 78);
            this.txtMemberName.Name = "txtMemberName";
            this.txtMemberName.Size = new System.Drawing.Size(224, 21);
            this.txtMemberName.TabIndex = 9;
            // 
            // btnForward
            // 
            this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForward.Location = new System.Drawing.Point(294, 19);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 38);
            this.btnForward.TabIndex = 8;
            this.btnForward.Text = ">>";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnBackWard
            // 
            this.btnBackWard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackWard.Location = new System.Drawing.Point(6, 19);
            this.btnBackWard.Name = "btnBackWard";
            this.btnBackWard.Size = new System.Drawing.Size(75, 38);
            this.btnBackWard.TabIndex = 7;
            this.btnBackWard.Text = "<<";
            this.btnBackWard.UseVisualStyleBackColor = true;
            this.btnBackWard.Click += new System.EventHandler(this.btnBackWard_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(87, 27);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btnCommitExcel
            // 
            this.btnCommitExcel.Enabled = false;
            this.btnCommitExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCommitExcel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommitExcel.Location = new System.Drawing.Point(628, 97);
            this.btnCommitExcel.Name = "btnCommitExcel";
            this.btnCommitExcel.Size = new System.Drawing.Size(173, 44);
            this.btnCommitExcel.TabIndex = 8;
            this.btnCommitExcel.Text = "Commit Excel";
            this.btnCommitExcel.UseVisualStyleBackColor = true;
            this.btnCommitExcel.Click += new System.EventHandler(this.btnCommitExcel_Click);
            // 
            // btnShowExcel
            // 
            this.btnShowExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowExcel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowExcel.Location = new System.Drawing.Point(422, 97);
            this.btnShowExcel.Name = "btnShowExcel";
            this.btnShowExcel.Size = new System.Drawing.Size(173, 44);
            this.btnShowExcel.TabIndex = 5;
            this.btnShowExcel.Text = "Get Lock And Show Excel";
            this.btnShowExcel.UseVisualStyleBackColor = true;
            this.btnShowExcel.Click += new System.EventHandler(this.btnShowExcel_Click);
            // 
            // txtTORecipients
            // 
            this.txtTORecipients.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTORecipients.Location = new System.Drawing.Point(32, 19);
            this.txtTORecipients.Multiline = true;
            this.txtTORecipients.Name = "txtTORecipients";
            this.txtTORecipients.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTORecipients.Size = new System.Drawing.Size(408, 38);
            this.txtTORecipients.TabIndex = 4;
            this.txtTORecipients.Text = "Jean Lozano (jean.lozano@mediavalet.com); Kenneth Li (kenneth.li@mediavalet.com);" +
    " Jason Marshall <jason.marshall@mediavalet.com>";
            this.txtTORecipients.TextChanged += new System.EventHandler(this.txtTORecipients_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnUnlockForeFully
            // 
            this.btnUnlockForeFully.Location = new System.Drawing.Point(396, 489);
            this.btnUnlockForeFully.Name = "btnUnlockForeFully";
            this.btnUnlockForeFully.Size = new System.Drawing.Size(196, 23);
            this.btnUnlockForeFully.TabIndex = 0;
            this.btnUnlockForeFully.Text = "UN LOCK Forcefully";
            this.btnUnlockForeFully.Visible = false;
            this.btnUnlockForeFully.Click += new System.EventHandler(this.btnUnlockForeFully_Click);
            // 
            // btnCleanSetting
            // 
            this.btnCleanSetting.Location = new System.Drawing.Point(733, 488);
            this.btnCleanSetting.Name = "btnCleanSetting";
            this.btnCleanSetting.Size = new System.Drawing.Size(138, 23);
            this.btnCleanSetting.TabIndex = 5;
            this.btnCleanSetting.Text = "Clean Setting";
            this.btnCleanSetting.UseVisualStyleBackColor = true;
            this.btnCleanSetting.Visible = false;
            this.btnCleanSetting.Click += new System.EventHandler(this.btnCleanSetting_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 728);
            this.Controls.Add(this.btnCleanSetting);
            this.Controls.Add(this.btnUnlockForeFully);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.webBrowser1);
            this.Name = "MainForm";
            this.Text = "Status MakerK";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenInOutlook;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnShowTodaysStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTORecipients;
        private System.Windows.Forms.Button btnShowExcel;
        private System.Windows.Forms.Button btnCommitExcel;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBackWard;
        private System.Windows.Forms.TextBox txtMemberName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetStatusFilePath;
        private System.Windows.Forms.CheckBox chkValidateJiraStatus;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnUnlockForeFully;
        private System.Windows.Forms.TextBox txtCCRecipients;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCleanSetting;
    }
}

