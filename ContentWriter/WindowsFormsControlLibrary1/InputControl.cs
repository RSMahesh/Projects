using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary1
{
    public partial class InputControl : UserControl
    {
        public string InputName;
        public int InputIndex;
        public static List<string> Words;

        public string Text
        {
            get
            { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public InputControl(string inputName, int inputIndex, int height)
        {
            InitializeComponent();
            label1.MaximumSize = new Size(100, 0);
            label1.AutoSize = true;
            this.AutoSize = true;
            InputName = inputName;
            InputIndex = inputIndex;
            textBox1.Height = height;
            textBox1.Tag = inputIndex;
           // var source = new AutoCompleteStringCollection();
           // source.AddRange(Words.ToArray());
           // textBox1.AutoCompleteCustomSource  = source;
           // textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           // textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

           //// textBox2.Multiline = true;
           // //textBox2.Height = 40;
           // textBox2.AutoCompleteCustomSource = source;
           // textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           // textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        public bool ReadOnly
        {
            set
            {
                
                if(value == true)
                {
                   // textBox1.Height = 200;
                    textBox1.ReadOnly = value;
                }
                else
                {
                  //  textBox1.Height = 100;
                    textBox1.ReadOnly = value;
                }
            }
        }

        private void InputControl_Load(object sender, EventArgs e)
        {
            label1.Text = InputName;
            textBox1.Name = InputName;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
