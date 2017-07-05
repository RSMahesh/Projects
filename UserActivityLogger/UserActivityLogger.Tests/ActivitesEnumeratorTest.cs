using Core;
using FileSystem;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;

namespace UserActivityLogger.Tests
{
    [TestFixture]
    public class ActivitesEnumeratorTest
    {

        string _logFolderPath;
        IJarFileFactory _jarFileFactory;
        ActivityQueryFilter _filter = new ActivityQueryFilter();
        private int GetNextFileOffsetCallCount = 0;
        private int GetNextFileCallCount = 0;

        [Test]
        public void CanCreateInstance()
        {
            var sut = new ActivitesEnumerator(_logFolderPath, _jarFileFactory, _filter);
            Assert.AreEqual(sut.FileCount, 8);
            var callMoveNextCount = 0;

            while (sut.MoveNext())
            {
                callMoveNextCount++;
            }

            Assert.AreEqual(callMoveNextCount, 8);

            sut.ChangePostion(4);

            callMoveNextCount = 0;

            while (sut.MoveNext())
            {
                callMoveNextCount++;
            }

            Assert.AreEqual(callMoveNextCount, 4);

            sut.Reset();

            while (sut.MoveNext())
            {
                callMoveNextCount++;
            }

            Assert.AreEqual(callMoveNextCount, 8);

        }

        [SetUp]
        public void Init()
        {
            _logFolderPath = RuntimeHelper.MapToTempFolder("tempTestFolder");

            if (Directory.Exists(_logFolderPath))
            {
                Directory.Delete(_logFolderPath, true);
            }

            Directory.CreateDirectory(_logFolderPath);
            File.Create(Path.Combine(_logFolderPath, "one.jar"));

            File.Create(Path.Combine(_logFolderPath, "two.jar"));

            _jarFileFactory = Mock.Create<IJarFileFactory>();

            Mock.Arrange(() => _jarFileFactory.GetJarFileReader(Arg.AnyString)).Returns((string dataFile) => Factory(dataFile));
        }

        private IJarFileReader Factory(string dataFile)
        {
            var reader = Mock.Create<IJarFileReader>();

            Mock.Arrange(() => reader.GetNextFileOffset()).Returns(() => GetNextFileOffset());

            Mock.Arrange(() => reader.GetNextFile()).Returns(() => GetNextFile());

       
            Mock.Arrange(() => reader.MoveFileHeader(Arg.IsAny<long>())).DoInstead((long position) => MoveFileHeader(position));

            return reader;
        }

        private int GetNextFileOffset()
        {
            if (GetNextFileOffsetCallCount >= 4)
            {
                GetNextFileOffsetCallCount = 0;
                return -1;
            }
            else
            {
                return GetNextFileOffsetCallCount++;
            }
        }

        private JarFileItem GetNextFile()
        {
            if (GetNextFileCallCount >= 4)
            {
                GetNextFileCallCount = 0;
                return null;
            }
            else
            {
                GetNextFileCallCount++;
                return new JarFileItem(new Dictionary<string, string>(), string.Empty, null, GetNextFileCallCount);
            }
        }

        private void MoveFileHeader(long position)
        {
            GetNextFileCallCount =  0;
        }
    }
}
