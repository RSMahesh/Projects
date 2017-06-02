using Core;
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
        KeyPressSimulater _keySimulator = new KeyPressSimulater();
        protected void PressKey(Keys key)
        {
            _keySimulator.PressKey(key);
        }

        protected void PressKeyH()
        {
            PressKey(Keys.H);
        }

        protected void PressShiftDown()
        {
            _keySimulator.PressShiftDown();
        }
        protected void PressShiftUp()
        {
            _keySimulator.PressShiftUp();
        }
        protected void ToggleCapLocks()
        {
            _keySimulator.ToggleCapLocks();
        }

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

            Assert.AreEqual("h", keysRecevied.ProcessedData);
        }

        [Test]
        public void ShouldLog1()
        {
            PressKey(Keys.D1);

            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());

            Assert.AreEqual("1", keysRecevied.ProcessedData);
        }

        [Test]
        public void ShouldLogExclamation()
        {
            PressShiftDown();
            PressKey(Keys.D1);
            PressShiftUp();

            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("!", keysRecevied.ProcessedData);
        }

        [Test]
        public void ShouldCleanBuffer()
        {
            PressKeyH();
            PressKeyH();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("hh", keysRecevied.ProcessedData);

            keyLogger.CleanBuffer();
            PressKeyH();
            keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("h", keysRecevied.ProcessedData);
        }

        [Test]
        public void WhenShiftIsPresedLogCapitalH()
        {
            PressShiftDown();
            PressKeyH();
            PressShiftUp();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("H", keysRecevied.ProcessedData);
        }

        [Test]
        public void WhenCapsLockCapitalH()
        {
            ToggleCapLocks();
            PressKeyH();
            ToggleCapLocks();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("H", keysRecevied.ProcessedData);
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


            Assert.AreEqual("h", keysRecevied.ProcessedData);
        }

        [Test]
        public void SpaceShouldLog()
        {
            PressKeyH();
            PressKey(Keys.Space);
            PressKeyH();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());
            Assert.AreEqual("h h", keysRecevied.ProcessedData);
        }

        [Test]
        public void FunctionKeysShouldNotLog()
        {
            PressKey(Keys.F2);
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("", keysRecevied.ProcessedData);
        }

        [Test]
        public void FunctionKeysShouldNotLogWithShift()
        {
            PressShiftDown();
            PressKey(Keys.F2);
            PressShiftUp();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());


            Assert.AreEqual("", keysRecevied.ProcessedData);
        }

        [Test]
        public void ShouldLogSingleQuotes()
        {
            PressKey(Keys.OemQuotes);
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());

            Assert.AreEqual("'", keysRecevied.ProcessedData);
        }

        [Test]
        public void ShouldLogPipe()
        {
            PressShiftDown();
            PressKey(Keys.OemPipe);
            PressShiftUp();
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());

            Assert.AreEqual("|", keysRecevied.ProcessedData);
        }


        [Test]
        public void EnterShlouldLogNewLine()
        {
            PressKey(Keys.Enter);
            var keysRecevied = processor.ProcessKeys(keyLogger.GetKeys());

            Assert.AreEqual(Environment.NewLine, keysRecevied.ProcessedData);
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


