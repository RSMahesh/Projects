using AppUpdater.Integration;
using AppUpdater.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdater.Test.Integration
{
    [TestFixture]
    [Category("Integration")]
    public class VNCApplicationUpdaterTest
    {
        [Test]
        public void ShouldGetCorrectVersionNumber()
        {
            VNCAppUpdater sut = GetAppUpdateDetail();
            var latestVerion = sut.GetLatestVesrion();
            Assert.AreEqual("1.0.0.11", latestVerion);
        }

        [Test]
        public void ShouldUpdateApp()
        {
            
            VNCAppUpdater sut = GetAppUpdateDetail();

            sut.UpdateApp();

            var foundKya = Directory.GetFiles(sut.AppUpdateDetail.LocalDestination).FirstOrDefault(x => Path.GetFileName(x).Equals(sut.AppUpdateDetail.ExcludeFiles.FirstOrDefault()));

            Assert.IsNull(foundKya);

            Assert.AreEqual(Directory.GetFiles(sut.AppUpdateDetail.LocalDestination).Count(), Directory.GetFiles(sut.AppUpdateDetail.RemoteSource).Count() - 1);
        }

        [Test]
        [Category("LongRunning")]
        [Ignore("LongRunning")]
        public void ShouldThrowExceptionWhenSourceIsNotReachable_LongRunning()
        {
            var sut = new VNCAppUpdater(new AppUpdateDetail(@"\\10.131.70.129\StausMaker", ""), null);
            Assert.Throws<LocationNotReachableException>(()=>  sut.GetLatestVesrion());
        }

        private VNCAppUpdater GetAppUpdateDetail(int numberOfFiles = 10)
        {
            var pp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var vncAppUpdateDetail = new AppUpdateDetail(Path.Combine(pp, "source"), Path.Combine(pp, "destination"), new[] { "Version.txt" });

            Utils.CreateFolderWithFiles(vncAppUpdateDetail.RemoteSource, numberOfFiles);

            Utils.CreateFolderWithFiles(vncAppUpdateDetail.LocalDestination);

            File.WriteAllText(Path.Combine(vncAppUpdateDetail.RemoteSource, "Version.txt"), "1.0.0.11");


            return new VNCAppUpdater(vncAppUpdateDetail, new LocalFileGateway());
         
        }
    }
}
