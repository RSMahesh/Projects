using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class PictureViewer : Form
    {
        public PictureViewer(string imagePath)
        {
            InitializeComponent();
            pictureBox1.ImageLocation = imagePath;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void SetImage(string imagePath)
        {
            pictureBox1.ImageLocation = imagePath;
        }

        private void PictureViewer_Load(object sender, EventArgs e)
        {
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            var imageSize = pictureBox1.Image.Size;
            var fitSize = pictureBox1.ClientSize;
            pictureBox1.SizeMode = imageSize.Width > fitSize.Width || imageSize.Height > fitSize.Height ?
                PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                Process.Start(pictureBox1.ImageLocation);
                this.Close();
            }
        }
    }
}
