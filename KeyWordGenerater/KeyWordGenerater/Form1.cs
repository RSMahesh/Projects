using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyWordGenerater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string abc = "";
            for (var i = int.Parse(textBox1.Text); i <= int.Parse(textBox2.Text); i++)
            {
                abc += txtPreFix.Text + i.ToString() + ",";
            }

            textBox3.Text = abc;
        }
    }
}
