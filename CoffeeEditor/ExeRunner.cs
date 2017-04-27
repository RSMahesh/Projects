// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExeRunner.cs" company="">
//   
// </copyright>
// <summary>
//   Class for running exe in seprate proccess
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

#endregion

namespace MediaProcessor.ServiceLibrary.Common
{
    /// <summary>
    ///     Class for running exe in seprate proccess
    /// </summary>
    public static class ExeRunner
    {
        /// <summary>
        /// Executes the specified full executable path.
        /// </summary>
        /// <param name="fullExePath">
        /// The full executable path.
        /// </param>
        /// <param name="exeParams">
        /// The executable parameters.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Throws exception  if exe exit with errors
        /// </exception>
        public static string Execute(string fullExePath, string exeParams)
        {
            var output = string.Empty;
            var exeProcessStartInfo = new ProcessStartInfo(fullExePath, exeParams)
                                          {
                                              WorkingDirectory =
                                                  Path.GetDirectoryName(
                                                      fullExePath), 
                                              CreateNoWindow = true, 
                                              UseShellExecute = false, 
                                              ErrorDialog = false, 
                                              RedirectStandardError = true, 
                                              RedirectStandardOutput = true
                                          };

            var exeProcess = new Process();
            exeProcess.EnableRaisingEvents = true;
            exeProcess.StartInfo = exeProcessStartInfo;
            exeProcess.Start();

            StreamReader stdReader = exeProcess.StandardOutput;
            output = stdReader.ReadToEnd();

            StreamReader errReader = exeProcess.StandardError;
            string errOut = errReader.ReadToEnd();
            exeProcess.WaitForExit();

            var errors = errOut != null && errOut.Length > 0 ? errOut : string.Empty;

            if (exeProcess.HasExited)
            {
                exeProcess.Close();
            }
            else
            {
                exeProcess.Kill();
            }

            exeProcess.Dispose();

            if (!string.IsNullOrEmpty(errors))
            {
                throw new ExeExecutionException(errors, fullExePath, exeParams);
            }

            return output;
        }

        /// <summary>
        /// Adds the ghost script path to environment variable.
        /// </summary>
        /// <param name="paths">
        /// The paths.
        /// </param>
        public static void AddGhostScriptPathToEnvironmentVariable(string paths)
        {
            if (string.IsNullOrEmpty(paths))
            {
                return;
            }

            var envPath = Environment.GetEnvironmentVariable("path");
            if (envPath != null)
            {
                var envPaths = envPath.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                var addlPathsList = paths.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (var addlPath in addlPathsList)
                {
                    var existingPathList = from ep in envPaths where ep.ToLower() == addlPath.ToLower() select ep;
                    if (existingPathList.ToList().Count <= 0)
                    {
                        envPath = addlPath + ";" + envPath;
                        envPaths.Add(addlPath);
                    }
                }
            }

            Environment.SetEnvironmentVariable("path", envPath);
        }
    }

    /// <summary>
    /// The exe execution exception.
    /// </summary>
    public class ExeExecutionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExeExecutionException"/> class.
        /// </summary>
        /// <param name="exeErrorMessage">
        /// The exe error message.
        /// </param>
        /// <param name="exePath">
        /// The exe path.
        /// </param>
        /// <param name="exeParameters">
        /// The exe parameters.
        /// </param>
        public ExeExecutionException(string exeErrorMessage, string exePath, string exeParameters)
            : base(
                "Error:" + exeErrorMessage + Environment.NewLine + " Exe:" + exePath + Environment.NewLine
                + " Exe Parameters :" + exeParameters)
        {
            ExeErrorMessage = exeErrorMessage;
            ExePath = exePath;
            ExeParameters = exeParameters;
        }

        /// <summary>
        /// Gets the exe error message.
        /// </summary>
        public string ExeErrorMessage { get; private set; }

        /// <summary>
        /// Gets the exe path.
        /// </summary>
        public string ExePath { get; private set; }

        /// <summary>
        /// Gets the exe parameters.
        /// </summary>
        public string ExeParameters { get; private set; }
    }
}