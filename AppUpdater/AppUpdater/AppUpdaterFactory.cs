using AppUpdater.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AppUpdater
{
    public class AppUpdaterFactory : IAppUpdaterFactory
    {
        string _updaterSettingsJason;
        public IEnumerable<IAppUpdater> GetAppUpdaters(string updaterSettingsJason)
        {
            _updaterSettingsJason = updaterSettingsJason;

            var updaterList = new List<IAppUpdater>();

            var settings = ReadSettings();

            foreach (var setting in settings)
            {
                updaterList.Add(GetAppUpdaterInstance(setting));
            }

            return updaterList;
        }

        private IEnumerable<UpdaterSetting> ReadSettings()
        {
            var serializer = new JavaScriptSerializer();
            var data = serializer.Deserialize<IEnumerable<UpdaterSetting>>(_updaterSettingsJason);
            return data.OrderBy(x => x.Sequence).ToList();
        }

        private IAppUpdater GetAppUpdaterInstance(UpdaterSetting setting)
        {
            var appUpdateDetail = new AppUpdateDetail(setting.Location, RuntimeHelper.ExecutionLocation, new[] { RuntimeHelper.ExecutionAssemblyName });

            switch (setting.Name)
            {
                case "AppUpdater.VNCAppUpdater":
                    return new VNCAppUpdater(appUpdateDetail, new LocalFileGateway());
                case "AppUpdater.GitHubAppUpdater":
                    return new GitHubAppUpdater(new GitHub(new HttpEngine()), appUpdateDetail);
                default:
                    throw new Exception("Not able to create updater for :" + setting.Name);
            }
        }
    }
}
