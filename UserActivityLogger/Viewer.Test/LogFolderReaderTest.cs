using NUnit.Framework;
using RecordSession;
using System.IO;

namespace Viewer.Test
{
    [TestFixture]
    [Category("Unit")]
    public class LogFolderReaderTest
    {
        string logFolder = @"D:\Projects\UserActivityLogger\UserActivityLogger.Tests\TestFiles";

        [Test]
        public void ShouldReadLogFolder()
        {
            using (LogFolderReader logFolderReader = new LogFolderReader())
            {
                logFolderReader.SetLogFolderPath(logFolder);
                var fileCount = logFolderReader.GetFileCountForReading();
                Assert.AreEqual(fileCount, 4);

                var actualFileCount = 0;

                while (true)
                {
                    var bytes = logFolderReader.GetNextImage();

                    if (bytes == null)
                    {
                        break;
                    }


                    var fileText = System.Text.Encoding.ASCII.GetString(bytes);
                    
                    actualFileCount++;
                    Assert.AreEqual(fileText, "TestData" + actualFileCount);
                }
            }

        }
    }
}
