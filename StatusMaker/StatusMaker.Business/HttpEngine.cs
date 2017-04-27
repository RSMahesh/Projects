using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace StatusMaker.Business
{
    public class HttpEngine : IHttpEngine
    {
        //This short of class only need intergation test cases not unit test case
        //as this is doing anything but calling external resource

        //bool _configureAwait;
        //public HttpEngine(string configureAwait)
        //{
        //    _configureAwait = bool.Parse(configureAwait);
        //}
        public string Authorization { private get; set; }

        public async Task<Dictionary<string, object>> GetHttpResponseAsync(string url)
        {
            //var file = "1__Http" + Guid.NewGuid().ToString() + ".mb";
            //File.AppendAllText(file, "Calling GetHttpResponseObjectAsync_  : " + url + Environment.NewLine);
            var response = (Dictionary<string, object>)await GetHttpResponseObjectAsync(url).ConfigureAwait(false);
            //File.AppendAllText(file, "Got Response From GetHttpResponseObjectAsync_  : " + url + Environment.NewLine);
            //File.Move(file, file + "-done.mb");
            //File.Delete(file + "-done.mb");
            return response;
        }
        public async Task<object> GetHttpResponseObjectAsync(string url)
        {
            var uri = new Uri(url);


            using (var hc = GetHttpClient())
            {
                try
                {
                    //var file = "Http" + Guid.NewGuid().ToString() + ".mb";
                    //File.AppendAllText(file, "Getting : " + url + Environment.NewLine);
                    var byteArray = await hc.GetByteArrayAsync(uri).ConfigureAwait(false);
                    //File.AppendAllText(file, "Done : " + url + Environment.NewLine);
                    //File.Move(file, file + "-done.mb");
                    //File.Delete(file + "-done.mb");
                    var response = new JavaScriptSerializer().Deserialize<object>(Encoding.UTF8.GetString(byteArray));
                    return response;
                }
                catch (Exception ex)
                {
                    throw new WebException("WebException uri: " + uri + " Message:" + ex.Message, ex);
                }
            }
        }
        public async Task<string> GetResponseStringAsync(string url)
        {
            var uri = new Uri(url);

            using (var wc = GetHttpClient())
            {
                try
                {
                    return await wc.GetStringAsync(uri).ConfigureAwait(false);
                }
                catch (Exception ex)
                {

                    throw new WebException("WebException uri: " + uri + " Message:" + ex.Message, ex);
                }
            }
        }

        public async Task<bool> DownLoadFileAsync(string url, string downloadFilePath)
        {
            using (var wc = GetHttpClient())
            {
                try
                {
                    var stream = await wc.GetStreamAsync(new Uri(url)).ConfigureAwait(false);
                    using (var fileStream = File.Create(downloadFilePath))
                    {
                        stream.CopyTo(fileStream);
                    }

                }
                catch (WebException ex)
                {
                    return false;
                }
            }
            return true;
        }

        private HttpClient GetHttpClient()
        {
            var hc = new HttpClient();

            hc.Timeout = TimeSpan.FromSeconds(15);

            if (!string.IsNullOrEmpty(Authorization))
            {
                hc.DefaultRequestHeaders.Add("Authorization", Authorization);
            }

            hc.DefaultRequestHeaders.Add("User-Agent", "MB");

            return hc;
        }
    }
}
