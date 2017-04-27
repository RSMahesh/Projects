namespace UploadFolder
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.btnGetRooTFiles = new System.Windows.Forms.Button();
            this.btnDeleteFile = new System.Windows.Forms.Button();
            this.txtExt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.Location = new System.Drawing.Point(12, 56);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(701, 480);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // txtServerUrl
            // 
            this.txtServerUrl.Location = new System.Drawing.Point(12, 1);
            this.txtServerUrl.Name = "txtServerUrl";
            this.txtServerUrl.Size = new System.Drawing.Size(272, 20);
            this.txtServerUrl.TabIndex = 1;
            this.txtServerUrl.Text = "http://infowebservices.in/UploadTest/FTP.aspx";
            // 
            // btnGetRooTFiles
            // 
            this.btnGetRooTFiles.Location = new System.Drawing.Point(12, 27);
            this.btnGetRooTFiles.Name = "btnGetRooTFiles";
            this.btnGetRooTFiles.Size = new System.Drawing.Size(148, 23);
            this.btnGetRooTFiles.TabIndex = 2;
            this.btnGetRooTFiles.Text = "GetRootFiles";
            this.btnGetRooTFiles.UseVisualStyleBackColor = true;
            this.btnGetRooTFiles.Click += new System.EventHandler(this.btnGetRooTFiles_Click);
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Location = new System.Drawing.Point(12, 549);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(148, 23);
            this.btnDeleteFile.TabIndex = 3;
            this.btnDeleteFile.Text = "Delete File";
            this.btnDeleteFile.UseVisualStyleBackColor = true;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // txtExt
            // 
            this.txtExt.Location = new System.Drawing.Point(414, 27);
            this.txtExt.Name = "txtExt";
            this.txtExt.Size = new System.Drawing.Size(100, 20);
            this.txtExt.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(348, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ext Filter";
            // 
            // FTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 584);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtExt);
            this.Controls.Add(this.btnDeleteFile);
            this.Controls.Add(this.btnGetRooTFiles);
            this.Controls.Add(this.txtServerUrl);
            this.Controls.Add(this.treeView1);
            this.Name = "FTP";
            this.Text = "FTP";
            this.Load += new System.EventHandler(this.FTP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox txtServerUrl;
        private System.Windows.Forms.Button btnGetRooTFiles;
        private System.Windows.Forms.Button btnDeleteFile;
        private System.Windows.Forms.TextBox txtExt;
        private System.Windows.Forms.Label label1;
    }
}