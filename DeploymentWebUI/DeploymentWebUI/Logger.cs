using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web;

namespace DeploymentWebUI
{
    /// <summary>
    /// This partail class is wraper on MediaValet.ServiceLibrary.Logging.Log for exposing it through ILogger interface
    /// </summary>
    public partial class Logger : ILogger
    {
        #region PrivateMembers
        private MethodBase _callingMethodBase;
        private const int _callingMethodStackFrame = 1;
        #endregion

        #region Public Methods

        /// <summary>
        /// Logs Exception
        /// </summary>
        /// <param name="exception"></param>
        public void LogException(Exception exception)
        {
            this._callingMethodBase = new StackTrace().GetFrame(_callingMethodStackFrame).GetMethod();
            this.WriteMessageToFileWithHeader(TraceEventType.Error.ToString(), exception.ToString());
        }

        /// <summary>
        /// Logs Exception
        /// </summary>
        /// <param name="error"></param>
        public void LogException(string error)
        {
            this._callingMethodBase = new StackTrace().GetFrame(_callingMethodStackFrame).GetMethod();
            this.WriteMessageToFileWithHeader(TraceEventType.Error.ToString(), error);
        }

        /// <summary>
        /// Logs Information
        /// </summary>
        /// <param name="message"></param>
        public void LogInformation(string message)
        {
            this._callingMethodBase = new StackTrace().GetFrame(_callingMethodStackFrame).GetMethod();
            this.WriteMessageToFileWithHeader(TraceEventType.Information.ToString(), message);
        }

        /// <summary>
        /// Logs Verbose
        /// </summary>
        /// <param name="message"></param>
        public void LogVerbose(string message)
        {
            this._callingMethodBase = new StackTrace().GetFrame(_callingMethodStackFrame).GetMethod();
            this.WriteMessageToFileWithHeader(TraceEventType.Verbose.ToString(), message);
        }

        /// <summary>
        /// Logs Critical 
        /// </summary>
        /// <param name="exception"></param>
        public void LogCriticalException(Exception exception)
        {
            this._callingMethodBase = new StackTrace().GetFrame(_callingMethodStackFrame).GetMethod();
            this.WriteMessageToFileWithHeader(TraceEventType.Critical.ToString(), exception.ToString());
        }

        /// <summary>
        /// Logs Critical
        /// </summary>
        /// <param name="message"></param>
        public void LogCriticalException(string message)
        {
            this._callingMethodBase = new StackTrace().GetFrame(_callingMethodStackFrame).GetMethod();
            this.WriteMessageToFileWithHeader(TraceEventType.Critical.ToString(), message);
        }

        /// <summary>
        /// Logs Warning
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message)
        {
            this._callingMethodBase = new StackTrace().GetFrame(_callingMethodStackFrame).GetMethod();
            this.WriteMessageToFileWithHeader(TraceEventType.Warning.ToString(), message);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Formats Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string FormatExceptionInfo(Exception ex)
        {
            MethodBase methodBase = this._callingMethodBase;
            string result = string.Empty;
            result = result + "Error Detected: " + methodBase.ReflectedType + "." + methodBase.Name + "()\t";
            result = result + "Error Msg: " + ex.Message + "\t";
            if (ex.InnerException != null)
            {
                result = result + "Inner Exception Msg: " + ex.InnerException.Message + "\t";
            }
            if (ex.StackTrace != string.Empty)
            {
                result = result + "Stack Trace: " + ex.StackTrace;
            }
            return result;
        }

        #endregion
    }

    /// <summary>
    /// Partail class for text file logging funtionality 
    /// </summary>
    public partial class Logger
    {
        #region Fields

        private static StreamWriter _streamWriter;
        private static readonly object _objLock;
        private static string _currentLogFile;

        #endregion

        #region Constructors

        static Logger()
        {
            _currentLogFile = string.Empty;
            _objLock = new object();
            CreateLogFile();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates Log File
        /// </summary>
        private static void CreateLogFile()
        {
            try
            {
                string newlogFile = GetLogFileName();
                //string rootPath = ConfigManager.GetConfigurationSetting("LogFileRootPath", string.Empty)+ _currentLogFile;
                if (_streamWriter != null)
                {
                    _streamWriter.Close();
                }
                _streamWriter = new StreamWriter(HttpContext.Current.Server.MapPath(newlogFile), true);
                _streamWriter.AutoFlush = true;
                _currentLogFile = newlogFile;
 
            }
            catch (Exception ex)
            {

                LogInEventLog(EventLogEntryType.Error, ex.ToString());
            }
           }

        /// <summary>
        /// Returns log file name
        /// </summary>
        /// <returns></returns>
        private static string GetLogFileName()
        {
            return "DeploymentLog-" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt";
        }

        /// <summary>
        /// Check if new log file required based on current date time ( as every day should have seperate log file)  
        /// </summary>
        /// <returns></returns>
        private static bool IsNewLogFileRequired()
        {
            return _currentLogFile != GetLogFileName();
        }

        /// <summary>
        /// Returns the method name which called the logger
        /// </summary>
        /// <returns></returns>
        private string GetMethodInfo()
        {
            MethodBase methodBase = this._callingMethodBase;
            return "Type:" + methodBase.ReflectedType + Environment.NewLine + "Method:" + methodBase.Name;
        }

        /// <summary>
        /// Returns the message header for logging
        /// </summary>
        /// <returns></returns>
        private string GetMessageHeader(string level)
        {
            return "Level:" + level + Environment.NewLine + "TimeStamp:" + DateTime.Now.ToString() + Environment.NewLine + this.GetMethodInfo();
        }

        /// <summary>
        /// Logs message with header
        /// </summary>
        /// <param name="message"></param>
        private void WriteMessageToFileWithHeader(string level, string message)
        {
            var messageWithHeader = this.GetMessageHeader(level) + Environment.NewLine + "Message:" + message + Environment.NewLine;
            this.WriteToFile(messageWithHeader);
        }

        /// <summary>
        /// Write message to file using thread safe approach
        /// </summary>
        /// <param name="message"></param>
        private void WriteToFile(string message)
        {
            lock (_objLock)
            {
                try
                {
                    if (IsNewLogFileRequired())
                    {
                        CreateLogFile();
                    }
                    _streamWriter.WriteLine(message);
                }
                catch (Exception ex)
                {
                    LogInEventLog(EventLogEntryType.Error, ex.ToString());
                    LogInEventLog(EventLogEntryType.Information, message);
                }
            }
        }

        private const string EVENTSOURCE = "DeploymentService";
        private const string EVENTLOG = "Application";

        private static void LogInEventLog(EventLogEntryType type, string message, params object[] paramsObjects)
        {

            try
            {
                if (!EventLog.SourceExists(EVENTSOURCE))
                    EventLog.CreateEventSource(EVENTSOURCE, EVENTLOG);
                EventLog.WriteEntry(EVENTSOURCE, message, type);
            }
            catch
            {
                // Do nothing... this is a last resort
            }
        }
        #endregion
    }

}
