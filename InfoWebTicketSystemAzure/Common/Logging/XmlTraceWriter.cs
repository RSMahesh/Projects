/*************************************************
* Name: Logging.XmlTraceWriter
* Purpose: To provide a common group of functions
*      for a writing the XmlTraceListener.
* Created: 10/28/2004
* Last Modified: 1/18/2005
*************************************************/

using System;
using System.Configuration;

namespace ResellerClub.Common.Logging
{
    /// <summary>
    /// XmlTraceWriter class. Used to write trace/debug information based in the common xml format.
    /// </summary>
    public sealed class XmlTraceWriter
    {
        private XmlTraceWriter()
        { }

        private static int traceLevelSetting;
        private static int traceErrorLevelSetting;
        private static bool benchmarkLevelSetting;

        static XmlTraceWriter()
        {
            traceLevelSetting = int.Parse(ConfigurationManager.AppSettings["TraceLevel"] == null ? "2" : ConfigurationManager.AppSettings["TraceLevel"]);
            traceErrorLevelSetting = int.Parse(ConfigurationManager.AppSettings["TraceErrorLevel"] == null ? "0" : ConfigurationManager.AppSettings["TraceErrorLevel"]);
            benchmarkLevelSetting = bool.Parse(ConfigurationManager.AppSettings["BenchmarkListener"] == null ? "false" : ConfigurationManager.AppSettings["BenchmarkListener"]);
        }

        /// <summary>
        /// This is used primarily for debug printing
        /// </summary>
        public const int TRACE_LEVEL_DETAILED_INFORMATION = 3;
        /// <summary>
        /// Used to output the basic information
        /// </summary>
        public const int TRACE_LEVEL_INFORMATION = 2;
        /// <summary>
        /// Used to output warnings (known exceptions, ect)
        /// </summary>
        public const int TRACE_LEVEL_WARNING = 1;
        /// <summary>
        /// Used for exception handlers
        /// </summary>
        public const int TRACE_LEVEL_ERROR = 0;

        /// <summary>
        /// Used for exception handlers
        /// </summary>
        public const int TRACE_LEVEL_BENCHMARK = 100;

        /// <summary>
        /// To trace write information. Will always output <see cref="Fiserv.BANKLINK.Common.Logging.XmlTraceWriter.TRACE_LEVEL_DETAILED_INFORMATION">TRACE_LEVEL_DETAILED_INFORMATION</see>
        /// </summary>
        /// <param name="message">The message to be written</param>
        public static void Trace(string message)
        {
            Trace(message, TRACE_LEVEL_INFORMATION);
        }


        public static void Trace(Exception ex)
        {
            Trace( Environment.NewLine + "Message:"+ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace  + Environment.NewLine  , TRACE_LEVEL_ERROR);
        }

        public static void TraceInfo(string info)
        {
            System.Diagnostics.Trace.Write(info + Environment.NewLine);
        }
        

        /// <summary>
        /// To trace write information.
        /// </summary>
        /// <param name="message">The message to be written</param>
        /// <param name="traceLevel">The level that this message will be written at.
        ///    Checks against the app.config for the applications tracelevel setting.</param>
        public static void Trace(string message, int traceLevel)
        {
            if (traceLevel == TRACE_LEVEL_BENCHMARK)
            {
                if (benchmarkLevelSetting)
                    System.Diagnostics.Trace.WriteLine(message);
            }
            else
            {
                message = traceLevel.ToString() + "\t" + message;

                if (traceLevel <= traceErrorLevelSetting)
                {
                    System.Diagnostics.Trace.Write(message);
                }
                else
                    if (traceLevel <= traceLevelSetting)
                        System.Diagnostics.Trace.Write(message, "true");
            }
        }
    }
}
