namespace JarFileGeneratorTool
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnGeneratePackage = new System.Windows.Forms.Button();
            chkSelectAll = new System.Windows.Forms.CheckBox();
            txtFilter = new System.Windows.Forms.TextBox();
            btnBrowse = new System.Windows.Forms.Button();
            txtSourceFolder = new System.Windows.Forms.TextBox();
            btnGenerate = new System.Windows.Forms.Button();
            ListBoxFiles = new System.Windows.Forms.CheckedListBox();
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnGeneratePackage);
            groupBox1.Controls.Add(chkSelectAll);
            groupBox1.Controls.Add(txtFilter);
            groupBox1.Controls.Add(btnBrowse);
            groupBox1.Controls.Add(txtSourceFolder);
            groupBox1.Controls.Add(btnGenerate);
            groupBox1.Controls.Add(ListBoxFiles);
            groupBox1.Location = new System.Drawing.Point(5, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(553, 474);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // btnGeneratePackage
            // 
            btnGeneratePackage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnGeneratePackage.Location = new System.Drawing.Point(17, 417);
            btnGeneratePackage.Name = "btnGeneratePackage";
            btnGeneratePackage.Size = new System.Drawing.Size(141, 29);
            btnGeneratePackage.TabIndex = 8;
            btnGeneratePackage.Text = "Generate Package";
            btnGeneratePackage.UseVisualStyleBackColor = true;
            btnGeneratePackage.Click += new System.EventHandler(btnGeneratePackage_Click);
            // 
            // chkSelectAll
            // 
            chkSelectAll.AutoSize = true;
            chkSelectAll.Location = new System.Drawing.Point(181, 60);
            chkSelectAll.Name = "chkSelectAll";
            chkSelectAll.Size = new System.Drawing.Size(70, 17);
            chkSelectAll.TabIndex = 7;
            chkSelectAll.Text = "Select All";
            chkSelectAll.UseVisualStyleBackColor = true;
            chkSelectAll.CheckedChanged += new System.EventHandler(chkSelectAll_CheckedChanged);
            // 
            // txtFilter
            // 
            txtFilter.Location = new System.Drawing.Point(17, 48);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new System.Drawing.Size(100, 20);
            txtFilter.TabIndex = 6;
            txtFilter.Text = ".dll,.exe";
            // 
            // btnBrowse
            // 
            btnBrowse.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnBrowse.Location = new System.Drawing.Point(405, 19);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(73, 23);
            btnBrowse.TabIndex = 5;
            btnBrowse.Text = "...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += new System.EventHandler(btnBrowse_Click);
            // 
            // txtSourceFolder
            // 
            txtSourceFolder.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtSourceFolder.Location = new System.Drawing.Point(17, 19);
            txtSourceFolder.Name = "txtSourceFolder";
            txtSourceFolder.Size = new System.Drawing.Size(389, 22);
            txtSourceFolder.TabIndex = 4;
            txtSourceFolder.KeyUp += new System.Windows.Forms.KeyEventHandler(txtSourceFolder_KeyUp);
            // 
            // btnGenerate
            // 
            btnGenerate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnGenerate.Location = new System.Drawing.Point(192, 417);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new System.Drawing.Size(141, 29);
            btnGenerate.TabIndex = 3;
            btnGenerate.Text = "Generate Jar File";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += new System.EventHandler(btnGenerate_Click);
            // 
            // ListBoxFiles
            // 
            ListBoxFiles.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ListBoxFiles.FormattingEnabled = true;
            ListBoxFiles.Location = new System.Drawing.Point(17, 83);
            ListBoxFiles.Name = "ListBoxFiles";
            ListBoxFiles.Size = new System.Drawing.Size(461, 327);
            ListBoxFiles.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(561, 480);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox ListBoxFiles;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSourceFolder;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnGeneratePackage;
    }
}

