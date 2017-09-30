using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utility
{
   public class TextFileReader
    {
        public static bool Find(string path, string  findText)
        {
           return  File.ReadAllText(path).
                IndexOf(findText, StringComparison.InvariantCultureIgnoreCase)!=-1 ;
        }
    }
}
