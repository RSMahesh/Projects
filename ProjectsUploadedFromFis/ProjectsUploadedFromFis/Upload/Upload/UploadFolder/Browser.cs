using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UploadFolder
{
    public partial class Browser : Form
    {
        public Browser()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void Browser_Load(object sender, EventArgs e)
        {
         string url =   System.AppDomain.CurrentDomain.BaseDirectory + @"\Error.htm";
         webBrowser1.Url = new Uri(url);
             webBrowser1.Url = new Uri(@"C:\Documents and Settings\mahesh.bailwal\My Documents\Projects\Upload\UploadFolder\bin\Debug\Error.htm");
            
        }
    }
}
