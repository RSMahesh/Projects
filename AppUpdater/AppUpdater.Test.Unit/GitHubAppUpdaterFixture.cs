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
    public class GitHubAppUpdaterFixture
    {
        [Test]

        public void ShouldGetCorrectVersionNumberGit()
        {
            var httpEngine = Mock.Create<IGitHub>();

            var dic = new Dictionary<string, object> { { "download_url", "urlfortest" } };

            Mock.Arrange(() => httpEngine.ReadAllTextAysnc(Arg.AnyString)).Returns(() => Task.Run(() => "1.0.0.11")).OccursOnce();

            var sut = new GitHubAppUpdater(httpEngine, new AppUpdateDetail(string.Empty, string.Empty));
            var latestVerion = sut.GetLatestVesrion();
            Assert.AreEqual("1.0.0.11", latestVerion);

            Mock.AssertAll(httpEngine);
        }


        [Test]
        public void ShouldUpdateAppGit()
        {
            var gitHub = Mock.Create<IGitHub>();
            Mock.Arrange(() => gitHub.PullFolderServer(string.Empty, string.Empty, null)).IgnoreArguments().OccursOnce();
            var sut = new GitHubAppUpdater(gitHub, new AppUpdateDetail(string.Empty, string.Empty, Enumerable.Empty<string>()));
            sut.UpdateApp();
       
            Mock.AssertAll(gitHub);
        }

        [Test]
        public void ShouldThrowExceptionWhenSourceIsNotReachableGit()
        {
            var gitHub = Mock.Create<IGitHub>();

            Mock.Arrange(() => gitHub.ReadAllTextAysnc(Arg.AnyString)).Throws(new AggregateException("Unit Test"));

            var sut = new GitHubAppUpdater(gitHub, new AppUpdateDetail(string.Empty, string.Empty));

            Assert.Throws<LocationNotReachableException>(() => sut.GetLatestVesrion());
        }


        //[Test]
        //public void ShouldUpdateAppGit()
        //{
        //    List<object> objectArr = new List<object>();

        //    objectArr.Add(new Dictionary<string, object> { { "name", "file1" }, { "download_url", "file1url" } });
        //    objectArr.Add(new Dictionary<string, object> { { "name", "excludeme" } });

        //    var copyedFiles = new List<string>();

        //    var httpEngine = Mock.Create<IHttpEngine>();

        //    Mock.Arrange(() => httpEngine.GetHttpResponseObjectAsync(Arg.AnyString)).Returns(() => Task.Run(() => (object)objectArr.ToArray())).OccursOnce();

        //    Mock.Arrange(() => httpEngine.DownLoadFileAsync(Arg.AnyString, Arg.AnyString)).DoInstead((string url, string downloadFilePath) => { copyedFiles.Add(url); }).OccursOnce();

        //    var sut = new GitHubAppUpdater(httpEngine, new AppUpdateDetail(string.Empty, string.Empty, new[] { "excludeme" }));

        //    sut.UpdateApp();

        //    var foundKya = copyedFiles.FirstOrDefault(x => x.Equals("excludeme"));

        //    Assert.IsNull(foundKya);
        //    Assert.AreEqual(copyedFiles.Count, objectArr.Count - 1);
        //    Assert.AreEqual(copyedFiles.FirstOrDefault(), "file1url");
        //    Mock.AssertAll(httpEngine);
        //}

        //[Test]
        //public void ShouldThrowExceptionWhenSourceIsNotReachableGit()
        //{
        //    var httpEngine = Mock.Create<IHttpEngine>();

        //    Mock.Arrange(() => httpEngine.GetHttpResponseAsync(Arg.AnyString)).Throws(new AggregateException("Unit Test"));

        //    var sut = new GitHubAppUpdater(httpEngine, new AppUpdateDetail(string.Empty, string.Empty));

        //    Assert.Throws<LocationNotReachableException>(() => sut.GetLatestVesrion());
        //}
    }
}
