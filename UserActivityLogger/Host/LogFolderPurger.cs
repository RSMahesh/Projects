using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Host
{
    public class LogFolderPurger
    {
        private readonly string copyLocation = @"\\10.131.60.128\ActivityLogs";
        public void StartPurging(string logFolder, TimeSpan pollingTimeInterval)
        {
            new Thread(() =>
             {
                 while (true)
                 {
                   //  try
                     {
                         PurgeFiles(logFolder);
                     }
                     //catch { }

                     Thread.Sleep(pollingTimeInterval);
                 }
             }).Start();
        }

        private void PurgeFiles(string logFolder)
        {
            var fileInfos = new DirectoryInfo(logFolder).GetFiles("*.log")
                                                                  .OrderBy(f => f.LastWriteTime)
                                                                  .ToList();

            var deleteLogsBeforeInDays = ConfigurationManager.AppSettings["DeleteLogsBeforeInDays"] ?? "3";
            var deleteBeforeDate = DateTime.UtcNow.AddDays(-int.Parse(deleteLogsBeforeInDays));

            if (fileInfos.Count() < 30)
            {
                return;
            }

            for(var i =0;i < fileInfos.Count && i < 30; i ++)
            {
                if (fileInfos[i].LastWriteTime < deleteBeforeDate)
                {
                    File.Delete(fileInfos[i].FullName);
                }
            }
        }

        private void CopyFile(string file)
        {
            try
            {
                File.Copy(file, Path.Combine(copyLocation, Path.GetFileName(file)));
            }
            catch(Exception ex)
            {

            }
        }
    }
}
