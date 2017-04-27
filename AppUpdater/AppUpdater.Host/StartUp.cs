using AppUpdater.Core;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace AppUpdater.Host
{
    public class StartUp
    {
        enum RuleCondition
        {
            EualTo=1,
            GreaterThan,
            LesserThan,
            Contains
        }

        const string defaultSettingFileOnGit = "settingFile";
        SettingFilePRovider settingFilePRovider = new SettingFilePRovider();
        string[] _args;
        public StartUp()
        {
            new EmbeddedAssemblyLoader().Register();
            settingFilePRovider.SettingFilePathOnGitHub = DefaultSettingFileOnGit;
        }

        public void StartUpdate(string[] args)
        {
            _args = args;

            settingFilePRovider.SettingFilePathOnGitHub = GetParameterValue(defaultSettingFileOnGit);

            var setting = settingFilePRovider.GetUpdaterSettingsJason();
            var appToUpdateExeName = RuntimeHelper.GetParentProcessName();

            RuntimeHelper.KillAllProcess(appToUpdateExeName);
            var app = new AppUdaterExecuter(GetCurrentVersion(), new AppUpdaterFactory().GetAppUpdaters(setting));
            app.Execute();

            Process.Start(RuntimeHelper.MapToCurrentLocation(appToUpdateExeName));
        }


        public void LaunchProcess()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = RuntimeHelper.MapToCurrentLocation("Updater.exe");

            startInfo.Arguments += StartUp.defaultSettingFileOnGit + "=\"" + DefaultSettingFileOnGit + "\"";

            Process.Start(startInfo);
        }

        public bool IsUpdateRequired()
        {
            var setting = settingFilePRovider.GetUpdaterSettingsJason();
            var app = new AppUdaterExecuter(GetCurrentVersion(), new AppUpdaterFactory().GetAppUpdaters(setting));
            return app.IsUpdateRequired();
        }

        private string GetCurrentVersion()
        {
            var localVersionFile = RuntimeHelper.MapToCurrentLocation(Constants.VersionFileName);

            var currentVersion = string.Empty;

            if (File.Exists(localVersionFile))
            {
                currentVersion = File.ReadAllText(localVersionFile);
            }

            return currentVersion;
        }

        private string GetParameterValue(string parameterName)
        {
            if (_args != null)
            {
                foreach (var arg in _args)
                {
                    var arr = arg.Split('=');

                    if (arr[0].Equals(parameterName, StringComparison.OrdinalIgnoreCase))
                    {
                        return arr[1];
                    }
                }
            }

            return string.Empty;
        }

        private string DefaultSettingFileOnGit
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["DefaultSettingFileOnGit"]))
                    return string.Empty;

                return ConfigurationManager.AppSettings["DefaultSettingFileOnGit"];
            }
        }
    }
}
