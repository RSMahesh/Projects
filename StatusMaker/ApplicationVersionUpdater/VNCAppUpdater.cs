using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationVersionUpdater
{
    public class VNCAppUpdater : IAppUpdater
    {
        private readonly AppUpdateDetail _vncAppUpdateDetail;
        public VNCAppUpdater(AppUpdateDetail vncAppUpdateDetail)
        {
            _vncAppUpdateDetail = vncAppUpdateDetail;
        }

        public void UpdateApp(IEnumerable<string> exludeFiles)
        {
            var sourceFiles = Directory.GetFiles(_vncAppUpdateDetail.Source);

            foreach (var file in sourceFiles)
            {
                var foundFile = exludeFiles.FirstOrDefault(x => x.Equals(Path.GetFileName(file), StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrEmpty(foundFile))
                {
                    var destinationFile = Path.Combine(_vncAppUpdateDetail.Destination, Path.GetFileName(file));

                    File.Copy(file, destinationFile, true);
                }
            }
        }

        public string GetLatestVesrion()
        {
            var versionFile = "Version.txt";
            var versionFilePath = Path.Combine(_vncAppUpdateDetail.Source, versionFile);

            try
            {
                return File.ReadAllText(versionFilePath);
            }
            catch (IOException ex)
            {
                throw new LocationNotReachable(versionFilePath, ex);
            }
        }
    }



}
