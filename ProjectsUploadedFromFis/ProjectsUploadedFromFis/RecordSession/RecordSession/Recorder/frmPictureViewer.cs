using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace RecordSession
{
    public partial class frmPictureViewer : Form
    {
        public List<FileInfo> files = null;
        public int index = 0;
        public Action<int> DisplayChange;
        public int incrementCount =1;

        public frmPictureViewer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }


        public int Play(string folder)
        {
            if (timer2.Enabled)
                timer2.Enabled = false;
            else
                files = new DirectoryInfo(folder).GetFiles()
                                                                .OrderBy(f => f.LastWriteTime)
                                                                .ToList();


            timer2.Enabled = true;
            return files.Count-1;
            //  trackBar1.Maximum = files.Count;
            //   trackBar1.Minimum = 0;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (files.Count <= index || index < 0)
            {
                timer2.Enabled = false;
                this.Close();
                return;
            }
            
            
            DisplayChange(index);
            // trackBar1.Value = index;
            pictureBox1.ImageLocation = files[index].FullName;
            index += incrementCount;

        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            timer2.Enabled = !timer2.Enabled;
        }


       



    }
}
