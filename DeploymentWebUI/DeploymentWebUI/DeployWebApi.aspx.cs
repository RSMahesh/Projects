using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;

using MediaProcessor.ServiceLibrary.Common;

using Microsoft.Ajax.Utilities;

namespace DeploymentWebUI
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private static object syncLock = new object();
        private string deploymentInProgessCacheKey = "DeploymentInProgess";

        private string enterpriseLibraryDllsSharedPath = @"\\10.131.60.3\Shared\EnterpriseLibraryDlls";

        private string enterpriseLibraryDllsLocalPath = @"D:\GitRepo\Deployment\EnterpriseLibraryDlls";

        private int cacheExpireInSeconds = 60 * 5;

        private string pullNumber;

        protected void Page_Load(object sender, EventArgs e)
        {
            IsValidIp();

            if (!IsPostBack)
            {
                IsDeploymentAlreadyInProgess();
            }

            Wizard1.PreRender += new EventHandler(Wizard1_PreRender);

        }

        private Dictionary<string, string> GetValidIps()
        {
            var ValidIpsAndUsers = ConfigurationManager.AppSettings["ValidIpsAndUsers"];
            var arr = ValidIpsAndUsers.Split(',');


            IpUserMapping = new Dictionary<string, string>();

            foreach (var item in arr)
            {
                var arrIpAndUser = item.Split('|');

                IpUserMapping[arrIpAndUser[0]] = arrIpAndUser[1];
            }

            return IpUserMapping;
        }

        private void IsValidIp()
        {
            if (!GetValidIps().ContainsKey(this.GetClientIPAddress()))
            {
                Response.Clear();
                Response.Write("UNAUTHORISED IP ADDRESS");
                Response.End();
            }
            else
            {
                divMessage.InnerText = "Your IP is:" + GetClientIPAddress() + " and User Name:" + GetValidIps()[this.GetClientIPAddress()];
            }
        }

        protected void Wizard1_PreRender(object sender, EventArgs e)
        {
            Repeater SideBarList = Wizard1.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
            SideBarList.DataSource = Wizard1.WizardSteps;
            SideBarList.DataBind();
        }

        protected string GetClassForWizardStep(object wizardStep)
        {
            WizardStep step = wizardStep as WizardStep;

            if (step == null)
            {
                return "";
            }

            int stepIndex = Wizard1.WizardSteps.IndexOf(step);

            if (stepIndex < Wizard1.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > Wizard1.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }

        private static Dictionary<string, string> IpUserMapping = new Dictionary<string, string> { { "::1", "Mahesh" }, { "", "" } };

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {

            divMessage.InnerHtml = "";
            if (!HandleNext(e.CurrentStepIndex))
            {
                e.Cancel = true;
            }
            if (e.CurrentStepIndex != 0)
            {
                UpdateCache(cacheExpireInSeconds);
            }
        }

        private bool HandleNext(int currentStepIndex)
        {
            Logger logger = new Logger();
            var suscess = true;
            switch (currentStepIndex)
            {
                case 0:
                    if (!GetExclusiveLockOnDeployment())
                    {
                        Wizard1.DisplayCancelButton = false;
                        this.IsDeploymentAlreadyInProgess();
                        return false;
                    }

                    Wizard1.DisplayCancelButton = true;

                    //return true;
                    logger.LogInformation("===Deployment Started By User IP :" + GetClientIPAddress() + "===========");

                    var output = HttpHelper.CleanDeploymentBranch();

                    logger.LogInformation(output);

                    DeleteEnterpriseDlls();

                    break;

                case 1:
                    var pulls = HttpHelper.GetPulls();
                    if (!pulls.Any())
                    {
                        divContent2.InnerHtml =
                            "No Pulls found against RsMahesh/MediaValetAPI. Please create pull and click next";
                        return false;
                    }

                    var label = HttpHelper.GetPullLabel(pulls.FirstOrDefault() as Dictionary<string, object>);
                    divContent3.InnerText = "Pulls found with label:" + label + ". Please click next to merge pull";
                    break;
                case 2:
                    try
                    {
                        HttpHelper.MergePull(HttpHelper.GetPulls());
                    }
                    catch (DeploymentException ex)
                    {
                        divContent3.InnerText = ex.Message;
                        suscess = false;
                    }

                    divContent4.InnerHtml = GetEnterpriseDllsAsString();

                    break;
                case 3:
                    if (CopyEnterpriseDlls())
                    {
                        var output12 = HttpHelper.UpdateEnterpriseDllReference();
                        logger.LogInformation(output12);
                    }
                    break;
            }

            return suscess;
        }

        private string GetEnterpriseDllsAsString()
        {
            var files = Directory.GetFiles(enterpriseLibraryDllsSharedPath);

            if (!files.Any())
            {
                return "No EnterpriseDll found at " + enterpriseLibraryDllsSharedPath;
            }

            var sb = new StringBuilder();

            foreach (var file in files)
            {
                sb.Append(Path.GetFileName(file) + "<br/>");
            }

            return files.Count().ToString() + " EnterpriseDll found at " + enterpriseLibraryDllsSharedPath
                   + Environment.NewLine + sb.ToString();
        }

        private void DeleteEnterpriseDlls()
        {
            var files = Directory.GetFiles(enterpriseLibraryDllsLocalPath);
            foreach (var file in files)
            {
                File.Delete(file);
            }

            files = Directory.GetFiles(enterpriseLibraryDllsSharedPath);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        private bool CopyEnterpriseDlls()
        {
            var files = Directory.GetFiles(enterpriseLibraryDllsSharedPath);
            foreach (var file in files)
            {
                File.Copy(file, Path.Combine(enterpriseLibraryDllsLocalPath, Path.GetFileName(file)));
            }

            return Directory.GetFiles(enterpriseLibraryDllsLocalPath).Any();
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            var logger = new Logger();
            this.UpdateCache(60 * 10);
            HttpHelper.TriggerTeamCityBuild();
            divMessage.InnerHtml = "Deployment Strated. Please check teamcity for progress";
            Wizard1.Enabled = false;
            logger.LogInformation("===Deployment Finished By User IP :" + GetClientIPAddress() + "===========");
        }

        private bool GetExclusiveLockOnDeployment()
        {
            if (this.Cache[this.deploymentInProgessCacheKey] != null)
            {
                return false;
            }

            lock (syncLock)
            {
                if (this.Cache[this.deploymentInProgessCacheKey] != null)
                {
                    return false;
                }

                this.Cache.Insert(
                    this.deploymentInProgessCacheKey,
                    this.GetClientIPAddress(),
                    null,
                    DateTime.Now.AddSeconds(this.cacheExpireInSeconds),
                    TimeSpan.Zero);

                return true;
            }

        }

        private string GetClientIPDoingDeployment()
        {
            if (Cache[deploymentInProgessCacheKey] != null)
            {
                return Cache[deploymentInProgessCacheKey].ToString();
            }

            return string.Empty;
        }

        private bool IsDeploymentAlreadyInProgess()
        {
            var ip = GetClientIPDoingDeployment();
            if (ip != string.Empty)
            {
                divMessage.InnerText = "Deployment Already In Progress by IP:" + ip;
                if (IpUserMapping.Keys.Contains(ip))
                {
                    divMessage.InnerText += " User:" + IpUserMapping[ip].ToString();
                }

                divMessage.InnerHtml += ". Please Wait";

                //   Wizard1.Enabled = false;
                return true;
            }
            return false;
        }

        private void UpdateCache(int expirationInseconds)
        {
            Cache.Remove(deploymentInProgessCacheKey);
            Cache.Insert(deploymentInProgessCacheKey, GetClientIPAddress(), null,
          DateTime.Now.AddSeconds(expirationInseconds), TimeSpan.Zero);
        }

        public string GetClientIPAddress()
        {
            NameValueCollection serverVariables = Request.ServerVariables;

            string ip = serverVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] ipRange = ip.Split(',');
                int le = ipRange.Length - 1;
                string trueIP = ipRange[le];
            }
            else
            {
                ip = serverVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        protected void Wizard1_CancelButtonClick(object sender, EventArgs e)
        {
            if (Cache[deploymentInProgessCacheKey] != null)
            {
                if (Cache[deploymentInProgessCacheKey].ToString() == GetClientIPAddress())
                {
                    Cache.Remove(deploymentInProgessCacheKey);
                    Wizard1.ActiveStepIndex = 0;
                    var logger = new Logger();
                    logger.LogInformation("===Deployment Canceled By User IP :" + GetClientIPAddress() + "===========");
                }
            }
        }

        protected void btnSkipCreatePull_Click(object sender, EventArgs e)
        {
            Wizard1.ActiveStepIndex = 3;
            divContent4.InnerHtml = GetEnterpriseDllsAsString();
        }

        private void SendMail()
        {
            //string smtp, int port, string username, string password, bool ssl
            var body = "Deployment Started by user ip: " + GetClientIPAddress() + " User Name :"
                       +  GetValidIps()[this.GetClientIPAddress()];

            if (string.IsNullOrEmpty(pullNumber))
            {

                body += " For pull :" + pullNumber;
            }

            var mail = new MailMessage(ConfigurationManager.AppSettings["FromAddress"], "mahesh.bailwal@rsystems.com", "Deployment Started", body);
            SMTPMailProvider sMTPMailProvider =
                new SMTPMailProvider(
                    ConfigurationManager.AppSettings["SmtpHost"],
                   int.Parse(ConfigurationManager.AppSettings["Port"]),
                    ConfigurationManager.AppSettings["UserName"],
                    ConfigurationManager.AppSettings["Password"],
                   true);

        }

    }
}