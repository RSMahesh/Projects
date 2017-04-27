namespace FTPControl
{
    partial class FTP
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalSiurceFile = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.lstSourceFile = new System.Windows.Forms.CheckedListBox();
            this.gbRemote = new System.Windows.Forms.GroupBox();
            this.lblTotalTargetFile = new System.Windows.Forms.Label();
            this.lstCopyedFiles = new System.Windows.Forms.ListBox();
            this.gbProgess = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtOutPut = new System.Windows.Forms.TextBox();
            this.btnStartFtp = new System.Windows.Forms.Button();
            this.gbSource.SuspendLayout();
            this.gbRemote.SuspendLayout();
            this.gbProgess.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSource
            // 
            this.gbSource.Controls.Add(this.label1);
            this.gbSource.Controls.Add(this.lblTotalSiurceFile);
            this.gbSource.Controls.Add(this.txtFilter);
            this.gbSource.Controls.Add(this.chkAll);
            this.gbSource.Controls.Add(this.lstSourceFile);
            this.gbSource.Location = new System.Drawing.Point(4, 3);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(494, 355);
            this.gbSource.TabIndex = 15;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "groupBox1";
            
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(177, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 14);
            this.label1.TabIndex = 20;
            this.label1.Text = "Filter";
            // 
            // lblTotalSiurceFile
            // 
            this.lblTotalSiurceFile.AutoSize = true;
            this.lblTotalSiurceFile.Location = new System.Drawing.Point(15, 330);
            this.lblTotalSiurceFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalSiurceFile.Name = "lblTotalSiurceFile";
            this.lblTotalSiurceFile.Size = new System.Drawing.Size(75, 14);
            this.lblTotalSiurceFile.TabIndex = 19;
            this.lblTotalSiurceFile.Text = "File Count:";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(219, 24);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(247, 22);
            this.txtFilter.TabIndex = 18;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged_1);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(18, 26);
            this.chkAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(82, 18);
            this.chkAll.TabIndex = 17;
            this.chkAll.Text = "Select All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged_1);
            // 
            // lstSourceFile
            // 
            this.lstSourceFile.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSourceFile.FormattingEnabled = true;
            this.lstSourceFile.Location = new System.Drawing.Point(14, 51);
            this.lstSourceFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstSourceFile.Name = "lstSourceFile";
            this.lstSourceFile.Size = new System.Drawing.Size(452, 276);
            this.lstSourceFile.TabIndex = 15;
            this.lstSourceFile.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstSourceFile_ItemCheck_1);
            // 
            // gbRemote
            // 
            this.gbRemote.Controls.Add(this.lblTotalTargetFile);
            this.gbRemote.Controls.Add(this.lstCopyedFiles);
            this.gbRemote.Location = new System.Drawing.Point(502, 3);
            this.gbRemote.Name = "gbRemote";
            this.gbRemote.Size = new System.Drawing.Size(494, 355);
            this.gbRemote.TabIndex = 16;
            this.gbRemote.TabStop = false;
            this.gbRemote.Text = "groupBox2";
            // 
            // lblTotalTargetFile
            // 
            this.lblTotalTargetFile.AutoSize = true;
            this.lblTotalTargetFile.Location = new System.Drawing.Point(14, 330);
            this.lblTotalTargetFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalTargetFile.Name = "lblTotalTargetFile";
            this.lblTotalTargetFile.Size = new System.Drawing.Size(75, 14);
            this.lblTotalTargetFile.TabIndex = 20;
            this.lblTotalTargetFile.Text = "File Count:";
            // 
            // lstCopyedFiles
            // 
            this.lstCopyedFiles.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCopyedFiles.FormattingEnabled = true;
            this.lstCopyedFiles.ItemHeight = 14;
            this.lstCopyedFiles.Location = new System.Drawing.Point(17, 51);
            this.lstCopyedFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstCopyedFiles.Name = "lstCopyedFiles";
            this.lstCopyedFiles.Size = new System.Drawing.Size(452, 270);
            this.lstCopyedFiles.TabIndex = 6;
            // 
            // gbProgess
            // 
            this.gbProgess.Controls.Add(this.progressBar1);
            this.gbProgess.Controls.Add(this.txtOutPut);
            this.gbProgess.Location = new System.Drawing.Point(4, 419);
            this.gbProgess.Name = "gbProgess";
            this.gbProgess.Size = new System.Drawing.Size(992, 144);
            this.gbProgess.TabIndex = 17;
            this.gbProgess.TabStop = false;
            this.gbProgess.Text = "Transfer Progress";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 21);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(960, 34);
            this.progressBar1.TabIndex = 12;
            // 
            // txtOutPut
            // 
            this.txtOutPut.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutPut.Location = new System.Drawing.Point(12, 61);
            this.txtOutPut.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOutPut.Multiline = true;
            this.txtOutPut.Name = "txtOutPut";
            this.txtOutPut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutPut.Size = new System.Drawing.Size(963, 73);
            this.txtOutPut.TabIndex = 10;
            // 
            // btnStartFtp
            // 
            this.btnStartFtp.Location = new System.Drawing.Point(18, 364);
            this.btnStartFtp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStartFtp.Name = "btnStartFtp";
            this.btnStartFtp.Size = new System.Drawing.Size(953, 42);
            this.btnStartFtp.TabIndex = 8;
            this.btnStartFtp.Text = "Start File Transfer";
            this.btnStartFtp.UseVisualStyleBackColor = true;
            this.btnStartFtp.Click += new System.EventHandler(this.btnStartFtp_Click);
            // 
            // FTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbProgess);
            this.Controls.Add(this.gbRemote);
            this.Controls.Add(this.gbSource);
            this.Controls.Add(this.btnStartFtp);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FTP";
            this.Size = new System.Drawing.Size(1005, 581);
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            this.gbRemote.ResumeLayout(false);
            this.gbRemote.PerformLayout();
            this.gbProgess.ResumeLayout(false);
            this.gbProgess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalSiurceFile;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckedListBox lstSourceFile;
        private System.Windows.Forms.GroupBox gbRemote;
        private System.Windows.Forms.Label lblTotalTargetFile;
        private System.Windows.Forms.ListBox lstCopyedFiles;
        private System.Windows.Forms.GroupBox gbProgess;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtOutPut;
        private System.Windows.Forms.Button btnStartFtp;
    }
}
