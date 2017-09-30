using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsControlLibrary1
{
    public partial class SmartText : UserControl
    {
        [DllImport("user32.dll")]
        static extern bool GetCaretPos(out Point lpPoint);
        public SmartText()
        {
            InitializeComponent();
        }

        private void SmartText_Load(object sender, EventArgs e)
        {
             var source = new AutoCompleteStringCollection();
             source.AddRange( new []{"abc","asd","azx" });
            textBox2.AutoCompleteCustomSource = source;
            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
      
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
           // Point p = new Point();

           // GetCaretPos(out p);

            //MessageBox.Show(p.X.ToString() + ":" + p.Y.ToString());
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //textBox2.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            textBox2.Focus();
            textBox2.Text += e.KeyChar;
           // textBox2.Focus();
          // textBox2.
            textBox2.SelectionStart = textBox2.Text.Length; // add some logic if length is 0
textBox2.SelectionLength = 0;

        }
    }
}
