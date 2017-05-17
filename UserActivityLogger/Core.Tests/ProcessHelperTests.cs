using Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger.Tests
{
    [TestFixture]

    [Category("Functional")]
    public class ProcessHelperTests
    {

        string runForeverExe = @"D:\Projects\UserActivityLogger\Core.Tests\RunForeEver.exe";

        [Test]
        public void ShouldRunInForeground()
        {
            ProcessHelper.Run(@"D:\Projects\UserActivityLogger\Core\bin\Debug\Core.exe");

            var gotIt = ProcessHelper.GetForegroundProcess().FirstOrDefault(x => x.Equals(Path.GetFileNameWithoutExtension(runForeverExe )));
            Assert.IsTrue(!string.IsNullOrEmpty(gotIt));
        }

        [Test]
        public void tt()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["lock1"] = Guid.NewGuid().ToString();
            dic["lock2"] = Guid.NewGuid().ToString();

            ProcessHelper.Run(@"D:\Projects\UserActivityLogger\Core\bin\Debug\Core.exe",dic);

          //  var gotIt = ProcessHelper.GetForegroundProcess().FirstOrDefault(x => x.Equals(Path.GetFileNameWithoutExtension(runForeverExe)));
            //Assert.IsTrue(!string.IsNullOrEmpty(gotIt));
        }


        [Test]
        public void ShouldRunInBackground()
        {
            ProcessHelper.RunHidden(runForeverExe);
            var gotIt = ProcessHelper.GetForegroundProcess().FirstOrDefault(x => x.Equals(Path.GetFileNameWithoutExtension(runForeverExe)));
            Assert.IsTrue(string.IsNullOrEmpty(gotIt));
        }


        [TearDown]
        public void CleanUp()
        {
            ProcessHelper.KillProcess(runForeverExe);
        }
    }
}
