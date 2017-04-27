using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MediaProcessor.ServiceLibrary.Common;

namespace DeploymentWebUI
{
    public partial class Deploy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Wizard2_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {

        }

        protected void Wizard2_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (!HandleNext(e.CurrentStepIndex))
            {
                e.Cancel = true;
            }

        }

        private bool HandleNext(int currentStepIndex)
        {
            var suscess = true;
            switch (currentStepIndex)
            {
                case 0:
                    // var output = ExeRunner.Execute(@"D:\GitRepo\Deployment\GitGetLatestFromStage.bat", "");
                    // Literal1.Text = output;
                    break;

                case 1:
                    var pulls = HttpHelper.GetPulls();
                    if (!pulls.Any())
                    {
                        Literal1.Text = "No Pulls found against RsMahesh/MediaValetAPI. Please create pull and click next";
                        suscess = false;
                    }

                    break;
                case 2:
                    try
                    {
                        HttpHelper.MergePull(HttpHelper.GetPulls());
                    }
                    catch (DeploymentException ex)
                    {
                        Literal2.Text = ex.Message;
                        suscess = false;
                    }
                    break;
            }

            return suscess;
        }
    }
}