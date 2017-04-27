using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

using NuGet;

namespace NugetDownlaoder
{
    class Program
    {

        private static string logFile;

        static void Main(string[] args)
        {

            var extractedFolder = @"C:\Deployment\ExtractedNuget";

            if (!Directory.Exists(extractedFolder))
            {
                Directory.CreateDirectory(extractedFolder);
            }

            logFile = Path.Combine(extractedFolder, GetLogFileName());

            GetLatestNugetPackge(
                "http://10.131.60.128:81/guestAuth/app/nuget/v1/FeedService.svc/",
                "MediaValet.WebJobs.EntityChangeListener",
                extractedFolder);

            GetLatestNugetPackge(
    "http://10.131.60.128:81/guestAuth/app/nuget/v1/FeedService.svc/",
    "MediaValet.Api.IISHost",
   extractedFolder);

        }


        static void GetLatestNugetPackge(string feedUrl, string packageID, string extractionFolder)
        {

            NuGet.IPackageRepository repo =
                NuGet.PackageRepositoryFactory.Default.CreateRepository(
                    feedUrl);


            var package = repo.FindPackage(packageID);

            File.AppendAllText(logFile, DateTime.Now.ToLongDateString() + "  " + string.Format("PackageID:{0} Version:{1}", packageID, package.Version) + Environment.NewLine);

            extractionFolder = Path.Combine(extractionFolder, packageID, package.Version.ToString());

            if (!Directory.Exists(extractionFolder))
            {
                Directory.CreateDirectory(extractionFolder);
            }

            package.ExtractContents(new PhysicalFileSystem(extractionFolder), extractionFolder);

            DeleteFiles(extractionFolder, "*.exe.config");
            DeleteFiles(extractionFolder, "Web.config");

            File.AppendAllText(logFile, "  Nuget Copleted");
        }

        private static void DeleteFiles(string extractionFolder, string serachPatteren)
        {
            var files = Directory.GetFiles(extractionFolder, serachPatteren);
            if (files.Any())
            {
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }

        }

        static void GetSpecficPackage(string[] args)
        {
            string feedUrl;
            string packageID;
            string version;
            string extractionFolder;


            feedUrl = "http://localhost:81/guestAuth/app/nuget/v1/FeedService.svc/";
            packageID = "MediaValet.WebJobs.EntityChangeListener";
            version = "1.0.0.43";
            extractionFolder = @"C:\webjob";

            if (args.Any())
            {
                File.AppendAllText(@"C:\args.txt", string.Join(",", args));

                feedUrl = args[0];
                packageID = args[1];
                version = args[2];
                extractionFolder = args[3];
            }

            if (!Directory.Exists(extractionFolder))
            {
                Directory.CreateDirectory(extractionFolder);
            }


            NuGet.IPackageRepository repo =
                NuGet.PackageRepositoryFactory.Default.CreateRepository(
                    feedUrl);


            var packages = repo.FindPackagesById(packageID);

            var package =
                packages
                    .Where(x => x.Version == SemanticVersion.Parse(version))
                    .FirstOrDefault();
            package.ExtractContents(new PhysicalFileSystem(extractionFolder), extractionFolder);

            File.AppendAllText(@"C:\args.txt", "Nuget Copleted");

        }



        static void UpdateAppConfig()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["AccountName"] = "mediavaletdevasia12";

            string appConfigFile = @"C:\webjob\MediaValet.WebJobs.EntityChangeListener.exe.config";

            System.Xml.Linq.XElement xelement = System.Xml.Linq.XElement.Load(appConfigFile);

            IEnumerable<System.Xml.Linq.XElement> elements = xelement.Elements().ToList();
            // Read the entire XML
            foreach (var element in elements)
            {
                if (element.Name.LocalName == "appSettings")
                {
                    var eles = element.Elements().ToList();

                    foreach (var ele in eles)
                    {
                        if (dic.ContainsKey(ele.Attribute("key").Value))
                        {
                            ele.Attribute("value").Value = dic[ele.Attribute("key").Value];
                        }
                    }
                }
            }

            var ww = new System.Xml.XmlTextWriter(appConfigFile, System.Text.Encoding.UTF8);
            ww.Formatting = System.Xml.Formatting.Indented;
            ww.WriteStartDocument();

            xelement.WriteTo(ww);
            ww.Flush();
            ww.Close();

        }

        private static string GetLogFileName()
        {
            return "Log-" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt";
        }
    }
}

