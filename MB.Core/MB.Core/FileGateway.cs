using System.Collections;
using System.Collections.Generic;
using System.IO;
using IOFile = System.IO.File;

namespace MB.Core
{
    public class LocalFileGateway : IFileGateway
    {
        public void Copy(string sourceFileName, string destFileName)
        {
            IOFile.Copy(sourceFileName, destFileName, true);
        }
        public string ReadAllText(string path)
        {
            return IOFile.ReadAllText(path);
        }
        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}
