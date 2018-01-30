using EventPublisher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class FindText : Form
    {
        public FindText()
        {
            InitializeComponent();
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
           (EventPublisher.Events.FindText.ToString(), new EventArg(Guid.NewGuid(), textBox1.Text));

        }

        private void btnReplace_Click(object sender, EventArgs e)
        {


            EventContainer.PublishEvent
            (EventPublisher.Events.Relace.ToString(), new EventArg(Guid.NewGuid(), new[] { textBox1.Text, txtReplaceText.Text }));

        }

      
    }
}
