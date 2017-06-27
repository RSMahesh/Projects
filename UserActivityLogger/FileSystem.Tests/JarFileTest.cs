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
        Dictionary<string, string> header = new Dictionary<string, string>();

        [SetUp]
        public void Init()
        {
            file1 = RuntimeHelper.MapToTempFolder("File1.txt");
            file2 = RuntimeHelper.MapToTempFolder("File2.txt");
            jarFiledata = RuntimeHelper.MapToTempFolder("data.jar");

            File.WriteAllText(file1, "TestData1");
            File.WriteAllText(file2, "TestData2");

            if (File.Exists(jarFiledata))
            {
                File.Delete(jarFiledata);
            }

        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(file1);
            File.Delete(file2);
            File.Delete(jarFiledata);
        }

        [Test]
        public void ShouldAddFiles2()
        {

            using (var jarFileWriter = new JarFile(FileAccessMode.Write, jarFiledata))
            {
                header["FileName"] = file1;
                jarFileWriter.AddFile(new JarFileItem(header, file1));

                header["FileName"] = file2;
                jarFileWriter.AddFile(new JarFileItem(header, file2));

            }

            using (var jarFileReader = new JarFile(FileAccessMode.Read, jarFiledata))
            {
                Assert.AreEqual(2, jarFileReader.FilesCount);
                var jarItem1 = jarFileReader.GetNextFile();

                var jarItem2 = jarFileReader.GetNextFile();

                Assert.AreEqual("TestData1", System.Text.Encoding.UTF8.GetString(jarItem1.Containt));
                Assert.AreEqual("TestData2", System.Text.Encoding.UTF8.GetString(jarItem2.Containt));
            }
        }


        [Test]
        public void ShouldAllowReadConcurrently()
        {
            using (var jarFileWriter = new JarFile(FileAccessMode.Write, jarFiledata))
            {
                header["FileName"] = file1;
                jarFileWriter.AddFile(new JarFileItem(header, file1));
            }

            using (var sourceStream = new FileStream(
             jarFiledata,
             FileMode.Open,
             FileAccess.Read,
             FileShare.ReadWrite))
            {
                using (var jarFileWriter = new JarFile(FileAccessMode.Write, jarFiledata))
                {
                    header["FileName"] = file2;
                    jarFileWriter.AddFile(new JarFileItem(header, file2));

                }
            }
        }

        [Test]
        public void ShouldThrowExceptionOnMaxLimit()
        {

            using (var jarFileWriter = new JarFile(FileAccessMode.Write, jarFiledata, 2))
            {
                jarFileWriter.AddFile(new JarFileItem(header, file1));

                jarFileWriter.AddFile(new JarFileItem(header, file2));

                Assert.Throws<JarFileReachedMaxLimitException>(() => jarFileWriter.AddFile(new JarFileItem(header, file2)));
            }
        }

        [Test]
        public void ShouldThrowExceptionForInvalidModeOpretion()
        {
            using (var jarFileWriter = new JarFile(FileAccessMode.Write, jarFiledata))
            {
                jarFileWriter.AddFile(new JarFileItem(header, file1));

                Assert.Throws<InvalidOperationException>(() => jarFileWriter.GetNextFile());

            }

            using (var jarFileReader = new JarFile(FileAccessMode.Read, jarFiledata))
            {
                Assert.Throws<InvalidOperationException>(() => jarFileReader.AddFile(new JarFileItem(header, file1)));
            }
        }
    }
}
