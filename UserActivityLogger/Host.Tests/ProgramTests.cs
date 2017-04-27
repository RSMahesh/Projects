using Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Host.Tests
{
    [TestFixture]
    [Category("Functional")]
    
    public class ProgramTests
    {
        string exe = @"D:\Projects\UserActivityLogger\Host\bin\Debug\SysHealth.exe";
        string _logFolder = @"C:\SysWin\SysLogs";
      
        [Test]
        public void TestActivityLogger()
        {
            CreateLogFiles(DateTime.Now.AddDays(-5), 2, 100);
            Assert.AreEqual(Directory.GetFiles(_logFolder).Count(), 100);

            ProcessHelper.KillProcess(Path.GetFileNameWithoutExtension(exe));
            ProcessHelper.RunAsBackGround(exe);
            Assert.IsTrue(ProcessHelper.GetProcess(exe).Any());

            Thread.Sleep(2 * 1000);

            Assert.LessOrEqual(Directory.GetFiles(_logFolder).Count(), Directory.GetFiles(_logFolder).Count());

        }

        [TearDown]
        public void TearDown()
        {
            ProcessHelper.KillProcess(Path.GetFileNameWithoutExtension(exe));
        }

        private void CreateLogFiles(DateTime startDateOfLastWrite, int minutesToadd, int numberOfFiles)
        {
            if (Directory.Exists(_logFolder))
            {
                Directory.Delete(_logFolder, true);
            }

            Directory.CreateDirectory(_logFolder);

            for (var i = 0; i < numberOfFiles; i++)
            {
                var filePath = Path.Combine(_logFolder, Guid.NewGuid().ToString() + ".log");

                File.WriteAllText(filePath, "Testing File");

                File.SetLastWriteTime(filePath, startDateOfLastWrite);

                startDateOfLastWrite = startDateOfLastWrite.AddMinutes(minutesToadd);
            }

            var fileInfos = new DirectoryInfo(_logFolder).GetFiles("*.log")
                                                                .OrderBy(f => f.LastWriteTime)
                                                            .ToList();
        }
    }
}
