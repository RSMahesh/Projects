using System;
using System.Data;
using System.Xml.Linq;
using System.Xml.XPath;

using NUnit.Framework;

using StatusMaker.Business.Columns;

namespace StatusMaker.Business.Test
{
    [TestFixture]
    [Category("Integration ")]
    public class InProgessTests : BaseTests
    {
#pragma warning disable CS0618 // 'TestFixtureSetUpAttribute' is obsolete: 'Use OneTimeSetUpAttribute'
        [TestFixtureSetUp]
#pragma warning restore CS0618 // 'TestFixtureSetUpAttribute' is obsolete: 'Use OneTimeSetUpAttribute'
        public void SetUp()
        {
            BaseSetUp(false);
        }

        [Test]
        public void JiraNumberShouldDisplayWithJiraLink()
        {
            Assert.IsTrue(
                GetValue("/SECTION/JIRA/TEXTWITHJIRALINK")
                    .Equals(GetFieldValue(ColumnTypes.JiraNumber), StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void DiscriptionShouldDisplay()
        {
            Assert.IsTrue(
                GetValue("/SECTION/Description")
                    .Equals(GetFieldValue(ColumnTypes.Description), StringComparison.OrdinalIgnoreCase));
        }

        public override DataRow GetDataRows()
        {
            return ConvertDataRows(
                  "DO-417",
                  "[MVTool] - Add Arm Support for the MV Tool tool.",
                  "High",
                  "Hari",
                  "Mahesh",
                  "Shruti",
                  "677",
                  "epic--MV-7382--MVTool]_-_Add_Arm_Support_for_the_MV_Tool_tool",
                  "In Progress",
                  "No By me",
                  "In Progress");
        }

        private string GetValue(string xpath)
        {
            return _xdoc.XPathSelectElement(xpath).Value;
        }

        private string GetFieldValue(string field)
        {
            return _dataRow[field].ToString();
        }
    }
}
