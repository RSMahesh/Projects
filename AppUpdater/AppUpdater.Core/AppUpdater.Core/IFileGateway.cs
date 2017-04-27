using System.Collections.Generic;

namespace AppUpdater.Core
{
    public interface IFileGateway
    {
        void Copy(string sourceFileName, string destFileName);
        string ReadAllText(string path);
        IEnumerable<string> GetFiles(string path);
    }
}