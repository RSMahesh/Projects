using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeProject
{
    public partial class AllQuestion : Form
    {
        public AllQuestion()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = listView1.SelectedItems;
            System.Diagnostics.Process.Start("firefox.exe", item[0].SubItems[3].Text);
            var questInfo = (QuestionInfo)Form1.questionHash[item[0].SubItems[3].Text];
            questInfo.readed = true;
        }

        private void AllQuestion_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void AllQuestion_Resize(object sender, EventArgs e)
        {
            listView1.Height = this.Size.Height - 50;
            listView1.Width = this.Size.Width - 40;
        }

        // ColumnClick event handler. 
        private void ColumnClick(object o, ColumnClickEventArgs e)
        {
            // Set the ListViewItemSorter property to a new ListViewItemComparer  
            // object. Setting this property immediately sorts the  
            // ListView using the ListViewItemComparer object. 
            this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }


        private void ShowAllQusetion()
        {
            foreach (var key in Form1.questionHash.Keys)
            {
                var val = (QuestionInfo)Form1.questionHash[key];
                ListViewItem item = new ListViewItem();
                string[] arr = new string[6];
                arr[0] = listView1.Items.Count.ToString();
                arr[1] = val.technologie;
                arr[2] = val.text;
                arr[3] = val.link;
                arr[4] = val.timeStamp.ToString();
                arr[5] = val.readed.ToString();
                listView1.Items.Add(new ListViewItem(arr));
            }
        }

        private void AllQuestion_Load(object sender, EventArgs e)
        {
            ShowAllQusetion();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ColumnClick(sender, e);
        }

    }
}
