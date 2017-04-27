using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MB.Core
{
    public interface IGitHub
    {
        void PullFolderServer(string serverUrlPath, string destiantionFolder, IEnumerable<string> excludeFiles);
        IEnumerable<string> GetFiles(string path);
       Task<string> ReadAllTextAysnc(string path);
    }
}