using System.Collections.Generic;

namespace AppUpdater
{
    public interface IAppUpdaterFactory
    {
        IEnumerable<IAppUpdater> GetAppUpdaters(string updaterSettingsFile);
    }
}