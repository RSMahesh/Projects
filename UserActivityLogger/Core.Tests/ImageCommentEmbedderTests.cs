using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests
{
    [TestFixture]
    public class ImageCommentEmbedderTests
    {
        string imgPath = @"D:\Projects\UserActivityLogger\Core.Tests\dogi.jpg";

        [Test]
        public void Test2()
        {
            var sut = new ImageCommentEmbedder();
            sut.AddComment(imgPath, "Test");
            var comments = sut.GetComments(imgPath);
            Assert.AreEqual("Test", comments);

            sut.AddComment(imgPath, string.Empty);
            comments = sut.GetComments(imgPath);
            Assert.AreEqual(string.Empty, comments);
        }

        [Test]
        public void Test23()
        {
            var sut = new ImageCommentEmbedder();
            string comments = string.Empty;
            using (var ms = new MemoryStream(File.ReadAllBytes(imgPath)))
            {
                comments = sut.GetComments(ms);
                Assert.AreEqual(string.Empty, comments);
                ms.Position = 0;

                var streamOut = sut.AddComment(ms, "Test");
                comments = sut.GetComments(streamOut);
            }

            Assert.AreEqual("Test", comments);
        }

        [TearDown]
        public void TearDown()
        {

        }

    }
}
