using System;

using NUnit.Framework;

using WindowsFormsApplication3;
using System.Runtime.InteropServices;
using EventPublisher;
using System.Threading;
using System.Text;

namespace TestingApp
{
    [TestFixture]
    [Category("Integration ")]

    public class MainFormTest
    {
        Form1 mainForm;

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        public const int BM_CLICK = 0x00F5;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern long GetClassName(IntPtr hwnd, StringBuilder lpClassName, long nMaxCount);

        public MainFormTest(Form1 mainForm)
        {
            this.mainForm = mainForm;
        }


        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void FindReplaceTest()
        {
            IntPtr handle12 = FindWindowByCaption(IntPtr.Zero, @"D:\temp\Shally150.xlsx");

            Assert.AreEqual(mainForm.GridView.Rows.Count, 150);

            EventContainer.PublishEvent
  (EventPublisher.Events.FindWindow.ToString(), new EventArg(Guid.NewGuid(), null));
            //            EventContainer.PublishEvent
            //(EventPublisher.Events.FindText.ToString(), new EventArg(Guid.NewGuid(), new[] { "The", "ALL" }));


            IntPtr handle = FindWindowByCaption(IntPtr.Zero, @"FindText");
          //  Thread.Sleep(TimeSpan.FromSeconds(4));

            IntPtr ButtonHandle = FindWindowEx(handle, IntPtr.Zero, "Button", null);

        }

        public void CheckWindow()
        {
            IntPtr handle = FindWindowByCaption(IntPtr.Zero, @"FindText");
            IntPtr ButtonHandle = FindWindowEx(handle, IntPtr.Zero, null, "Find Next");
           var className = GetClassNameOfWindow(ButtonHandle);
            SendMessage(ButtonHandle, BM_CLICK, IntPtr.Zero, IntPtr.Zero);

        }

        string GetClassNameOfWindow(IntPtr hwnd)
        {
            string className = "";
            StringBuilder classText = null;
            try
            {
                int cls_max_length = 1000;
                classText = new StringBuilder("", cls_max_length + 5);
                GetClassName(hwnd, classText, cls_max_length + 2);

                if (!String.IsNullOrEmpty(classText.ToString()) && !String.IsNullOrWhiteSpace(classText.ToString()))
                    className = classText.ToString();
            }
            catch (Exception ex)
            {
                className = ex.Message;
            }
            finally
            {
                classText = null;
            }
            return className;
        }
        public void FindReplaceTest123()
        {
            IntPtr handle12 = FindWindowByCaption(IntPtr.Zero, @"D:\temp\Shally150.xlsx");
            // IntPtr handle = FindWindowByCaption(handle12, @"FindText");

            var handle = FindWindowEx(handle12, IntPtr.Zero, null, "FindText");

            IntPtr ButtonHandle = FindWindowEx(handle, IntPtr.Zero, "Button", "Replace");

            Console.WriteLine(handle.ToString());
            Assert.AreEqual(mainForm.GridView.Rows.Count, 150);
            EventContainer.PublishEvent
(EventPublisher.Events.FindWindow.ToString(), new EventArg(Guid.NewGuid(), null));
        }

        private void FindReplaceTest12()
        {
            Form1 frm = new Form1(@"D:\temp\Shally150.xlsx");
            frm.Show();
        }

        [Test]
        private void FindReplaceTest12hhh3()
        {
            Form1 frm = new Form1(@"D:\temp\Shally150.xlsx");
            frm.Show();
        }
    }
}
