using AppUpdater;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Telerik.JustMock;

namespace ApplicationVersionUpdater.Test
{
    [TestFixture]
    [Category("Unit")]
    public class AppUdaterExecuterFixture
    {
        //[Test]
        //public void Test()
        //{
        //    var json = File.ReadAllText(@"D:\Projects\AppUpdater\AppUpdater\Settings.json");
        //    var serializer = new JavaScriptSerializer();
        //    var data = serializer.Deserialize<IEnumerable<UpdaterSetting>>(json);
        //    data = data.OrderBy(x => x.Sequence).ToList();
        //}

        [Test]

        public void ShouldCallUpdateIfServersioVersionNewer()
        {
            var appUpdater = Mock.Create<IAppUpdater>();

            Mock.Arrange(() => appUpdater.GetLatestVesrion()).Returns(() => "1.0.0.13").OccursOnce();

            Mock.Arrange(() => appUpdater.UpdateApp()).OccursOnce();

            var sut = new AppUdaterExecuter("1.0.0.12", new[] { appUpdater });

            sut.Execute();

            Mock.AssertAll(appUpdater);
        }

        [Test]

        public void ShouldNotCallUpdateIfServersioVersionNotNew()
        {
            var appUpdater = Mock.Create<IAppUpdater>();

            Mock.Arrange(() => appUpdater.GetLatestVesrion()).Returns(() => "1.0.0.12").OccursOnce();

            Mock.Arrange(() => appUpdater.UpdateApp()).OccursNever();

            var sut = new AppUdaterExecuter("1.0.0.12", new[] { appUpdater });

            sut.Execute();

            Mock.AssertAll(appUpdater);
        }

        [Test]
        public void ShouldCallUpdateOnNextIfNotRechable()
        {
            // DebugView.IsTraceEnabled = true;

            var appUpdater1 = Mock.Create<IAppUpdater>();
            Mock.Arrange(() => appUpdater1.GetLatestVesrion()).Throws(new LocationNotReachableException("Unit Test", null)).OccursOnce();

            var appUpdater2 = Mock.Create<IAppUpdater>(Behavior.CallOriginal);
            Mock.Arrange(() => appUpdater2.GetLatestVesrion()).Returns(() => "1.0.0.13").OccursOnce();

            Mock.Arrange(() => appUpdater1.UpdateApp()).OccursNever();


            var appUpdater2Called = string.Empty;

            Mock.Arrange(() => appUpdater2.UpdateApp()).DoInstead(() => appUpdater2Called = "appUpdater2Called").OccursOnce();

            var sut = new AppUdaterExecuter("1.0.0.12", new[] { appUpdater1, appUpdater2 });

            sut.Execute();

            Mock.Assert(appUpdater1);

            //Somehow justmock only  Mock.Assert work as expected for 
            //appUpdater1 (which is arragned first UpdateApp()) so Mock.Assert(appUpdater2) is not used
            // This need more research
            Assert.AreEqual(appUpdater2Called, "appUpdater2Called");
        }

        [Test]
        public void ShouldNotCallUpdateOnNextIfRechable()
        {
            var appUpdaterCalled = string.Empty;

            var appUpdater1 = Mock.Create<IAppUpdater>();
            Mock.Arrange(() => appUpdater1.GetLatestVesrion()).Returns(() => "1.0.0.13").OccursOnce();
            Mock.Arrange(() => appUpdater1.UpdateApp()).DoInstead(() => appUpdaterCalled = "appUpdater1Called").OccursOnce();

            var appUpdater2 = Mock.Create<IAppUpdater>(Behavior.CallOriginal);
            Mock.Arrange(() => appUpdater2.GetLatestVesrion()).Returns(() => "1.0.0.13").OccursNever();
            Mock.Arrange(() => appUpdater2.UpdateApp()).DoInstead(() => appUpdaterCalled = "appUpdater2Called").OccursNever();

            var sut = new AppUdaterExecuter("1.0.0.12", new[] { appUpdater1, appUpdater2 });

            sut.Execute();

            Mock.Assert(appUpdater1);
            Mock.Assert(appUpdater2);

            Assert.AreEqual(appUpdaterCalled, "appUpdater1Called");
        }

        [Test]
        public void ShouldThrowExceptionIfAllUpdaterFails()
        {
            var appUpdater1 = Mock.Create<IAppUpdater>();
            Mock.Arrange(() => appUpdater1.GetLatestVesrion()).Throws(new LocationNotReachableException("Unit Test", null)).OccursOnce();

            var appUpdater2 = Mock.Create<IAppUpdater>(Behavior.CallOriginal);
            Mock.Arrange(() => appUpdater2.GetLatestVesrion()).Throws(new LocationNotReachableException("Unit Test", null)).OccursOnce();

            var sut = new AppUdaterExecuter(string.Empty, new[] { appUpdater1, appUpdater2 });

            Assert.Throws<AppUpdatersFailedException>(() => sut.Execute());

            Mock.Assert(appUpdater1);
            Mock.Assert(appUpdater2);
        }
    }
}
