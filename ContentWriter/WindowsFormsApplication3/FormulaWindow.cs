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
        public static string Formula123;
        DataGridView _gridView;
        Regex regex = new Regex("{.*?}");

        public FormulaWindow(DataGridView gridView)
        {
            InitializeComponent();
            _gridView = gridView;
            textBox1.Text = Formula123;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            Formula123 = textBox1.Text;
        }

        private void FormulaWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Formula123 = textBox1.Text;

            if (!Validate())
            {
                e.Cancel = true;
                return;
            }

        }

        bool Validate()
        {
            if (string.IsNullOrEmpty(Formula123))
            {
                return true;
            }

            var matches = regex.Matches(FormulaWindow.Formula123);

            foreach (Match match in matches)
            {
                var columnName = match.Value.Replace("{", "").Replace("}", "");

                if (!DoesColumnExist(columnName))
                {
                    MessageBox.Show("Column :" + columnName + " does not exist."+ 
                       Environment.NewLine +
                       "Please make sure column name does not conatin space at start and end");
                    return false;
                }

            }

            return true;
        }

        private bool DoesColumnExist(string colName)
        {
            foreach(DataGridViewColumn col in _gridView.Columns)
            {
              if( col.Name.Equals(colName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
