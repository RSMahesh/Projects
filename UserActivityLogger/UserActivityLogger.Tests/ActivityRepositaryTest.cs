using Core;
using FileSystem;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telerik.JustMock;

namespace UserActivityLogger.Tests
{
    [TestFixture]
    [Category("Unit")]
    public class ActivityRepositaryTest
    {
        string imageFile = @"D:\Projects\UserActivityLogger\UserActivityLogger.Tests\One.jpg";
        Dictionary<string, List<string>> appendedFilesStore;

        [Test]
        public void ShouldCreateTwoLogFilesIfFilesAreGreaterThen100()
        {
            appendedFilesStore = new Dictionary<string, List<string>>();

            var jarFactory = Mock.Create<IJarFileFactory>();

            Mock.Arrange(() => jarFactory.GetJarFileWriter(Arg.AnyString)).Returns((string dataFile) => Factory(dataFile));

            var imageCommentEmbedder = Mock.Create<IImageCommentEmbedder>();

            var activityRepositary = new ActivityRepositary(jarFactory, imageCommentEmbedder);

            var img = Image.FromFile(imageFile);

            for (var ind = 0; ind < 190; ind++)
            {
                Activity activity = new Activity(img, string.Empty);
                activityRepositary.Add(activity);
            }

            Assert.AreEqual(appendedFilesStore.Count(), 2);
            Assert.AreEqual(100, appendedFilesStore[appendedFilesStore.Keys.FirstOrDefault()].Count());
            Assert.AreEqual(90, appendedFilesStore[appendedFilesStore.Keys.LastOrDefault()].Count());
            Assert.IsTrue(appendedFilesStore.Keys.FirstOrDefault().Contains(GetUserNameInReverse()));
        }

        private IJarFileWriter Factory(string dataFile)
        {
            var jarFileWriter = Mock.Create<IJarFileWriter>();

            //we could live with out mock here but  I am learing using this complex mocking

            Mock.Arrange(() => jarFileWriter.AddFile(Arg.IsAny<JarFileItem>()))
                                .DoInstead((JarFileItem jarFileItem) => AppendFile(jarFileItem, dataFile, appendedFilesStore)).OccursAtLeast(100);

         //   Mock.Arrange(() => jarFile.FilesCount).Returns(() => GetFileCount(dataFile, appendedFilesStore));

            return jarFileWriter;
        }

        int GetFileCount(string dataFile, Dictionary<string, List<string>> appendedFilesStore)
        {
            if (!appendedFilesStore.ContainsKey(dataFile))
            {
                return 0;
            }

            return appendedFilesStore[dataFile].Count();
        }

        void AppendFile(JarFileItem jarFileItem, string dataFile, Dictionary<string, List<string>> appendedFilesStore)
        {
            if (!appendedFilesStore.ContainsKey(dataFile))
            {
                appendedFilesStore[dataFile] = new List<string>();
            }

            var list = appendedFilesStore[dataFile];

            if (list.Count >=100)
            {
                throw new JarFileReachedMaxLimitException();
            }

            list.Add(jarFileItem.Headers.ToString());
        }

        private string GetUserNameInReverse()
        {
            var userFullName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').LastOrDefault();
            return new string(userFullName.Reverse().ToArray());
        }
    }
}
