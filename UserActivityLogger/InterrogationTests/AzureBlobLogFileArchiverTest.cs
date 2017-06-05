using Core;

using FileSystem;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserActivityLogger;

namespace InterrogationTests
{
    [TestFixture]
    [Category("Unit")]
    public class AzureBlobLogFileArchiverTest
    {
        string _logFile = string.Empty;
        IFileSystem _fileSystem = new AzureBlobFileSystem(ConfigurationManager.AppSettings["StorageConnectionString"]);

        string _archiveLocation = Path.Combine(Constants.SharedFolderPath, RuntimeHelper.GetCurrentUserName());

        [SetUp]
        public void StartUp()
        {
            _logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "testFile.txt");
        }

        [Test]
        public void ShouldDeleteFilesOldestFiles()
        {
            var fileContent = "Testing File";
            File.WriteAllText(_logFile, fileContent);
            var file = Path.Combine(@"0012345", Guid.NewGuid().ToString());
            var fullUrl = ConfigurationManager.AppSettings["BlobContainerBaseUri"] + "/" + file;
            _fileSystem.CopyFile(_logFile, file);
            var responseText = new HttpEngine().GetResponseStringAsync(fullUrl).Result;
            Assert.IsTrue(responseText.Equals(fileContent));
        }
    }
}
