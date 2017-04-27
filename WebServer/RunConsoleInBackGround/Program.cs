using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunConsoleInBackGround
{
    class Program
    {
        static void Main(string[] args)
        {
            var start =new ProcessStartInfo();
            
            start.FileName = ConfigurationManager.AppSettings["WebServerExePath"];
            
            start.WindowStyle = ProcessWindowStyle.Hidden;

            Process.Start(start);
        }
    }
}
