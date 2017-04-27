using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatusMaker.Business
{
    public interface IJira
    {
        Task<string> IsValidEpicNumberAsync(string jiraNumber, string epic);
        Task<string> IsValidJiraStatusAsync(string jiraNumber, string status);
        Task<IEnumerable<string>> GetAllValidPullsAsync(string jiraNumber);
        Task<string> GetDescriptionAsync(string jiraNumber);
    }
}