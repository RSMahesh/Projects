using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ServerWindowsInterface
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();

            ServerDLL.Server server = new ServerDLL.Server();
            server.start();
        }
    }
}