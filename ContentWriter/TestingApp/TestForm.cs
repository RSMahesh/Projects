using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication3;

namespace TestingApp
{
    public partial class TestForm : Form
    {
        MainFormTest mainTest;
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Form1(@"D:\temp\Shally150.xlsx");
            frm.Show();

            mainTest = new MainFormTest(frm);
            mainTest.FindReplaceTest();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainTest.CheckWindow();
        }
    }
}
