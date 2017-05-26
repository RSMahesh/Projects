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
    public class ActivityLoggerTest
    {
        string fileName = "C:\\Big.dat";

        [Test]
        public void test1()
        {
           var task = Task.Run(() => CopyFile());
            Thread.Sleep(10);
            var inputFile = new FileStream(
         fileName,
         FileMode.OpenOrCreate,
         System.IO.FileAccess.Write,
         FileShare.ReadWrite);

            using (BinaryWriter writer = new BinaryWriter(inputFile))
            {
                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(Encoding.ASCII.GetBytes("assasas".PadLeft(10)));
                writer.Seek(0, SeekOrigin.End);
            }
        }

        private void CopyFile()
        {

            //  File.Copy(fileName, "C:\\tt.dat");

            copyFileExt(fileName, "C:\\tt.dat");
        }

        [TearDown]
        public void TearDown()
        {
            //File.Delete("C:\\tt.dat");
        }

        private void copyFileExt(string sourceFile, string destinationFile)
        {

            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }

            using (var inputFile = new FileStream(
         sourceFile,
         FileMode.Open,
         System.IO.FileAccess.Read,
         FileShare.ReadWrite))
            {
                using (var outputFile = new FileStream(destinationFile, FileMode.CreateNew))
                {
                    var buffer = new byte[0x10000];
                    int bytes;

                    while ((bytes = inputFile.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outputFile.Write(buffer, 0, bytes);
                    }
                }
            }
        }
    }
}
