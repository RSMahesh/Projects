using System;


using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var feedUrl = "http://localhost:81/guestAuth/app/nuget/v1/FeedService.svc/";
            var packageID = "MediaValet.WebJobs.EntityChangeListener";
            var version = "1.0.0.43";
            var extractionFolder = @"C:\webjob";
            var nugetDownloaderExe = @"D:\Projects\NugetDownlaoder\NugetDownlaoder\bin\Debug\NugetDownlaoder.exe";

            var arguments = feedUrl + " " + packageID + " " + version + " " + extractionFolder;
            System.Diagnostics.Process.Start(nugetDownloaderExe, arguments);
        }
    }
}
