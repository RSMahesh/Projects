using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic dte = System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.14.0");
            var ff = dte.Solution.FullName;
        }
    }
}
