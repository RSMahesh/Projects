using MB.Core;
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
    public class GitHubFixture
    {
        [Test]
        public void ShouldCallHttpEngineMethodsForReadAllText()
        {
            var httpEngine = Mock.Create<IHttpEngine>();

            var dic = new Dictionary<string, object> { { "download_url", "urlfortest" } };

            Mock.Arrange(() => httpEngine.GetHttpResponseAsync(Arg.AnyString)).Returns(() => Task.Run(() => dic)).OccursOnce();
            Mock.Arrange(() => httpEngine.GetResponseStringAsync(Arg.AnyString)).Returns(() => Task.Run(() => "1.0.0.11")).OccursOnce();

            var sut = new GitHub(httpEngine);
            var latestVerion = sut.ReadAllText(string.Empty);
            Assert.AreEqual("1.0.0.11", latestVerion);

            Mock.AssertAll(httpEngine);
        }

        [Test]
        public void ShouldUpdateAppGit()
        {
            const string  excludefile = "excludeFile";

            List<object> objectArr = new List<object>();

            objectArr.Add(new Dictionary<string, object> { { "name", "file1" }, { "download_url", "file1url" } });
            objectArr.Add(new Dictionary<string, object> { { "name", excludefile } });

            var copyedFiles = new List<string>();

            var httpEngine = Mock.Create<IHttpEngine>();

            Mock.Arrange(() => httpEngine.GetHttpResponseObjectAsync(Arg.AnyString)).Returns(() => Task.Run(() => (object)objectArr.ToArray())).OccursOnce();

            Mock.Arrange(() => httpEngine.DownLoadFileAsync(Arg.AnyString, Arg.AnyString)).DoInstead((string url, string downloadFilePath) => { copyedFiles.Add(url); }).OccursOnce();

            var sut = new GitHub(httpEngine);

            sut.PullFolderServer(string.Empty, string.Empty, new[] { excludefile });
          
            var foundKya = copyedFiles.FirstOrDefault(x => x.Equals(excludefile));

            Assert.IsNull(foundKya);
            Assert.AreEqual(copyedFiles.Count, objectArr.Count - 1);
            Assert.AreEqual(copyedFiles.FirstOrDefault(), "file1url");
            Mock.AssertAll(httpEngine);
        }
    }
}
