using NUnit.Framework;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using UserActivityLogger;

namespace KeyBoardEventsListener.Tests
{
    //Note: Test Case started failing restaring VS helped.
    //Note : Test Case may fail when ran along with Functional test cases.
    [TestFixture]
    [Category("Unit")]
    public class KeyLoggerTests
    {
        KeyLogger keyLogger = new KeyLogger();
        KeyProcessor processor = new KeyProcessor();

        //=============================================================================

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public const int VK_RCONTROL = 0xA3; //Right Control key code
        protected void PressKeyH()
        {
            PressKey(Keys.H);
        }

        protected void PressKey(Keys key)
        {
            keybd_event((byte)key, 0, 0, 0);
            Thread.Sleep(100);
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
        }
        protected void PressShiftDown()
        {
            keybd_event((int)Keys.LShiftKey, 0, 0, 0);
        }
        protected void PressShiftUp()
        {
            keybd_event((int)Keys.LShiftKey, 0, KEYEVENTF_KEYUP, 0);
        }
        protected void ToggleCapLocks()
        {
            keybd_event((int)Keys.CapsLock, 0, 0, 0);
            Thread.Sleep(100);
            keybd_event((int)Keys.CapsLock, 0, KEYEVENTF_KEYUP, 0);
        }

        //================================================================================

        [TestFixtureSetUp]
        public void Init()
        {
            keyLogger.StartListening();
        }
        

        [TearDown]
        public void TeradOwn()
        {
            keyLogger.CleanBuffer();
        }

        [Test]
        public void ShouldLogH()
        {
            PressKeyH();

            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());

            Assert.AreEqual("h", keysRecevied);
        }

        [Test]
        public void ShouldLog1()
        {
            PressKey(Keys.D1);

            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());

            Assert.AreEqual("1", keysRecevied);
        }

        [Test]
        public void ShouldLogExclamation()
        {
            PressShiftDown();
            PressKey(Keys.D1);
            PressShiftUp();

            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("!", keysRecevied);
        }

        [Test]
        public void ShouldCleanBuffer()
        {
            PressKeyH();
            PressKeyH();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("hh", keysRecevied);

            keyLogger.CleanBuffer();
            PressKeyH();
            keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("h", keysRecevied);
        }

        [Test]
        public void WhenShiftIsPresedLogCapitalH()
        {
            PressShiftDown();
            PressKeyH();
            PressShiftUp();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("H", keysRecevied);
        }

        [Test]
        public void WhenCapsLockCapitalH()
        {
            ToggleCapLocks();
            PressKeyH();
            ToggleCapLocks();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("H", keysRecevied);
        }

        [Test]
        public void WhenCapsLockWithShiftKeyLogh()
        {
            ToggleCapLocks();
            PressShiftDown();
            PressKeyH();
            PressShiftUp();
            ToggleCapLocks();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("h", keysRecevied);
        }

        [Test]
        public void SpaceShouldLog()
        {
            PressKeyH();
            PressKey(Keys.Space);
            PressKeyH();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());
            Assert.AreEqual("h h", keysRecevied);
        }

        [Test]
        public void FunctionKeysShouldNotLog()
        {
            PressKey(Keys.F2);
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("", keysRecevied);
        }

        [Test]
        public void FunctionKeysShouldNotLogWithShift()
        {
            PressShiftDown();
            PressKey(Keys.F2);
            PressShiftUp();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("", keysRecevied);
        }

        [Test]
        public void ShouldLogSingleQuotes()
        {
            PressKey(Keys.OemQuotes);
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());

            Assert.AreEqual("'", keysRecevied);
        }

        [Test]
        public void ShouldLogPipe()
        {
            PressShiftDown();
            PressKey(Keys.OemPipe);
            PressShiftUp();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());

            Assert.AreEqual("|", keysRecevied);
        }


        [Test]
        public void EnterShlouldLogNewLine()
        {
            PressKey(Keys.Enter);
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());

            Assert.AreEqual(Environment.NewLine, keysRecevied);
        }

        public void TestMe()
        {
            var omeCharaKey = File.ReadAllLines(@"C:\temp\keys.txt");
            var omeCharavalue = File.ReadAllLines(@"C:\temp\values.txt");

            int indx = 0;
            var dic = string.Empty;

            foreach (var key in omeCharaKey)
            {
                var val = string.Empty;
                while (true)
                {
                    val = omeCharavalue[indx];
                    indx++;
                    if (!string.IsNullOrEmpty(val))
                    {
                        break;
                    }
                }

                dic += string.Format(" openBraces \"{0}\", '{1}' closeBraces,", key, val) + Environment.NewLine;
            }

            dic = dic.Replace("openBraces", "{").Replace("closeBraces", "}");
        }

    }
}


