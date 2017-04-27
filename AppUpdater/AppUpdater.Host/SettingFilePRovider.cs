using AppUpdater.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AppUpdater.Host
{
    public class SettingFilePRovider
    {
        const string appUpdateSettingMappingFilePath = "https://api.github.com/repos/MaheshBailwal/AppUpdateInformation/contents/AppUpdateSettingMapping.json";
        public string SettingFilePathOnGitHub { get; set; }
        public string GetUpdaterSettingsJason()
        {
            var LocalUpdaterSettingsFile = Path.Combine(RuntimeHelper.ExecutionLocation, Constants.UpdaterSettingsFile);

            if (File.Exists(Path.Combine(LocalUpdaterSettingsFile)))
            {
                return File.ReadAllText(LocalUpdaterSettingsFile);
            }

            return GetSetttingFileFromGitHub();
        }
        private  string GetSetttingFileFromGitHub()
        {

            if (string.IsNullOrEmpty(SettingFilePathOnGitHub))
            {
                SettingFilePathOnGitHub = GetSettingFilePathFromGitHub();
            }

            GitHub gitHub = new GitHub(new HttpEngine());
            return gitHub.ReadAllText(SettingFilePathOnGitHub);
        }

        private string GetSettingFilePathFromGitHub()
        {
            GitHub gitHub = new GitHub(new HttpEngine());
            var json = gitHub.ReadAllText(appUpdateSettingMappingFilePath);

            var serializer = new JavaScriptSerializer();
            var lst = serializer.Deserialize<IEnumerable<AppUpdateSettingMapping>>(json);

            var appToUpdateName = Path.GetFileNameWithoutExtension(RuntimeHelper.GetParentProcessName());

            foreach (var mapping in lst)
            {
                if (mapping.AppName.Equals(appToUpdateName, StringComparison.OrdinalIgnoreCase))
                {
                    return mapping.SettingFile;
                }
            }

            throw new Exception("GetSettingFilePathFromGitHub Failed");
        }
    }
}
