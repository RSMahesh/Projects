using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UploadFolder
{
    public partial class CutsomMessage : Form
    {
        public string result = "";
        public string msg;

        public CutsomMessage()
        {
            InitializeComponent();
        }

        private void CutsomMessage_Load(object sender, EventArgs e)
        {
            label1.Text = msg;
        }

        private void btnYesToall_Click(object sender, EventArgs e)
        {
            result = ((Button)sender).Text;
            CloseWindow();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            result = ((Button)sender).Text;
            CloseWindow();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            result = ((Button)sender).Text; 
            CloseWindow();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = ((Button)sender).Text; 
            CloseWindow();
        }

        private void CloseWindow()
        {
            this.Hide();
        }
    }
}
