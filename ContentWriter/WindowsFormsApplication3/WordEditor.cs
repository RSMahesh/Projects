using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication3;
using Microsoft.Office.Interop;
using Utility;

namespace WindowsFormsApplication3
{
    public partial class WordEditor : Form
    {
        public WordEditor()
        {
            InitializeComponent();
        }

        private void WordEditor_Load(object sender, EventArgs e)
        {
            OutProcessDocument.OpenDocument(@"D:\Study\1.docx");
            OutProcessDocument.ReSize(200, 200, 500, 500);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OutProcessDocument.Visible = !OutProcessDocument.Visible;
        }
    }
}
