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
    public class ProcessHelperTests
    {

        string runForeverExe = @"D:\Projects\UserActivityLogger\Core.Tests\RunForeEver.exe";

        [Test]
        public void ShouldRunInForeground()
        {
            ProcessHelper.Run(runForeverExe);
            var gotIt = ProcessHelper.GetForegroundProcess().FirstOrDefault(x => x.Equals(Path.GetFileNameWithoutExtension(runForeverExe )));
            Assert.IsTrue(!string.IsNullOrEmpty(gotIt));
        }

        [Test]
        public void ShouldRunInBackground()
        {
            ProcessHelper.RunAsBackGround(runForeverExe);
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
