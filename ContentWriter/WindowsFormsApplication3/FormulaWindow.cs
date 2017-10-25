using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class FormulaWindow : Form
    {
        public static string Formula;
        DataGridView _gridView;
        Regex regex = new Regex("{.*?}");

        public FormulaWindow(DataGridView gridView)
        {
            InitializeComponent();
            _gridView = gridView;
            textBox1.Text = Formula;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            Formula = textBox1.Text;
        }

        private void FormulaWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Formula = textBox1.Text;

            if (!Validate())
            {
                e.Cancel = true;
                return;
            }

        }

        bool Validate()
        {
            if (string.IsNullOrEmpty(Formula))
            {
                return true;
            }

            var matches = regex.Matches(FormulaWindow.Formula);

            foreach (Match match in matches)
            {
                var columnName = match.Value.Replace("{", "").Replace("}", "");

                if (!_gridView.Columns.Contains(columnName))
                {
                    MessageBox.Show("Column :" + columnName + " does not exist");
                    return false;
                }

            }

            return true;
        }
    }
}
