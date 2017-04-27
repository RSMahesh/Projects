using System.Collections.Generic;
using System.Threading.Tasks;

namespace MB.Core
{
    public interface IHttpEngine
    {
        string Authorization { set; }
        Task<bool> DownLoadFileAsync(string url, string downloadFilePath);
        Task<Dictionary<string, object>> GetHttpResponseAsync(string url);
        Task<object> GetHttpResponseObjectAsync(string url);
        Task<string> GetResponseStringAsync(string url);
    }
}