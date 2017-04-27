using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using MediaProcessor.ServiceLibrary.Common;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllText("C:\\jason.txt");
            var resp = HttpHelper.PostUrl(
                    "https://localhost/public/mbdevasiaui2.azurewebsites.net/users?runAsync=true",
                    "POST","",
                    content);


            //using (var wc = new System.Net.WebClient())
            //{
            //    wc.Headers["Authorization"] = "Basic bWFoZXNoLmJhaWx3YWw6ZmlzZXJ2QDEyOA==";
            //    var uri = new Uri("http://rsin-svnsr.india.rsystems.com/svn/Media-Valet/SDLC/Release/Status/Progress%20Tracking.xlsx");

            //    wc.DownloadFile(uri, @"C:\pp.xlsx");
            //}

        }
    }
}
