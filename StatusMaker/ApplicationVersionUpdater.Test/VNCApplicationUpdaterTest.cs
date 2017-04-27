using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationVersionUpdater.Test
{
    [TestFixture]
    public class VNCApplicationUpdaterTest
    {
        [Test]
        public void ShouldGetCorrectVersionNumber()
        {
            VNCAppUpdater sut = new VNCAppUpdater(GetAppUpdateDetail());
            var latestVerion = sut.GetLatestVesrion();
            Assert.AreEqual("1.0.0.11", latestVerion);
        }

        [Test]
        public void ShouldUpdateApp()
        {
            var vncAppUpdateDetail = GetAppUpdateDetail(1);
            VNCAppUpdater sut = new VNCAppUpdater(vncAppUpdateDetail);

            var exludeFile = "Version.txt";

            sut.UpdateApp(new[] { exludeFile });

            var foundKya = Directory.GetFiles(vncAppUpdateDetail.Destination).FirstOrDefault(x => Path.GetFileName(x).Equals(exludeFile));

            Assert.IsNull(foundKya);

            Assert.AreEqual(Directory.GetFiles(vncAppUpdateDetail.Destination).Count(), Directory.GetFiles(vncAppUpdateDetail.Source).Count() - 1);
        }


        [Test]
        public void ShouldThrowExceptionWhenSourceIsNotReachable()
        {
            var sut = new VNCAppUpdater(new AppUpdateDetail(@"\\10.131.70.129\StausMaker", ""));
            Assert.Throws<LocationNotReachable>(()=>  sut.GetLatestVesrion());
        }

        private AppUpdateDetail GetAppUpdateDetail(int numberOfFiles = 10)
        {
            var pp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var vncAppUpdateDetail = new AppUpdateDetail(Path.Combine(pp, "source"), Path.Combine(pp, "destination"));

            Utils.CreateFolderWithFiles(vncAppUpdateDetail.Source, numberOfFiles);

            Utils.CreateFolderWithFiles(vncAppUpdateDetail.Destination);

            File.WriteAllText(Path.Combine(vncAppUpdateDetail.Source, "Version.txt"), "1.0.0.11");

            return vncAppUpdateDetail;
        }


    }
}
