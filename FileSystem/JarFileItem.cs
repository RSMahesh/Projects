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
            : this(headers, filePath, null)
        {
        }

        public JarFileItem(Dictionary<string, string> headers, string filePath, byte[] containt)
        {
            //To DO: Ensure 
            this.Headers = headers;
            this.FilePath = filePath;
            this.Containt = containt;
        }

        public Dictionary<string, string> Headers { get; private set; }

        public string FilePath { get; private set; }

        public byte[] Containt { get; private set; }
    }
}
