using AppUpdater;
using AppUpdater.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;

namespace ApplicationVersionUpdater.Test
{
    [TestFixture]
    [Category("Unit")]
    public class VNCApplicationUpdaterFixture
    {
        // IHttpEngine _httpEngine = new HttpEngine();

        [Test]
        public void ShouldGetCorrectVersionNumberVnc()
        {
            var fileGateway = Mock.Create<IFileGateway>();
      
            Mock.Arrange(() => fileGateway.ReadAllText(Arg.AnyString)).Returns(() => "1.0.0.11").OccursOnce();

            var sut = new VNCAppUpdater(new AppUpdateDetail(string.Empty, string.Empty), fileGateway);
            var latestVerion = sut.GetLatestVesrion();
            Assert.AreEqual("1.0.0.11", latestVerion);

            Mock.AssertAll(fileGateway);
        }

        [Test]
        public void ShouldUpdateAppVnc()
        {
            var fileGateway = Mock.Create<IFileGateway>();

            var copyedFiles = new List<string>();

            Mock.Arrange(() => fileGateway.GetFiles(Arg.AnyString)).Returns(() => new[] { "file1" , "excludeme" }).OccursOnce();

            Mock.Arrange(() => fileGateway.Copy(Arg.AnyString, Arg.AnyString)).DoInstead((string source, string destination) => { copyedFiles.Add(source); }).OccursOnce();

            var sut = new VNCAppUpdater(new AppUpdateDetail(string.Empty, string.Empty, new[] { "excludeme" }), fileGateway);

            sut.UpdateApp();

            var foundKya = copyedFiles.FirstOrDefault(x => x.Equals("excludeme"));

            Assert.IsNull(foundKya);
            Assert.AreEqual(copyedFiles.Count, 1);
            Assert.AreEqual(copyedFiles.FirstOrDefault(), "file1");
            Mock.AssertAll(fileGateway);
        }

        [Test]
        public void ShouldThrowExceptionWhenSourceIsNotReachableVnc()
        {
            var fileGateway = Mock.Create<IFileGateway>();

            Mock.Arrange(() => fileGateway.ReadAllText(Arg.AnyString)).Throws(new IOException("Unit Test"));

            var sut = new VNCAppUpdater( new AppUpdateDetail(string.Empty, string.Empty), fileGateway);

            Assert.Throws<LocationNotReachableException>(() => sut.GetLatestVesrion());
        }
    }
}
