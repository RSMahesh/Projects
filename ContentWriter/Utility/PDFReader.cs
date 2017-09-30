using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utility
{
   public static  class PDFReader
    {
       public static bool Find(string path, string findText)
       {
           PDFParser pdfParser = new PDFParser();
           string text = "";
           using (MemoryStream ms = new MemoryStream())
           {
               text = pdfParser.ExtractText(path, ms);
           }
           return text.IndexOf(findText, 0, StringComparison.InvariantCultureIgnoreCase) != -1;
       }

    }
}
