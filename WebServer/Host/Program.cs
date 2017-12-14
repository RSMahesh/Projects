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
        static string startPage = string.Empty;
        static CommandLineParser _commandLineParser;

        static void Main(string[] args)
        {
            _commandLineParser = new CommandLineParser(args);

            var rootPath = "http://10.131.60.58:" + GetPort() + "/";

            var webServer = new WebServer(new string[] { rootPath }, GetRootDirectory());

            webServer.StartServer();

            Console.WriteLine("A simple webserver at " + ConfigurationManager.AppSettings["port"] + ". Press a key to quit.");

            OpenDefaultPage(rootPath);

            Console.ReadKey();

            webServer.StopServer();
        }
        static void OpenDefaultPage(string rootPath)
        {
            Console.WriteLine("Root Path:" + rootPath);
            Process.Start(rootPath + "index.html");
        }

        private static string GetPort()
        {
            var port = _commandLineParser.GetParameterValue("-port");

            if (string.IsNullOrEmpty(port) && ConfigurationManager.AppSettings["port"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["port"]))
            {
                port = ConfigurationManager.AppSettings["port"];
            }


            if (string.IsNullOrEmpty(port))
            {
                port = "8180";
            }

            return port;
        }

        private static string GetRootDirectory()
        {
            var rootDir = _commandLineParser.GetParameterValue("-rootDir");

            if (string.IsNullOrEmpty(rootDir) && ConfigurationManager.AppSettings["rootDir"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["rootDir"]))
            {
                rootDir = ConfigurationManager.AppSettings["rootDir"];
            }


            if (string.IsNullOrEmpty(rootDir))
            {
                rootDir = Directory.GetParent(
                        System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
                        .ToString();
            }

            return rootDir;
        }
    }
}
