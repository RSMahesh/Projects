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
    public class FileAppenderTest
    {
        string file1 = string.Empty;
        string file2 = string.Empty;
        const string dataFile = "C:\\data.log";

        [SetUp]
        public void Init()
        {
            file1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "File1.txt");
            file2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "File2.txt");
            File.WriteAllText(file1, "TestData1");
            File.WriteAllText(file2, "TestData2");
            File.Delete(dataFile);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(file1);
            File.Delete(file2);
            File.Delete(dataFile);
        }

        [Test]
        public void ShouldAppendFile()
        {
            FileAppender logFileAppender = new FileAppender();
            logFileAppender.AppendFile(file1,dataFile);
            logFileAppender.AppendFile(file2,dataFile);

            Assert.AreEqual(2, GetFileCountForReading(dataFile));
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
