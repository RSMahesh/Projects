using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recorder
{
    public partial class Settings : Form
    {
        public static int FastSpeed = 10;
       
        public Settings()
        {
            InitializeComponent();
            lblVal.Text = FastSpeed.ToString();
            trackBarFastSpeed.Value = FastSpeed;
        }

        private void trackBarFastSpeed_Scroll(object sender, EventArgs e)
        {
            FastSpeed = trackBarFastSpeed.Value;
            lblVal.Text = FastSpeed.ToString();
        }
    }
}
