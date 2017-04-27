using AppUpdater.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdater
{
    public class VNCAppUpdater : IAppUpdater
    {
        private readonly IFileGateway _fileGateway;
        public VNCAppUpdater(AppUpdateDetail appUpdateDetail, IFileGateway file)
        {
            AppUpdateDetail = appUpdateDetail;
            _fileGateway = file;
        }

        public AppUpdateDetail AppUpdateDetail { get; private set; }

        public void UpdateApp()
        {
            var sourceFiles = _fileGateway.GetFiles(AppUpdateDetail.RemoteSource);

            foreach (var file in sourceFiles)
            {
                var foundFile = AppUpdateDetail.ExcludeFiles.FirstOrDefault(x => x.Equals(Path.GetFileName(file), StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrEmpty(foundFile))
                {
                    var destinationFile = Path.Combine(AppUpdateDetail.LocalDestination, Path.GetFileName(file));

                    _fileGateway.Copy(file, destinationFile);
                }
            }
        }

        public string GetLatestVesrion()
        {
            var versionFile = "Version.txt";
            var versionFilePath = Path.Combine(AppUpdateDetail.RemoteSource, versionFile);

            try
            {
                return _fileGateway.ReadAllText(versionFilePath);
            }
            catch (IOException ex)
            {
                throw new LocationNotReachableException(versionFilePath, ex);
            }
        }
    }
}
