using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransparentWindow
{
    public partial class TopForm1 : Form
    {
        public TopForm1()
        {
            InitializeComponent();
        }

        public Action OnDubleClick;

        private void closeBtn_Click(object sender, EventArgs e)
        {
            TransparentFrm.MinimizeAll();
            Application.Exit();
        }

        //private void TopForm1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    transparentFrm.TransparentFrm_KeyDown(sender, e);
        //}

        private void TopForm1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        public TransparentFrm transparentFrm;

        private void TopForm1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void TopForm1_Activated(object sender, EventArgs e)
        {

        }

        private void TopForm1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        
        private void TopForm1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                OnDubleClick();
        }
    }
}
