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
        public int FastSpeed { get; set; }
        public Settings()
        {
            InitializeComponent();
        }

        private void trackBarFastSpeed_Scroll(object sender, EventArgs e)
        {

        }
    }
}
