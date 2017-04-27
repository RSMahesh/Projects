using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.Text;
using System.IO;

namespace ResellerClub.Common
{
    public class Email
    {
        public static void SendMail(string emailTo, string subject, string emailText, string attachment)
        {
            SendMail(emailTo,subject,emailText,attachment, "jai-hind@infowebservices.in", "Infowebservices Support");
        }
        public static void SendMail(string emailTo, string subject, string emailText, string attachment,string fromId,string fromDisplayName)
        {

            MailMessage mail = null;
            SmtpClient smtp = new SmtpClient("smtp.infowebservices.in");
            System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("jai-hind@infowebservices.in", "HIa(^yh8");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = SMTPUserInfo;
            MailAddress from = new MailAddress(fromId,
               fromDisplayName,
            System.Text.Encoding.UTF8);
            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress(emailTo);
            // Specify the message content.
            mail = new MailMessage(from, to);
            mail.Body = emailText;
            mail.IsBodyHtml = true;
            // Include some non-ASCII characters in body and subject.
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            if (!string.IsNullOrEmpty(attachment))
            {
                using (MemoryStream sm = new MemoryStream(Encoding.UTF8.GetBytes(attachment)))
                {
                    mail.Attachments.Add(new Attachment(sm,Path.GetFileName(attachment)));
                   
                }
            }
            smtp.Send(mail);
            mail.Dispose();
        }
    

    }

}

