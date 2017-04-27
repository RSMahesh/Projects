using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    public class UnhandledExceptionHandlercs
    {
        private string _errorLogFile = string.Empty;
        public void Register(string logFolder)
        {
            _errorLogFile = Path.Combine(logFolder, "ActivityErrorLogs.log"); ;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(Handler);
        }

        private void Handler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            File.AppendAllText(_errorLogFile, Environment.NewLine + DateTime.Now.ToString() + ":" + e.ToString());
        }
    }
}
