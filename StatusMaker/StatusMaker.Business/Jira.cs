
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;


namespace StatusMaker.Business
{
    public class Jira : IJira
    {
        const string rootUrl = "https://mediavalet.atlassian.net/rest";
        const string authorization = "Basic " + "bWFoZXNoLmJhaWx3YWw6bWIyNDgwMDE=";
        readonly IHttpEngine _httpEngine = new HttpEngine();

        public Jira(IHttpEngine httpEngine)
        {
            _httpEngine = httpEngine;
            _httpEngine.Authorization = authorization;
        }

        public async Task<string> IsValidEpicNumberAsync(string jiraNumber, string epic)
        {
            const string EpicNumberCustomField = "customfield_10007";
            const string Parent = "parent";
            var issueInformation = await GetIsuueInformation(jiraNumber);

            var fields = (Dictionary<string, object>)issueInformation["fields"];

            if (fields.ContainsKey(EpicNumberCustomField) && fields[EpicNumberCustomField] != null)
            {
                var epicNumber = fields[EpicNumberCustomField].ToString();
                return epic.ToLowerInvariant().Contains(epicNumber.ToLowerInvariant()) ? string.Empty : epicNumber;
            }

            if (!fields.ContainsKey(Parent) || fields[Parent] == null)
            {
                return string.Empty;
            }

            var parentIssue = ((Dictionary<string, object>)fields[Parent])["key"].ToString();

            return IsValidEpicNumberAsync(parentIssue, epic).Result;
            //fields["description"]
        }

        public async Task<string> GetDescriptionAsync(string jiraNumber)
        {
            //var file = "3__Http_"+jiraNumber+ "_" + Guid.NewGuid().ToString() + ".mb";
            //File.AppendAllText(file, Environment.NewLine + "GetIsuueInformation Start : " + jiraNumber);
            var issueInformation = await GetIsuueInformation(jiraNumber).ConfigureAwait(false);
            var fields = (Dictionary<string, object>)issueInformation["fields"];
            //File.AppendAllText(file, Environment.NewLine + "GetDescriptionAsync end : " + jiraNumber);
            //File.AppendAllText(file, Environment.NewLine + "Description : " + fields["summary"].ToString());
            //File.Move(file, file + "-done.mb");
            return fields["summary"].ToString();
        }

        public async Task<string> IsValidJiraStatusAsync(string jiraNumber, string status)
        {
            var issueInformation = await GetIsuueInformation(jiraNumber);

            var fields = (Dictionary<string, object>)issueInformation["fields"];
            var statusInfo = (Dictionary<string, object>)fields["status"];
            var name = statusInfo["name"].ToString();

            return name.Equals(status, StringComparison.OrdinalIgnoreCase) ? string.Empty : name;
        }

        public async Task<IEnumerable<string>> GetAllValidPullsAsync(string jiraNumber)
        {

            var pullNumbersFromServer = await GetAllPullRequestFromServerAsync(jiraNumber);

            var tt = (from Dictionary<string, object> dicValues in pullNumbersFromServer
                      where dicValues["status"].ToString() != "DECLINED"
                      select dicValues["id"].ToString().Trim('#'));
            return tt;
        }



        private async Task<IEnumerable<object>> GetAllPullRequestFromServerAsync(string issueNo)
        {
            var issueInformation = await GetIsuueInformation(issueNo);

            var dic = await
                _httpEngine.GetHttpResponseAsync(
                    string.Format(
                     rootUrl + "/dev-status/1.0/issue/detail?issueId={0}&applicationType=github&dataType=pullrequest&_=1460441522916",
                        issueInformation["id"]));

            var detail = (Dictionary<string, object>)((object[])dic["detail"])[0];

            return (object[])(detail["pullRequests"]);
        }

        private async Task<Dictionary<string, object>> GetIsuueInformation(string issueNo)
        {
            var IssueInformationCache = this.GetCacheIisuesInformation();

            if (IssueInformationCache.ContainsKey(issueNo))
            {
                return IssueInformationCache[issueNo];
            }

            var url = rootUrl + "/api/latest/issue/" + issueNo;
            //var file = "2__Http" + Guid.NewGuid().ToString() + ".mb";
            //File.AppendAllText(file, "Calling GetHttpResponseAsync  : " + url + Environment.NewLine);
            var issueInfo = await this._httpEngine.GetHttpResponseAsync(
               url).ConfigureAwait(false);
            //File.AppendAllText(file, "Got From GetHttpResponseAsync  : " + url + Environment.NewLine);
            //File.Move(file, file + "-done.mb");
            //File.Delete(file + "-done.mb");

            lock (IssueInformationCache)
            {
                IssueInformationCache[issueNo] = issueInfo;
            }

            return IssueInformationCache[issueNo];
        }

        private Dictionary<string, Dictionary<string, object>> GetCacheIisuesInformation()
        {
            //TO DO : Clear before get status
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains("JiraIsuueInformation"))
            {
                return cache["JiraIsuueInformation"] as Dictionary<string, Dictionary<string, object>>;
            }

            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) };

            var dic = new Dictionary<string, Dictionary<string, object>>();
            cache.Add("JiraIsuueInformation", dic, policy);

            return dic;
        }
    }
}