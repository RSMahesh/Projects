using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Host.Tests
{
    [TestFixture]
    public class LogFolderPurgerTest
    {
        string _logFolder = string.Empty;

        [SetUp]
        public void StartUp()
        {
            _logFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "tempLogFolder");
        }

        [Test]
        public void ShouldDeleteFilesOldestFilesToMaxLimit()
        {
            DateTime startDateOfLastWrite = DateTime.Now.AddDays(-5);

            CreateLogFiles(startDateOfLastWrite, 1, 50);

            Assert.AreEqual(Directory.GetFiles(_logFolder).Count(), 50);

            var sut = new LogFolderPurger();
            sut.StartPurging(_logFolder, TimeSpan.FromSeconds(1));
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Assert.AreEqual(Directory.GetFiles(_logFolder).Count(), 20);

            startDateOfLastWrite = startDateOfLastWrite.AddMinutes(1 * 30);

            var fileInfos = new DirectoryInfo(_logFolder).GetFiles("*.log")
                                                                   .OrderBy(f => f.LastWriteTime)
                                                               .ToList();

            Assert.GreaterOrEqual(fileInfos[0].LastWriteTime, startDateOfLastWrite);
        }

        [Test]
        public void ShouldDeleteFilesIfOldEnough()
        {
            DateTime startDateOfLastWrite = DateTime.Now.AddDays(-1);

            CreateLogFiles(startDateOfLastWrite, 1, 50);

            Assert.AreEqual(Directory.GetFiles(_logFolder).Count(), 50);

            var sut = new LogFolderPurger();
            sut.StartPurging(_logFolder, TimeSpan.FromSeconds(1));
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Assert.AreEqual(Directory.GetFiles(_logFolder).Count(), 50);


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
