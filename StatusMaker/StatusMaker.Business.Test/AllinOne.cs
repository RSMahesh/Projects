using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using Castle.Windsor;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

using StatusMaker.UI;

namespace StatusMaker.Business.Test
{
    [TestFixture]
    [Category("Functional")]
    public class AllinOne
    {
        private IWebDriver driver;

#pragma warning disable CS0618 // 'TestFixtureSetUpAttribute' is obsolete: 'Use OneTimeSetUpAttribute'
        [TestFixtureSetUp]
#pragma warning restore CS0618 // 'TestFixtureSetUpAttribute' is obsolete: 'Use OneTimeSetUpAttribute'
        public void SetUp()
        {
            string excelDownloadFilePath =
       Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Progress Tracking.xlsx");

            var statusDate = DateTime.Parse("10/13/2016");

            var testExcelFilePath = ConfigurationManager.AppSettings["ExcelDownloadFilePath"];

            if (File.Exists(excelDownloadFilePath))
            {
                File.Delete(excelDownloadFilePath);
            }

            File.Copy(testExcelFilePath, excelDownloadFilePath);

            IWindsorContainer windsorContainer = new WindsorContainer();

            CastleWireUp.WireUp(windsorContainer);

            var statusGenerator = windsorContainer.Resolve<IStatusGenerator>();

            var statusHTML = statusGenerator.GenerateStatusForSingleDay(statusDate, string.Empty, true);

            var htmlFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "statusHtml.html");

            File.WriteAllText(htmlFilePath, statusHTML);

            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("file://" + htmlFilePath);

        }

        [TestCase("In Progress:")]
        [TestCase("Regression:")]
        [TestCase("Merged To Epic:")]
        [TestCase("AD-Hoc:")]
        public void ShouldContainAllSection(string section)
        {
            var elements = driver.FindElements(By.TagName("span"));
            var spanSection = elements.FirstOrDefault(x => x.Text == section);
            Assert.NotNull(spanSection);

        }


        [TestCase("Regression:", 
            "MV-7439,[Regression] [Release] 'LastModifiedDatetime' with seconds gives incorrect search results,High,[Regression] [Release] 'LastModifiedDatetime' with seconds gives incorrect search results,epic--MV-7189--Extend_modifiedAt_filter_to_support_seconds,Being Coded")]
        [TestCase("In Progress:", "DO-417,[MVTool] - Add Arm Support for the MV Tool tool.,High,A:Hari R:Mahesh T:Shruti,677,In Progress,No Comments")]
        [TestCase("Merged To Epic:", " MV-6936,API permissions warning should not be exposed in the current fashion,Medium,Hari Reviewer Missing,Ready For Test,epic--MV-6936--API_permissions_warning_should_not_be_exposed_in_the_current_fashion")]
        [TestCase("AD-Hoc:", "Ankur")]
        public void ShouldContainInProgessSection(string section, string expectedCellsText)
        {

            var elements = driver.FindElements(By.TagName("span"));
            var spanSection = elements.FirstOrDefault(x => x.Text == section);
            var parent = spanSection.FindElement(By.XPath(".."));
            var table = parent.FindElement(By.XPath("following-sibling::*"));

            var tanleElements = table.FindElements(By.TagName("span"));

            var expectedCellsTextArray = expectedCellsText.Split(',');

            foreach (var cellText in expectedCellsTextArray)
            {
                var span = tanleElements.FirstOrDefault(x => x.Text.Replace("&nbsp;", "").Trim() == cellText.Trim());
                Assert.NotNull(span);
            }
        }
        
#pragma warning disable CS0618 // 'TestFixtureTearDownAttribute' is obsolete: 'Use OneTimeTearDownAttribute'
        [TestFixtureTearDown]
#pragma warning restore CS0618 // 'TestFixtureTearDownAttribute' is obsolete: 'Use OneTimeTearDownAttribute'
        public void TearDown()
        {
            driver.Quit();
        }

    }
}
