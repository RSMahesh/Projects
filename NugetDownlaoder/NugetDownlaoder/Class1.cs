//using System;
//using System.Collections.Generic;
//using System.Linq;

//using NuGet;

//namespace NugetDownlaoder
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            UpdateAppConfig();
//            Dictionary<string, string> dic = new Dictionary<string, string>();

//            foreach (var item in dic)
//            {
//                if (!(item.Key.StartsWith("octopus.") || item.Key.StartsWith("env:")))
//                {
//                    System.IO.File.AppendAllText(
//                        @"C:\dic.txt",
//                        item.Key.ToLower() + " :" + item.Value + Environment.NewLine);
//                }
//            }



//            string packageID = "MediaValet.WebJobs.EntityChangeListener";

//            NuGet.IPackageRepository repo =
//                NuGet.PackageRepositoryFactory.Default.CreateRepository(
//                    "http://localhost:81/guestAuth/app/nuget/v1/FeedService.svc/");

//            var package = repo.FindPackagesById(packageID);
//            package.FirstOrDefault().ExtractContents(new PhysicalFileSystem(@"C:\webjob"), @"C:\webjob");
//        }



//        static void UpdateAppConfig()
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic["AccountName"] = "mediavaletdevasia12";

//            string appConfigFile = @"C:\webjob\MediaValet.WebJobs.EntityChangeListener.exe.config";

//            System.Xml.Linq.XElement xelement = System.Xml.Linq.XElement.Load(appConfigFile);

//            IEnumerable<System.Xml.Linq.XElement> elements = xelement.Elements().ToList();
//            // Read the entire XML
//            foreach (var element in elements)
//            {
//                if (element.Name.LocalName == "appSettings")
//                {
//                    var eles = element.Elements().ToList();

//                    foreach (var ele in eles)
//                    {
//                        if (dic.ContainsKey(ele.Attribute("key").Value))
//                        {
//                            ele.Attribute("value").Value = dic[ele.Attribute("key").Value];
//                        }
//                    }
//                }
//            }

//            var ww = new System.Xml.XmlTextWriter(appConfigFile, System.Text.Encoding.UTF8);
//            ww.Formatting = System.Xml.Formatting.Indented;
//            ww.WriteStartDocument();

//            xelement.WriteTo(ww);
//            ww.Flush();
//            ww.Close();


//        }
//    }
//}

