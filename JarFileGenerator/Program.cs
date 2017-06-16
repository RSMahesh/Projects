using Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarFileGenerator
{
    class Program
    {
        static void Main12(string[] args)
        {
            var dir = Path.Combine(RuntimeHelper.ExecutionLocation, "Jar");
            var jarFilePath = Path.Combine(dir, "Include.jar");

            if(File.Exists(jarFilePath))
            {
                File.Delete(jarFilePath);
            }


            JarFileFactory jarFactory = new JarFileFactory();

            var files = Directory.GetFiles(dir);
            Dictionary<string, string> header = new Dictionary<string, string>();

            using (var jarFileWriter = jarFactory.GetJarFileWriter(jarFilePath))
            {
                foreach (var file in files)
                {
                    header["FileName"] = Path.GetFileName(file);
                    var fvi = FileVersionInfo.GetVersionInfo(file);
                    header["version"] = fvi.FileVersion;

                    jarFileWriter.AddFile(new FileSystem.JarFileItem(header, file));
                }
            }
        }

        static void Main(string[] args)
        {
            var dir = Path.Combine(RuntimeHelper.ExecutionLocation, "Jar");
            var jarFilePath = Path.Combine(dir, "Include.jar");

            JarFileFactory jarFactory = new JarFileFactory();

            var files = Directory.GetFiles(dir);
            Dictionary<string, string> header = new Dictionary<string, string>();

            using (var jarFileReader = jarFactory.GetJarFileReader(jarFilePath))
            {
                foreach (var file in files)
                {
                    header["FileName"] = Path.GetFileName(file);
                    var fvi = FileVersionInfo.GetVersionInfo(file);
                    header["version"] = fvi.FileVersion;

                    var jarFileItem = jarFileReader.GetNextFile();

                }
            }
        }
    }
}
