using AppUpdater.Host;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalTest
{
    [TestFixture]
    [Category("Functional")]
    public class UpdateApp
    {
        [Test]
        [TimeoutAttribute(1000 * 30)]
        public void UpdateTestAppWithIsUpdateRequired()
        {
            var dic = Path.GetDirectoryName(ConfigurationManager.AppSettings["TestExe"]);
            File.Delete(Path.Combine(dic, "Version.txt"));
            File.Delete(Path.Combine(dic, "output.txt"));

            EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset, "FunctionalTestAppUpdater");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = ConfigurationManager.AppSettings["TestExe"];
            Process.Start(startInfo);

            handle.WaitOne();

            Assert.IsTrue(File.Exists(Path.Combine(dic, "Version.txt")));
            var lines = File.ReadAllLines(Path.Combine(dic, "output.txt"));
            Assert.AreEqual(lines.LastOrDefault(), "Server");
        }

        [Test]
        public void UpdateTestAppWithOutIsUpdateRequired()
        {
            var dic = Path.GetDirectoryName(ConfigurationManager.AppSettings["TestExe"]);
            File.Delete(Path.Combine(dic, "Version.txt"));
            File.Delete(Path.Combine(dic, "output.txt"));

            EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset, "FunctionalTestAppUpdater");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.Arguments = "RunWithoutIsUpdateRequiredCheck";
            startInfo.FileName = ConfigurationManager.AppSettings["TestExe"];

            Process.Start(startInfo);
            handle.WaitOne();

            Assert.IsTrue(File.Exists(Path.Combine(dic, "Version.txt")));
            var lines = File.ReadAllLines(Path.Combine(dic, "output.txt"));
            Assert.AreEqual(lines.LastOrDefault(), "Server");
        }
    }
}
