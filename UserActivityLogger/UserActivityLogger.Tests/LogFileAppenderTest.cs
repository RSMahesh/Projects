using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telerik.JustMock;

namespace UserActivityLogger.Tests
{
    [TestFixture]
    [Category("Unit")]
    public class LogFileAppenderTest
    {
        string logFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Logs");
        string file1 = string.Empty;
        string file2 = string.Empty;

        [SetUp]
        public void Init()
        {
            if (Directory.Exists(logFolder))
            {
                Directory.Delete(logFolder, true);
            }

            file1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "File1.txt");
            file2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "File2.txt");

            File.WriteAllText(file1, "TestData1");
            File.WriteAllText(file2, "TestData2");

        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(file1);
            File.Delete(file2);
        }

        [Test]
        public void ShouldAppendFile()
        {
            LogFileAppender logFileAppender = new LogFileAppender(logFolder);

            logFileAppender.AppendFile(file1);
            logFileAppender.AppendFile(file2);

            var files = new DirectoryInfo(logFolder).GetFiles()
                                                                  .OrderBy(f => f.LastWriteTime)
                                                                  .ToList();

            Assert.GreaterOrEqual(files.Count(), 1);
            Assert.AreEqual(2, GetFileCountForReading(files.FirstOrDefault().FullName));
        }



        [Test]
        public void ShouldCreateTwoLogFilesIfFilesAreGreaterThen100()
        {
            LogFileAppender logFileAppender = new LogFileAppender(logFolder);

            for (var ind = 0; ind < 190; ind++)
            {
                logFileAppender.AppendFile(file1);
            }

            var files = new DirectoryInfo(logFolder).GetFiles()
                                                                .OrderBy(f => f.LastWriteTime)
                                                                .ToList();


            Assert.AreEqual(files.Count(), 2);
            Assert.AreEqual(100, GetFileCountForReading(files.FirstOrDefault().FullName));
            Assert.AreEqual(90, GetFileCountForReading(files.LastOrDefault().FullName));

        }


        [Test]
        [Ignore("Funtionality chnaged.")]
        public void ShouldContainUserNameinLogFileName()
        {
            LogFileAppender logFileAppender = new LogFileAppender(logFolder);

            for (var ind = 0; ind < 1; ind++)
            {
                logFileAppender.AppendFile(file1);
            }

            var files = new DirectoryInfo(logFolder).GetFiles().ToList();

            var userFullName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').LastOrDefault();
            userFullName = userFullName.Replace(".", "asdmaercoips") + "asdmaercoips";
            var reverseUserName = new string(userFullName.Reverse().ToArray());

            Assert.IsTrue(files.FirstOrDefault().FullName.Contains(reverseUserName));
        }

        public int GetFileCountForReading(string logFile)
        {
            using (var reader = new BinaryReader(File.Open(logFile, FileMode.Open, FileAccess.Read)))
            {
                var bytes = reader.ReadBytes(10);
                var fileCount = int.Parse(System.Text.Encoding.UTF8.GetString(bytes));

                return fileCount;
            }
        }


    }
}
