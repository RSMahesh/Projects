using AppUpdater.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdater.Integration
{
    [TestFixture]
    [Category("Integration")]
    public class GitHubAppUpdaterTest
    {
        IGitHub _gitHub = new GitHub(new HttpEngine());

        [Test]
        public void ShouldGetCorrectVersionNumber()
        {
            var sut = new GitHubAppUpdater(_gitHub,GetAppUpdateDetail());
            var latestVerion = sut.GetLatestVesrion();
            Assert.AreEqual("1.0.0.11", latestVerion);
        }

        [Test]
        public void ShouldUpdateApp()
        {
            var appUpdateDetail = GetAppUpdateDetail();

            var sut = new GitHubAppUpdater(_gitHub, appUpdateDetail);
         
            sut.UpdateApp();

            var foundKya = Directory.GetFiles(appUpdateDetail.LocalDestination).FirstOrDefault(x => Path.GetFileName(x).Equals(appUpdateDetail.ExcludeFiles.FirstOrDefault()));

            Assert.IsNull(foundKya);

            var files = (object[])new HttpEngine().GetHttpResponseObjectAsync(appUpdateDetail.RemoteSource).Result;

            Assert.AreEqual(Directory.GetFiles(appUpdateDetail.LocalDestination).Count(), files.Count() - 1);
        }

        [Test]
        public void ShouldThrowExceptionWhenSourceIsNotReachable()
        {
            var sut = new GitHubAppUpdater(_gitHub, new AppUpdateDetail(@"https://api.github.com/repos/MrBailwal/Repo", ""));
            Assert.Throws<LocationNotReachableException>(() => sut.GetLatestVesrion());
        }
        private AppUpdateDetail GetAppUpdateDetail()
        {
            var applicationDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var appUpdateDetail = new AppUpdateDetail("https://api.github.com/repos/MaheshBailwal/TestRepo/contents/App", Path.Combine(applicationDataDir, "destination"), new[] { "Version.txt" });

            Utils.CreateFolderWithFiles(appUpdateDetail.LocalDestination);

            return appUpdateDetail;
        }
    }
}
