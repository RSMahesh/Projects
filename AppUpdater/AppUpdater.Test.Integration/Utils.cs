using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdater.Integration
{
    internal class Utils
    {
        internal static void CreateFolderWithFiles(string folder, int numberOfFiles = 0)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }

            Directory.CreateDirectory(folder);

            for (var i = 0; i < numberOfFiles; i++)
            {
                var filePath = Path.Combine(folder, Guid.NewGuid().ToString() + ".dll");

                File.WriteAllText(filePath, "Testing File");
            }
        }
    }
}
