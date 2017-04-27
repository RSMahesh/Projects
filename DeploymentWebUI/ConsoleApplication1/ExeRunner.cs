using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MediaProcessor.ServiceLibrary.Common
{
    /// <summary>
    ///     Class for running exe in seprate proccess
    /// </summary>
    public static class ExeRunner
    {
        /// <summary>
        ///     Executes the specified full executable path.
        /// </summary>
        /// <param name="fullExePath">The full executable path.</param>
        /// <param name="exeParams">The executable parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Throws exception  if exe exit with errors</exception>
        public static string Execute(string fullExePath, string exeParams)
        {
            string output = "";
            var exeProcessStartInfo = new ProcessStartInfo(fullExePath, exeParams)
            {
                WorkingDirectory = Path.GetDirectoryName(fullExePath),
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

            string errors = (errOut != null && errOut.Length > 0 ? errOut : "");

            if (exeProcess.HasExited)
            {
                exeProcess.Close();
            }
            else
            {
                exeProcess.Kill();
            }
            exeProcess.Dispose();

            //if (!string.IsNullOrEmpty(errors))
            //{
            //    throw new ExeException("Error:" + errors +
            //                        Environment.NewLine + " Exe:" + fullExePath +
            //                        Environment.NewLine + " Exe Parameters :" + exeParams);
            //}
            return output + Environment.NewLine + "=====================Errors=========================" + Environment.NewLine + errors;
        }

        /// <summary>
        ///     Adds the ghost script path to environment variable.
        /// </summary>
        /// <param name="paths">The paths.</param>
        public static void AddGhostScriptPathToEnvironmentVariable(string paths)
        {
            if (string.IsNullOrEmpty(paths))
                return;

            string envPath = Environment.GetEnvironmentVariable("path");
            if (envPath != null)
            {
                List<string> envPaths = envPath.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                List<string> addlPathsList =
                    paths.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string addlPath in addlPathsList)
                {
                    IEnumerable<string> existingPathList = from ep in envPaths
                        where ep.ToLower() == addlPath.ToLower()
                        select ep;
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
}