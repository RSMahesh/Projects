using System;
using System.Data;
using System.Xml.XPath;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using WindowsFormsApplication3;
using System.Threading;
using NUnit.Extensions.Forms;

namespace StatusMaker.Business.Test
{
    [TestFixture]
    [Category("Integration ")]
    public class InProgessTests 
    {
        [SetUp]
        public void SetUp()
        {

        }

        [TestCase]
        [Description("should open file")]
        public void ShouldOpenFile()
        {
          

            var dc = new DesiredCapabilities();
            dc.SetCapability("app", @"D:\Projects\ContentWriter\WindowsFormsApplication3\bin\Debug\WindowsFormsApplication3.exe");
            var driver = new RemoteWebDriver(new Uri("http://localhost:9999"), dc);

            var window = driver.FindElementById("MDIParent1");
            var viewMenuItem = window.FindElement(By.Name("File"));

            viewMenuItem.Click();
            viewMenuItem.FindElement(By.Name("Open")).Click();

            var window2 = window.FindElement(By.Name("Open"));
            window2.SendKeys(@"D:\temp\Shally150.xlsx");
            window2.Submit();

            var form1 = window.FindElement(By.Name(@"D:\temp\Shally150.xlsx"));
            var data = form1.FindElement(By.Id(@"dataGridView1"));

            driver.Close();
        }


        [Test]

        public void FindReplaceTest()
        {
            Thread t = new Thread(FindReplaceTest12);
            t.SetApartmentState(ApartmentState.STA);

            t.Start();

            Thread.Sleep(TimeSpan.FromSeconds(10));
        }

        private void FindReplaceTest12()
        {
            Form1 frm = new Form1(@"D:\temp\Shally150.xlsx");
            frm.Show();
        }

        [Test]
        private void FindReplaceTest123()
        {
            Form1 frm = new Form1(@"D:\temp\Shally150.xlsx");
            frm.Show();
        }
    }
}
