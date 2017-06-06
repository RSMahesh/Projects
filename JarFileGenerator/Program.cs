using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarFileGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
           var dir = RuntimeHelper.ExecutionLocation;
           var jarFilePath = RuntimeHelper.MapToCurrentExecutionLocation("Include.jar");

            JarFileFactory jarFactory = new JarFileFactory();
            var jarFile = jarFactory.GetJarFile(FileSystem.FileAccessMode.Write, jarFilePath);

            var files = Directory.GetFiles(dir);
            Dictionary<string, string> header = new Dictionary<string, string>();

            foreach(var file in files)
            {
                header["FileName"] = Path.GetFileName(file);
               // jarFile.
            }
        }
    }
}
