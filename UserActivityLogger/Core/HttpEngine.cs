using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Core
{
    public class HttpEngine : IHttpEngine
    {
        //This short of class only need intergation test cases not unit test case
        //as this is doing anything but calling external resource
        public string Authorization { private get; set; }

        public async Task<Dictionary<string, object>> GetHttpResponseAsync(string url)
        {
            return (Dictionary<string, object>)await GetHttpResponseObjectAsync(url);
        }

        public async Task<object> GetHttpResponseObjectAsync(string url)
        {
            var uri = new Uri(url);

            using (var wc = GetWebClient())
            {
                try
                {
                    var byteArray = await wc.DownloadDataTaskAsync(uri);

                    return new JavaScriptSerializer().Deserialize<object>(Encoding.UTF8.GetString(byteArray));
                }
                catch (WebException ex)
                {
                    throw new WebException("WebException uri: " + uri + " Message:" + ex.Message, ex);
                }
            }
        }

        public async Task<string> GetResponseStringAsync(string url)
        {
            var uri = new Uri(url);

            using (var wc = GetWebClient())
            {
                try
                {
                    return await wc.DownloadStringTaskAsync(uri);
                }
                catch (Exception ex)
                {

                    throw new WebException("WebException uri: " + uri + " Message:" + ex.Message, ex);
                }
            }
        }

        public async Task<bool> DownLoadFileAsync(string url, string downloadFilePath)
        {
            using (var wc = GetWebClient())
            {
                try
                {
                    wc.DownloadFile(new Uri(url), downloadFilePath);

                }
                catch (WebException ex)
                {
                    return false;
                }
            }
            return true;
        }

        //public static async Task<HttpResponseMessage> ExecutePutAsync<T>(UserType userType, string relativeUri, T postContet, TimeSpan timeSpan)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(HostUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();

        //        client.DefaultRequestHeaders.Authorization = DetermineToken(userType);
        //        MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
        //        HttpContent content = new ObjectContent<T>(postContet, jsonFormatter);
        //        client.Timeout = timeSpan;
        //        return await client.PutAsync(relativeUri, content);
        //    }
        //}
        private WebClient GetWebClient()
        {
            var wc = new WebClient();

            if (!string.IsNullOrEmpty(Authorization))
            {
                wc.Headers["Authorization"] = Authorization;
            }

            wc.Headers.Add("User-Agent", "Fiddler");

            return wc;


        }
    }
}
