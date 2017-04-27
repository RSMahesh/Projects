using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileRenamer = new FileRenamer();
            fileRenamer.RenameFiles(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
        }
    }
}
