using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using Microsoft.Win32;

namespace HaveABreak
{
    public partial class Form1 : Form
    {
        [DllImport("user32")]
        public static extern void LockWorkStation();

        private int breakkIntervalInSeconds = 60 * 20;

        private int reminderIntervalInSeconds = 60*2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Hide();
            this.timer1.Interval = 1000 * breakkIntervalInSeconds;
            timer1.Enabled = true;
            LockWorkStation();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Visible = true;
            timer1.Enabled = false;
        }

        private void btnNotNow_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 1000 * reminderIntervalInSeconds;
            this.Visible = false;
            timer1.Enabled = true;
        }

     
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Hide();
            timer2.Enabled = false;
        }

        

    void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
    {
        if (e.Reason == SessionSwitchReason.SessionLock)
        {
            this.timer1.Enabled = false;
        }
        else if (e.Reason == SessionSwitchReason.SessionUnlock)
        {
            this.timer1.Enabled = true;
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

        try
        {
            var ss = (int)60 * double.Parse(textBox1.Text);
            breakkIntervalInSeconds = Convert.ToInt32(ss);

            MessageBox.Show(
                "BreakkIntervalInSeconds :" + breakkIntervalInSeconds.ToString() + Environment.NewLine
                + "reminderIntervalInSeconds" + reminderIntervalInSeconds.ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());

        }
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var ss = (int)60 * double.Parse(textBox2.Text);
            reminderIntervalInSeconds = Convert.ToInt32(ss);
            MessageBox.Show(
        "BreakkIntervalInSeconds :" + breakkIntervalInSeconds.ToString() + Environment.NewLine
        + "reminderIntervalInSeconds" + reminderIntervalInSeconds.ToString());
    
    
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
           
        }
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
         Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);
           // this.Opacity = 0.5;
            this.timer2.Interval = 1000 * 1;
            timer2.Enabled = true;
            this.timer1.Interval = 1000 * breakkIntervalInSeconds;
            this.timer1.Enabled = true;
           
            this.Hide();
    }

    }
}
