using Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Tests
{
    [TestFixture]
    [Category("Unit")]
    public class JarFileTest
    {
        string file1, file2, jarFiledata;

        [SetUp]
        public void Init()
        {
            file1 = RuntimeHelper.MapToTempFolder("File1.txt");
            file2 = RuntimeHelper.MapToTempFolder("File2.txt");
            jarFiledata = RuntimeHelper.MapToTempFolder("data.log");

            File.WriteAllText(file1, "TestData1");
            File.WriteAllText(file2, "TestData2");
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(file1);
            File.Delete(file2);
            File.Delete(jarFiledata);
        }

        [Test]
        public void ShouldAddFiles()
        {
            using (var jarFileWriter = new JarFile(FileAccessMode.Write, jarFiledata))
            {
                jarFileWriter.AddFile(file1);
                jarFileWriter.AddFile(file2);
            }

            using (var jarFileReader = new JarFile(FileAccessMode.Read, jarFiledata))
            {
                Assert.AreEqual(2, jarFileReader.FilesCount);
                Assert.AreEqual("TestData1", System.Text.Encoding.UTF8.GetString(jarFileReader.GetNextFile()));
                Assert.AreEqual("TestData2", System.Text.Encoding.UTF8.GetString(jarFileReader.GetNextFile()));
            }
        }

        [Test]
        public void ShouldThrowExceptionOnMaxLimit()
        {
            using (var jarFileWriter = new JarFile(FileAccessMode.Write, jarFiledata, 2))
            {
                jarFileWriter.AddFile(file1);
                jarFileWriter.AddFile(file2);
                Assert.Throws<JarFileReachedMaxLimitException>(() => jarFileWriter.AddFile(file2));
            }
        }

        [Test]
        public void ShouldThrowExceptionForInvalidModeOpretion()
        {
            using (var jarFileWriter = new JarFile(FileAccessMode.Write, jarFiledata))
            {
                jarFileWriter.AddFile(file1);

                Assert.Throws<InvalidOperationException>(() => jarFileWriter.GetNextFile());
               
            }

            using (var jarFileReader = new JarFile(FileAccessMode.Read, jarFiledata))
            {
                Assert.Throws<InvalidOperationException>(() => jarFileReader.AddFile(file1));
            }
        }
    }
}
