using NUnit.Framework;
using System.IO;

namespace UserActivityLogger.Tests
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
                    var bytes = logFolderReader.GetNextImageBytes();

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
