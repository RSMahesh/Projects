using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;


namespace SettingUpCORS
{
    class Program
    {
        static void Main(string[] args)
        {
            var corRules = new CorsRule
            {
                AllowedHeaders = new List<string> {"x-ms-*", "content-type", "accept"},
                AllowedMethods = CorsHttpMethods.Put,
                AllowedOrigins =
                    new List<string> {"https://mbdevasia.azurewebsites.net",
                        "https://devasia.mediavalet.net", 
                        "https://mediavaletdevasia.azurewebsites.net",
                        "https://localhost", "https://localhost/MediaValet", 
                        "http://test-light.mediavalet.net:1337",
                        "https://mbdevasiaui2.azurewebsites.net"},
                MaxAgeInSeconds = 365*24*60*60
            };

            var account =
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=mediavaletdevasia;AccountKey=wtjIEIXRmtA6tHUW5zkhYwc1cCYhlFhsW8z2Cf3TUpKacrrnYWBaLUUrQDacfH3kQ3XhftEhVt3f2ONZQhCMog==");
            var client = account.CreateCloudBlobClient();
            var serviceProperties = client.GetServiceProperties();
            var corsSettings = serviceProperties.Cors;

            corsSettings.CorsRules.Clear();

            corsSettings.CorsRules.Add(corRules);
            client.SetServiceProperties(serviceProperties);
            Console.Read();


        }
    }
}
