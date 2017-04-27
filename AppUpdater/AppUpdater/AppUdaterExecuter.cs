using AppUpdater.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace AppUpdater
{
    public class AppUdaterExecuter
    {
        private readonly IEnumerable<IAppUpdater> _appUdaters;
        private readonly string _currentVersion;
        public AppUdaterExecuter(string currentVersion, IEnumerable<IAppUpdater> appUdaters)
        {
            _appUdaters = appUdaters;
            _currentVersion = currentVersion;
        }

        public void Execute()
        {
            var exceptions = new List<LocationNotReachableException>();
            var appUpdated = false;

            foreach (var appUpdater in _appUdaters)
            {
                try
                {
                    if (IsServerVersionNewer(_currentVersion, appUpdater.GetLatestVesrion()))
                    {
                        appUpdater.UpdateApp();
                    }

                    appUpdated = true;
                    break;
                }
                catch (LocationNotReachableException ex)
                {
                    exceptions.Add(ex);
                    // if there is exception then try to update with next one app updater.        
                }
            }

            if (!appUpdated)
            {
                throw new AppUpdatersFailedException(exceptions);
            }
        }


        public bool IsUpdateRequired()
        {
            foreach (var appUpdater in _appUdaters)
            {
                try
                {
                    if (IsServerVersionNewer(_currentVersion, appUpdater.GetLatestVesrion()))
                    {
                        return true;
                    }

                    return false;
                }
                catch (LocationNotReachableException ex)
                {
                    // if there is exception then try to update with next one app updater.        
                }
            }

            return false;
        }

        bool IsServerVersionNewer(string localVersionNumber, string serverVersionNumber)
        {
            if (string.IsNullOrEmpty(localVersionNumber))
            {
                return true;
            }

            var localVersion = new Version(localVersionNumber);
            var serverVersion = new Version(serverVersionNumber);

            return serverVersion > localVersion;
        }
    }
}
