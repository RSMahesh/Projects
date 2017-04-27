using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.Core
{
    //Implemeting IFileGateway (for GitHub) this way would be breaking LSP principle
    //HOW : Because we were only implemetnting pull functionalty in Copy method for GitHub 
    //implemantation of IFileGateway (from GitHub to local), where else IFileGateway Copy funtion should allow
    //file to copyed either ways. i.e from source to destination and destination to source. 
    //So I am not inherting GitHub from IFileGateway.
    //public class GitHub : IFileGateway
    public class GitHub : IGitHub
    {
        private readonly IHttpEngine _httpEngine;
        const string DownloadUrl = "download_url";

        public GitHub(IHttpEngine httpEngine)
        {
            _httpEngine = httpEngine;
        }
        public void PullFolderServer(string serverUrlPath, string destiantionFolder, IEnumerable<string> excludeFiles)
        {
            var arr = (object[])_httpEngine.GetHttpResponseObjectAsync(serverUrlPath).Result;

            foreach (var file in arr)
            {
                var dic = file as Dictionary<string, object>;

                var foundFile = excludeFiles.FirstOrDefault(x => x.Equals(dic["name"].ToString(), StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrEmpty(foundFile))
                {
                    _httpEngine.DownLoadFileAsync(dic[DownloadUrl].ToString(), Path.Combine(destiantionFolder, dic["name"].ToString()));
                }
            }
        }

        public void ReadAllText(object cofigurationManager)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetFiles(string path)
        {
            List<string> files = new List<string>();
            var arr = (object[])_httpEngine.GetHttpResponseObjectAsync(path).Result;

            foreach (var file in arr)
            {
                var dic = file as Dictionary<string, object>;

                files.Add(dic["url"].ToString());
            }

            return files;
        }

        public string ReadAllText(string path)
        {
            var result = _httpEngine.GetHttpResponseAsync(path).Result;

            return _httpEngine.GetResponseStringAsync(result[DownloadUrl].ToString()).Result;
        }

        public async Task<string> ReadAllTextAysnc(string path)
        {
            var result = await _httpEngine.GetHttpResponseAsync(path);

            return await _httpEngine.GetResponseStringAsync(result[DownloadUrl].ToString());
        }
    }
}
