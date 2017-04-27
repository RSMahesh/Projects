using MB.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationVersionUpdater
{
    public class GitHubAppUpdater : IAppUpdater
    {
        private readonly AppUpdateDetail _appUpdateDetail;
        const string DownloadUrl = "download_url";
        public GitHubAppUpdater(AppUpdateDetail appUpdateDetail)
        {
            _appUpdateDetail = appUpdateDetail;
        }

        public string GetLatestVesrion()
        {
            var versionFile = "Version.txt";
            var versionFilePath = _appUpdateDetail.Source + "/" + versionFile;
            return GetLatestVesrionFrormRemote(versionFilePath);
        }

        public void UpdateApp(IEnumerable<string> exludeFiles)
        {
            var httpEngine = new HttpEngine();
            var arr = (object[])httpEngine.GetHttpResponseObjectAsync(_appUpdateDetail.Source).Result;

            foreach (var file in arr)
            {
                var dic = file as Dictionary<string, object>;
                var foundFile = exludeFiles.FirstOrDefault(x => x.Equals(dic["name"].ToString(), StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrEmpty(foundFile))
                {
                    httpEngine.DownLoadFileAsync(dic[DownloadUrl].ToString(), Path.Combine(_appUpdateDetail.Destination, dic["name"].ToString()));
                }
            }
        }

        private void DownLoadFile(string url, string downloadFilePath)
        {
            using (var wc = new WebClient())
            {
                var uri = new Uri(url);

                wc.DownloadFile(uri, downloadFilePath);
            }
        }

        private string GetLatestVesrionFrormRemote(string url)
        {
            try
            {
                var httpEngine = new HttpEngine();
                return httpEngine.GetResponseStringAsync(httpEngine.GetHttpResponseAsync(url).Result[DownloadUrl].ToString()).Result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
}
