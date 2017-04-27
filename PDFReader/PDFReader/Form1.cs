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

namespace PDFReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = @"C:\abc.pdf";
            bool passwordProtected = PdfDocument.IsPasswordProtected(input);
            if (passwordProtected)
            {
                string password = "MB123"; // retrieve the password somehow

                using (PdfDocument doc = new PdfDocument(input, password))
                {
                    // clear both passwords in order
                    // to produce unprotected document
                    doc.OwnerPassword = "";
                    doc.UserPassword = "";

                    doc.Save(@"C:\me.pdf");
                }
            }
            else
            {
                // no decryption is required
              //  File.Copy(input, output, true);
            }

        }


        private bool CheckPassword(string input, string password)
        {
            bool passwordProtected = PdfDocument.IsPasswordProtected(input);
            if (passwordProtected)
            {

                try
                {
                    using (PdfDocument doc = new PdfDocument(input, password))
                    {
                        // clear both passwords in order
                        // to produce unprotected document
               //         doc.OwnerPassword = "";
                //        doc.UserPassword = "";

                  //      doc.Save(@"C:\me.pdf");
                    }
          
                }
                catch (BitMiracle.Docotic.Pdf.PdfException)
                {

                    return false;
                }
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void brnBackColor_Click(object sender, EventArgs e)
        {
            var input = @"C:\abc.pdf";

            PdfDocument pdf = new PdfDocument(txtFile.Text);
            PdfCanvas canvas = pdf.Pages[4].Canvas;

            canvas.Brush.Color = new PdfRgbColor(255, 0, 0);
            canvas.Pen.Color = new PdfRgbColor(0, 255, 255);
            canvas.Pen.Width = 3;

            canvas.DrawRectangle(new RectangleF(10, 50, 500, 500), PdfDrawMode.FillAndStroke);

            pdf.Save(@"D:\Study\123.pdf");
         
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.OpenFile();
            txtFile.Text = openFileDialog1.FileName;
        }
    }
}
