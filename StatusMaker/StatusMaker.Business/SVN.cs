using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using SharpSvn;
using System.Windows.Forms;
using System.Web;

namespace StatusMaker.Business
{
    public class SVN : ISVN
    {
        private string _excelFilePath;

        private readonly string _svnPathOfTrackingSheet;

        private readonly string _excelDownloadFilePath;

        private readonly IHttpEngine _httpEngine;

        public SVN(string excelFilePath, string excelDownloadFilePath, string trackingSheetPath, IHttpEngine httpEngine)
        {
            _excelFilePath = excelFilePath;
            _svnPathOfTrackingSheet = trackingSheetPath;
            _excelDownloadFilePath = excelDownloadFilePath;
            _httpEngine = httpEngine;
        }

        private IEnumerable<SvnFileVersionEventArgs> reversedFileversion = null;

        public string ExcelFilePath
        {
            set
            {
                _excelFilePath = value;
            }

        }
        public bool GetLatestAndLock()
        {
            using (SvnClient client = new SvnClient())
            {
                if (IsAlreadLockedByMe(client))
                {
                    return true;
                }

                File.Delete(_excelFilePath);
                client.Update(_excelFilePath);
                client.Lock(_excelFilePath, new SvnLockArgs());
            }
            return true;
        }

        public bool GetLockForceFully()
        {
            using (SvnClient client = new SvnClient())
            {
                File.Delete(_excelFilePath);
                client.Update(_excelFilePath);
                var args = new SvnLockArgs();
                args.StealLock = true;
                client.Lock(_excelFilePath, args);
            }

            return true;
        }

        public void GetLatestOfDay(DateTime dt, string checkOutDir)
        {

            GetHistory();

            var version = reversedFileversion.FirstOrDefault(x => x.Time.Date == dt.Date);

            if (version == null) return;

            using (SvnClient client = new SvnClient())
            {
                client.Update(_excelFilePath);
                Uri uri = new Uri("http://rsin-svnsr.india.rsystems.com/svn/Media-Valet/SDLC/Release/Status/");

                var svnCheckOutArgs = new SvnCheckOutArgs();
                svnCheckOutArgs.Revision = version.Revision;

                if (File.Exists(Path.Combine(checkOutDir, "Progress Tracking.xlsx")))
                {
                    File.Delete(Path.Combine(checkOutDir, "Progress Tracking.xlsx"));
                }
                client.CheckOut(uri, checkOutDir, svnCheckOutArgs);

            }

        }

        private void GetHistory()
        {
            if (reversedFileversion != null) return;

            using (SvnClient client = new SvnClient())
            {
                client.CleanUp(Path.GetDirectoryName(_excelFilePath));

                Collection<SvnFileVersionEventArgs> fileVersions = new Collection<SvnFileVersionEventArgs>();
                var tt = new SvnFileVersionsArgs();
                tt.Start = new SvnRevision(DateTime.Now.AddDays(-1));
                tt.End = new SvnRevision(DateTime.Now);

                client.GetFileVersions(
                    new SvnPathTarget(_excelFilePath, DateTime.Now),
                    new SvnFileVersionsArgs(),
                    out fileVersions);

                reversedFileversion = fileVersions.Reverse();
                reversedFileversion = reversedFileversion.OrderByDescending(x => x.Revision);

            }
        }

        public void CheckIn()
        {
            SvnCommitArgs a = new SvnCommitArgs();
            a.LogMessage = "Status Update";

            using (SvnClient client = new SvnClient())
            {
                client.Commit(_excelFilePath, a);
                try
                {
                    client.Unlock(_excelFilePath);
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (SharpSvn.SvnClientMissingLockTokenException ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                }

            }
        }

        public bool DownLoadLatestProgressTrackingSheet(string userName, string password)
        {
            try
            {
                _httpEngine.Authorization = "Basic " + Base64Encode(userName + ":" + password);
                return _httpEngine.DownLoadFileAsync(
                    _svnPathOfTrackingSheet,
                    _excelDownloadFilePath).Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsValidUser(string userName, string password)
        {
            try
            {
                var url = "http://10.131.40.102/IsValidSvnUser.aspx?user=" + Base64Encode(userName + ":" + password)
                          + "&ip=" + IPAddress.GetCurrentMachineIp();

                var res = _httpEngine.GetResponseStringAsync(url).Result;
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return false;
            }

            return true;
        }

        private static string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private bool IsAlreadLockedByMe(SvnClient client)
        {
            var svnInfo = new Collection<SvnInfoEventArgs>();
            client.GetInfo(_excelFilePath, new SvnInfoArgs(), out svnInfo);

            if (svnInfo.FirstOrDefault().Lock == null)
            {
                return false;
            }

            var userFullName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').LastOrDefault();

            return svnInfo.FirstOrDefault().Lock.Owner.Equals(userFullName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
