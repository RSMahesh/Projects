using System.Configuration;
using System.Diagnostics;

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
