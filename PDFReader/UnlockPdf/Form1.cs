using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BitMiracle.Docotic.Pdf;

namespace UnlockPdf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    var input = @"C:\abc.pdf";
        //    bool passwordProtected = PdfDocument.IsPasswordProtected(input);
        //    if (passwordProtected)
        //    {
        //        string password = "MB123"; // retrieve the password somehow

        //        using (PdfDocument doc = new PdfDocument(input, password))
        //        {
        //            // clear both passwords in order
        //            // to produce unprotected document
        //            doc.OwnerPassword = "";
        //            doc.UserPassword = "";

        //            doc.Save(@"C:\me.pdf");
        //        }
        //    }
        //    else
        //    {
        //        // no decryption is required
        //      //  File.Copy(input, output, true);
        //    }

        //}


        private bool Unlock(string file, string password)
        {
            bool passwordProtected = PdfDocument.IsPasswordProtected(file);
            if (passwordProtected)
            {
                try
                {
                    using (PdfDocument doc = new PdfDocument(file, password))
                    {
                        // clear both passwords in order
                        // to produce unprotected document
                        doc.OwnerPassword = "";
                        doc.UserPassword = "";

                      var unlockFileName = Path.GetDirectoryName(file) + "\\" +  Path.GetFileNameWithoutExtension(file) + "_unlock" + ".pdf";

                        doc.Save(unlockFileName);

                        MessageBox.Show("Done");
                    }
          
                }
                catch (BitMiracle.Docotic.Pdf.PdfException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return true;
        }

        private void brnBackColor_Click(object sender, EventArgs e)
        {
            Unlock(txtFile.Text,txtPassword.Text);
         
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            txtFile.Text = openFileDialog1.FileName;
        }
    }
}
