using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test();
            int d = 9;
        }

        private void test ()
        {
            for (int i = 0; i < 10; i++)
            {
                int a = 0;
                Thread.Sleep(500);

            }


        }
    }
}