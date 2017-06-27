using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class UnhandledExceptionHandlercs
    {
        private string _errorLogFile = string.Empty;
        Action<Exception> _logException = null;
        public void Register(Action<Exception> logException)
        {
            _logException = logException;
            //   AppDomain currentDomain = AppDomain.CurrentDomain;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Handler);
            
        }

        private void Handler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            _logException(e);

            
        }
    }
}
