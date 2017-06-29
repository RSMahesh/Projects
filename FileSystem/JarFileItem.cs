using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    public class JarFileItem
    {
        public JarFileItem(Dictionary<string, string> headers, string filePath)
            : this(headers, filePath, null, -1)
        {
        }

        public JarFileItem(Dictionary<string, string> headers, string filePath, byte[] containt, long offSetInJarFile)
        {
            //To DO: Ensure 
            this.Headers = headers;
            this.FilePath = filePath;
            this.Containt = containt;
            OffSetInJarFile = offSetInJarFile;
        }

        public Dictionary<string, string> Headers { get; private set; }

        public string FilePath { get; private set; }

        public byte[] Containt { get; private set; }

        public long OffSetInJarFile { get; private set; }
    }
}
