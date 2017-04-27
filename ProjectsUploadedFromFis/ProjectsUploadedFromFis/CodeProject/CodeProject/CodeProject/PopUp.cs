using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace CodeProject
{
    public partial class PopUp : Form
    {

        int index;
       public static int formTop=-1;
       public static Size formSize = new System.Drawing.Size(-1, -1);
       public static Size minSize = new System.Drawing.Size(15,15);
       //var newSize = new Size(10, 10);
       
        internal List<QuestionInfo> newQuestions;
        public  System.Windows.Forms.Timer timer1;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
        );
        public PopUp()
        {
            
            InitializeComponent();
            int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            if (formTop  != -1)
            {
                y = formTop;
            }

            this.Location = new Point(x - 11, y - 1);

            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Vie All Question", new EventHandler(ViewAllQuestion));
           // cm.MenuItems.Add("Item 2", new EventHandler(btnClose_Click_1));
            this.panel1.ContextMenu = cm; 
        }

           private void ViewAllQuestion(object sender, EventArgs e)
           {
               AllQuestion frm = new AllQuestion();
               frm.Show();
           }
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.Hide();
        }

        private void PopUp_Load(object sender, EventArgs e)
        {

            //GraphicsPath p = new GraphicsPath();
            //p.AddArc(new Rectangle(10, 10, 200, 200), 180, 360);
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 400, 300, 15, 15));
            //p.AddEllipse(0, 0, this.Width, this.Height);
            ////this.Region = new Region(p);
            timer1.Enabled = false;
            Showquestion();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            //Process.Start(label1.Tag.ToString());
           // System.Diagnostics.Process proc = new System.Diagnostics.Process();

            //proc.StartInfo.FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe";

            //proc.StartInfo.Arguments = label1.Tag.ToString();
           
            System.Diagnostics.Process.Start("firefox.exe", label1.Tag.ToString());
            var questInfo= (QuestionInfo) Form1.questionHash[label1.Tag.ToString()];
            questInfo.readed = true;

            //proc.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBackWard_Click(object sender, EventArgs e)
        {
            index--;
            Showquestion();

        }
        private void Showquestion()
        {
            label1.Text = newQuestions[index].text;
            label1.Tag = newQuestions[index].link;
            label3.Text = newQuestions[index].technologie;
            label2.Text = (index + 1).ToString() + " of " + newQuestions.Count.ToString(); 
            if (index +1 >= newQuestions.Count)
            {
                btnForward.Enabled = false;
            }
            else
            {
                btnForward.Enabled = true;
            }

            if (index <= 0)
            {
                btnBackWard.Enabled = false;
            }
            else
            {
                btnBackWard.Enabled = true;
            }

        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            index++;
            Showquestion();
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (formSize.Height != -1)
            {
                this.Size = formSize;
                formSize.Height = -1;
                this.label1.Visible = true;
            }
        }

        private void lblHide_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            return;
            if (formSize.Height == -1)
            {
                formSize = this.Size;
                this.Size = minSize;
                this.label1.Visible = false;
            }
            
        }
    }
}
