using Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserActivityLogger;

namespace Host.Tests
{
    [TestFixture]
    [Category("Functional")]
    
    public class ProgramTests
    {
        string exe = @"D:\Projects\UserActivityLogger\Host\bin\Debug\SysHealth.exe";
      
        [Test]
        public void TestActivityLogger()
        {
            var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dataFolder = Path.Combine(dataFolder, "SysLogs");

            if (Directory.Exists(dataFolder))
            {
                Directory.Delete(dataFolder, true);
            }
        
            ProcessHelper.KillProcess(Path.GetFileNameWithoutExtension(exe));

            ProcessHelper.RunHidden(exe);

            Assert.IsTrue(ProcessHelper.GetProcess(exe).Any());

            KeyPressSimulater keyPresSimulator = new KeyPressSimulater();

            Thread.Sleep(2 * 1000);

            keyPresSimulator.PressKeyH();
            Thread.Sleep(3 * 1000);

            keyPresSimulator.PressKey(Keys.S);
            keyPresSimulator.PressKey(Keys.M);
            keyPresSimulator.PressKey(Keys.I);
            keyPresSimulator.PressKey(Keys.L);
            keyPresSimulator.PressKey(Keys.E);
            Thread.Sleep(3 * 1000);

            Assert.GreaterOrEqual(Directory.GetFiles(dataFolder).Count(), 1);

            ActivityRepositary repo = new ActivityRepositary(new JarFileFactory(), new ImageCommentEmbedder());

            var enumerator = repo.GetReader(Directory.GetFiles(dataFolder));

            var lst = enumerator.ToList();

            KeyProcessor processor = new KeyProcessor();

            Assert.True(lst.Count() == 3);

            var procesedKeys = lst.FirstOrDefault().KeyPressedData;
            Assert.True( procesedKeys.Equals("Process Started"));


            procesedKeys = processor.ProcessKeys(lst[1].KeyPressedData).ProcessedData;
            Assert.True(procesedKeys.Equals("h"));


            procesedKeys = processor.ProcessKeys(lst.LastOrDefault().KeyPressedData).ProcessedData;
            Assert.True(procesedKeys.Equals("smile"));
        }

        [TearDown]
        public void TearDown()
        {
            ProcessHelper.KillProcess(Path.GetFileNameWithoutExtension(exe));
        }
    }
}
