using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WebServer.Host
{
    class Program
    {
        static string webSitePhysicalPath;
        static string rootUrl = "http://localhost:";
        static string startPage = string.Empty;


        static void Main(string[] args)
        {
            rootUrl += ConfigurationManager.AppSettings["port"] + "/";

            GetWebSiteRootPath(args);

            WebServer.Interface.IWebServer webServer = GetWebServerInstant();

            webServer.StartServer();

            Console.WriteLine("A simple webserver at " + ConfigurationManager.AppSettings["port"] + ". Press a key to quit.");

            OpenDefaultPage();

            Console.ReadKey();

            webServer.StopServer();
        }

       static void GetWebSiteRootPath(string[] args)
        {

            if (args != null && args.Count() > 0)
            {
                webSitePhysicalPath = args[0];

                if (args.Count() > 1)
                {
                    startPage = args[1];
                }
            }
            else
            {
                webSitePhysicalPath =
                    Directory.GetParent(
                        System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
                        .ToString();
            }
        }


       static void OpenDefaultPage()
        {
            Console.WriteLine("Root Path:" + rootUrl);

           //To Do: need to implement later
            //if (startPage != string.Empty)
            //{
            //    Process.Start(rootUrl + startPage);
            //}

            if (File.Exists(rootUrl + "index.htm"))
            {
                Process.Start(rootUrl + "index.htm");
            }
            else
            {
                Process.Start(rootUrl + "index.html");
            }
        }

        static WebServer.Interface.IWebServer GetWebServerInstant()
        {
            return new SimpleWebServer.WebServer(new string[] { rootUrl }, webSitePhysicalPath);
        }
    }
}
