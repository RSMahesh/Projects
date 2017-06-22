using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiddenAppStarter
{
    static class Program
    {
        static Dictionary<string, string> commadLineArgs;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            const string AppToStartArgName = "AppToStart";
            commadLineArgs = ParseCommandLineArguments(args);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (!commadLineArgs.ContainsKey(AppToStartArgName) || string.IsNullOrEmpty(commadLineArgs[AppToStartArgName]))
            {
                MessageBox.Show(AppToStartArgName + " is name is needed in command line parameter");
                return;
            }

            Application.Run(new HiddenForm(commadLineArgs[AppToStartArgName]));
        }

        private static Dictionary<string, string> ParseCommandLineArguments(string[] args)
        {
            var dic = new Dictionary<string, string>();

            foreach (var arg in args)
            {
                var arg1 = arg.Split('=');
                if (arg1.Length > 1)
                {
                    dic[arg1[0]] = arg1[1];
                }
            }

            return dic;
        }

    }
}
