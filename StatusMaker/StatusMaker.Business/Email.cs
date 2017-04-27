using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;

namespace StatusMaker.Business
{
    //TODO: move this clase and others out of business/domain layer as they belong to infrastructure layer or you may call it Coreor helper layer.
    //This according to Domain driven disgin "Isolating the domain implementation is a prerequisite for domain-driven design.The best part of isolating the domain is getting
    //all that other stuff out of the way so that we can really focus on the domain design"
    public class Email : IEmail
    {
        private const string Subject = "Dev status as on {0}";
        private IEnumerable<string> _toRecipients;
        private IEnumerable<string> _ccRecipients;
        private MailItem outlookMsg = null;

        public void SendMail(IEnumerable<string> toRecipients, IEnumerable<string> ccRecipients, string eamilHtmlBody)
        {
            outlookMsg = CreateMsgObject();
            _toRecipients = toRecipients;
            _ccRecipients = ccRecipients;

            outlookMsg.Subject = GetSubject();
            outlookMsg.HTMLBody = eamilHtmlBody;

            AddReceivers();
            OpenMsgInOutlook();
        }

        private MailItem CreateMsgObject()
        {
            Application oApp = new Application();

            return (MailItem)oApp.CreateItem(OlItemType.olMailItem);
        }

        private string GetSubject()
        {
            return string.Format(Subject, String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now));
        }

        private void AddReceivers()
        {
            ResloveReceipents(_toRecipients, OlMailRecipientType.olTo);
            ResloveReceipents(_ccRecipients, OlMailRecipientType.olCC);
        }

        private void ResloveReceipents(IEnumerable<string> recipients, OlMailRecipientType mailRecipientType)
        {
            Recipients oRecips = outlookMsg.Recipients;

            foreach (var recipient in recipients)
            {
                Recipient ccRecipient = oRecips.Add(recipient);
                ccRecipient.Type = (int)mailRecipientType;
                ccRecipient.Resolve();
            }
        }

        private void OpenMsgInOutlook()
        {
            var appdataFolder =  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StatusMaker");
            if(!Directory.Exists(appdataFolder))
            {
                Directory.CreateDirectory(appdataFolder);
            }

            var statusMsgFilePath = Path.Combine(appdataFolder, Guid.NewGuid().ToString() + ".msg");
            outlookMsg.SaveAs(statusMsgFilePath);
            Process.Start(statusMsgFilePath);
        }
    }
}
