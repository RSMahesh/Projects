using MB.Core;
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
    public class GitHubAppUpdaterTest
    {
        [Test]
        public void ShouldGetCorrectVersionNumber()
        {
            var sut = new GitHubAppUpdater(GetAppUpdateDetail());
            var latestVerion = sut.GetLatestVesrion();
            Assert.AreEqual("1.0.0.11", latestVerion);
        }

        [Test]
        public void ShouldUpdateApp()
        {
            var appUpdateDetail = GetAppUpdateDetail();

            var sut = new GitHubAppUpdater(appUpdateDetail);

            var exludeFile = "Version.txt";

            sut.UpdateApp(new[] { exludeFile });

            var foundKya = Directory.GetFiles(appUpdateDetail.Destination).FirstOrDefault(x => Path.GetFileName(x).Equals(exludeFile));

            Assert.IsNull(foundKya);

            var files = (object[])new HttpEngine().GetHttpResponseObjectAsync(appUpdateDetail.Source).Result;

            Assert.AreEqual(Directory.GetFiles(appUpdateDetail.Destination).Count(), files.Count() - 1);
        }

        [Test]
        public void ShouldThrowExceptionWhenSourceIsNotReachable()
        {
            var sut = new GitHubAppUpdater(new AppUpdateDetail(@"https://api.github.com/repos/MrBailwal/Repo", ""));
            Assert.Throws<LocationNotReachable>(() => sut.GetLatestVesrion());
        }
        private AppUpdateDetail GetAppUpdateDetail()
        {
            var applicationDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var appUpdateDetail = new AppUpdateDetail("https://api.github.com/repos/MaheshBailwal/TestRepo/contents/App", Path.Combine(applicationDataDir, "destination"));

            Utils.CreateFolderWithFiles(appUpdateDetail.Destination);

            return appUpdateDetail;
        }

    }
}
