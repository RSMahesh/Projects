using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using System.Web;

namespace ResellerClub.Common.Logging
{
    /// <summary>
    /// A no-frills trace listener that writes to a rolling text file.
    /// Designed specifically to replace XmlTraceListener.
    /// </summary>
    public class TextTraceListener : System.Diagnostics.TraceListener
    {
        private string logfileNameTrace;
        private StreamWriter fileStreamTrace;
        private string logfileNameBenchmark;
        private StreamWriter fileStreamBenchmark;

        private string rootLogPath;
        private long maxFileSize;

        /// <summary>
        /// TextTraceListener contructor.
        /// </summary>
        public TextTraceListener(string logPath)
        {
            if (logPath == String.Empty)
                logPath = HttpContext.Current.Server.MapPath(".");
            rootLogPath = logPath;
            maxFileSize = ConfigurationManager.AppSettings["TraceLogPruneCount"] == null ? 8 * 1024 * 1024 : long.Parse(ConfigurationManager.AppSettings["TraceLogPruneCount"]) * 1024;

            logfileNameTrace = logPath + "\\" + (ConfigurationManager.AppSettings["TraceLogFileName"] == null ? "TraceLog.txt" : ConfigurationManager.AppSettings["TraceLogFileName"]);
            fileStreamTrace = InitStreamWriter(ref logfileNameTrace);

            logfileNameBenchmark = (ConfigurationManager.AppSettings["BenchmarkFileName"] == null ? logfileNameTrace.Replace("Trace", "Benchmark") : logPath + "\\" + ConfigurationManager.AppSettings["BenchmarkFileName"]);
            fileStreamBenchmark = InitStreamWriter(ref logfileNameBenchmark);
        }



        /// <summary>
        /// Writes the message to a fileStreamTrace with predefined header.
        /// </summary>
        public override void Write(string message, string singleLine)
        {
            StackTrace stack = new StackTrace(4);
            StackFrame frame = stack.GetFrame(1);
            MethodBase methodBase = frame.GetMethod();
            StringBuilder traceMsg = new StringBuilder();

            try
            {
                CheckFileSize(logfileNameTrace, ref fileStreamTrace);

                traceMsg.Append(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss.fffffff") + "\t");
                traceMsg.Append(methodBase.Module + "\t" + methodBase.Name + "\t");
                traceMsg.Append(message);

                fileStreamTrace.WriteLine(traceMsg.ToString());
            }
            catch
            {
                // Ignore all exceptions during logging
            }
        }

        /// <summary>
        /// Writes the message to a fileStreamTrace with predefined header.
        /// </summary>
        public override void Write(string message)
        {
            try
            {
                CheckFileSize(logfileNameTrace, ref fileStreamTrace);
                WriteHeader();
                fileStreamTrace.WriteLine(message);
            }
            catch
            {
                // Ignore all exceptions during logging
            }
        }

        /// <summary>
        /// Writes the message to a fileStreamTrace without predefined header.
        /// </summary>
        public override void WriteLine(string message)
        {

            StackTrace stack = new StackTrace(4);
            StackFrame frame = stack.GetFrame(0);
            MethodBase methodBase = frame.GetMethod();
            StringBuilder traceMsg = new StringBuilder();

            try
            {
                CheckFileSize(logfileNameBenchmark, ref fileStreamBenchmark);

                traceMsg.Append(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss.fffffff") + "\t");
                traceMsg.Append(methodBase.ReflectedType.FullName + "." + methodBase.Name + "\t");
                traceMsg.Append(message);

                fileStreamBenchmark.WriteLine(traceMsg.ToString());
            }
            catch
            {
                // Ignore all exceptions during logging
            }
        }

        private void WriteHeader()
        {
            StackTrace stack = new StackTrace(4);
            StackFrame frame = stack.GetFrame(1);
            MethodBase methodBase = frame.GetMethod();

            fileStreamTrace.WriteLine();
            fileStreamTrace.WriteLine("TimeStamp: " + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss.fffffff"));  //DateTime.Now
            fileStreamTrace.WriteLine("Assembly: " + ResellerClub.Common.Diagnostics.GetFullAssemblyName(Assembly.GetAssembly(methodBase.DeclaringType)));
            fileStreamTrace.WriteLine("Class: " + methodBase.ReflectedType.FullName);      //methodBase.ReflectedType.FullName
            fileStreamTrace.WriteLine("Method: " + methodBase.Name);     //methodBase.Name
            fileStreamTrace.Write("Message: ");
            fileStreamTrace.WriteLine();
        }

        private StreamWriter InitStreamWriter(ref string logfileName)
        {
            StreamWriter fileStream = null;

            try
            {
                FileStream  streamWithReadAcess = File.Open(logfileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                fileStream = new StreamWriter(streamWithReadAcess);
            }
            catch
            {
                throw;
                logfileName = rootLogPath + "\\" + Guid.NewGuid().ToString() + ".log";
                fileStream = new StreamWriter(logfileName, true);
            }

            fileStream.AutoFlush = true;
            return fileStream;
        }

        private void CheckFileSize(string logfileName, ref StreamWriter fileStream)
        {
            if (fileStream.BaseStream.Length > maxFileSize)
            {
                lock (this)  //Rollover File with single thread
                {
                    fileStream.Close();
                    File.Move(logfileName, logfileName.Insert(logfileName.Length - 4, "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss")));
                    fileStream = InitStreamWriter(ref logfileName);
                }
            }
        }

        /// <summary>
        /// Releases the resources used by TextTraceListener.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                fileStreamTrace.Close();
                fileStreamBenchmark.Close();
            }
        }
    }
}
