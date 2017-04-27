using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.ToUpperInvariant() == "START")
            {
                timer1.Interval = int.Parse(textBox3.Text) * 1000;
                timer1.Start();
                button1.Text = "Stop";
            }
            else
            {
                button1.Text = "Start";
                timer1.Stop();
            }
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ServerDLL.MouseSimulater.MoveTo(new Point(int.Parse(textBox1.Text), int.Parse(textBox2.Text)));
            ServerDLL.MouseSimulater.Click_Left(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
   
        }
    
            
    
    
    
    }
}
