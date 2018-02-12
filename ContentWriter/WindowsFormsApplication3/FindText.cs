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
    partial class FindText : Form
    {
        AppContext appContext;
        public FindText(AppContext appContext)
        {
            this.appContext = appContext;
            InitializeComponent();
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
           (EventPublisher.Events.FindText.ToString(), new EventArg(Guid.NewGuid(), new[] { textBox1.Text, cmbFields.Text }));

        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            EventContainer.PublishEvent
            (EventPublisher.Events.Relace.ToString(), new EventArg(Guid.NewGuid(), new[] { textBox1.Text, txtReplaceText.Text, cmbFields.Text }));
        }

        private void FindText_Load(object sender, EventArgs e)
        {
            cmbFields.Items.Add("ALL");

            foreach (DataGridViewColumn col in appContext.dataGridView.Columns)
            {
                if (col.Visible)
                {
                    cmbFields.Items.Add(col.Name);
                }
            }

            cmbFields.SelectedIndex = 0;
        }
    }

    class FindParameter
    {
        public FindParameter(string columnName, string findText)
        {
            ColumnName = columnName;
            FindText = findText;
        }
        public string ColumnName { get; private set; }
        public string FindText { get; private set; }

        public bool SearchAll
        {
            get
            {
                return ColumnName == "ALL";
            }
        }
    }
}
