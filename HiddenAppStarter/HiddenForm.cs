using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiddenAppStarter
{
    public class HiddenForm : Form
    {
        string _appToRunName;
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
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(287, 155);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "Form1";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Windows svs host";
            WindowState = System.Windows.Forms.FormWindowState.Minimized;
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form1_FormClosing);
            Load += new System.EventHandler(Form1_Load);
            ResumeLayout(false);

        }

        public HiddenForm(string appToRunName)
        {
            _appToRunName = appToRunName;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
            if (_appToRunName.IndexOf("\\") == -1)
            {
                _appToRunName = RuntimeHelper.MapToCurrentExecutionLocation(_appToRunName);
            }
      
            ProcessHelper.RunHidden(_appToRunName);

            Thread.Sleep(100);

            Process.GetCurrentProcess().Kill();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion
    }
}
