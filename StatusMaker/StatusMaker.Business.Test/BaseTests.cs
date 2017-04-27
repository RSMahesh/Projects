using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

using NUnit.Framework;

using StatusMaker.Business.Sections;
using StatusMaker.Data;
using StatusMaker.UI;

using Telerik.JustMock;

namespace StatusMaker.Business.Test
{
    public abstract class BaseTests
    {
        protected IOLDBConnection _oldbConnection;

        protected IWindsorContainer _windsorContainer;

        protected XDocument _xdoc;

        protected DataRow _dataRow;

        protected void BaseSetUp(bool validate)
        {
            _oldbConnection = Mock.Create<IOLDBConnection>();

            _dataRow = this.GetDataRows();

            Mock.Arrange(() => _oldbConnection.GetInProgessRowsExcludingRegression(Arg.IsAny<DateTime>(), string.Empty))
             .Returns(new[] { _dataRow }).OccursOnce();

            _windsorContainer = new WindsorContainer();

            _windsorContainer.Register(Component.For<IOutPutGenerator>().ImplementedBy<XMLOutPutGenerator>());

            CastleWireUp.WireUp(_windsorContainer);
            InProgess inProgess = new InProgess(_windsorContainer.Resolve<ISectionHelper>(), _oldbConnection);

            var xml = inProgess.GetItmesAsHtml(DateTime.Now, string.Empty, validate);

            _xdoc = XDocument.Parse(xml);
        }


        public abstract DataRow GetDataRows();

        protected DataRow ConvertDataRows(
            string JIRA,
            string Description,
            string Priority,
            string Author,
            string Review,
            string Tester,
            string PR,
            string EPIC,
            string Status,
            string Comments,
            string category)
        {


            DataTable dt = new DataTable();
            dt.Columns.Add("JIRA #");
            dt.Columns.Add("Description");
            dt.Columns.Add("Priority");
            dt.Columns.Add("Author");
            dt.Columns.Add("Review");
            dt.Columns.Add("Tester");
            dt.Columns.Add("PR #");
            dt.Columns.Add("EPIC");
            dt.Columns.Add("Status");
            dt.Columns.Add("Comments");
            dt.Columns.Add("Category");


            var row = dt.NewRow();
            row["JIRA #"] = JIRA;
            row["Description"] = Description;
            row["Priority"] = Priority;
            row["Author"] = Author;
            row["Review"] = Review;
            row["Tester"] = Tester;
            row["PR #"] = PR;
            row["EPIC"] = EPIC;
            row["Status"] = Status;
            row["Comments"] = Comments;
            row["Category"] = category;

            return row;
        }

    }
}
