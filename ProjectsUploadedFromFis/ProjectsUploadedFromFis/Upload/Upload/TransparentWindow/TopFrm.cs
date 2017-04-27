using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace TransparentWindow
{
    public partial class TopFrm : Form
    {
        private Form _transparentForm;
        public TopFrm()
        {
        }

        private void TopFrm_Load(object sender, EventArgs e)
        {

        }

        private void TopFrm_Resize(object sender, EventArgs e)
        {

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
         TransparentFrm.MinimizeAll();
        }
    }
}
