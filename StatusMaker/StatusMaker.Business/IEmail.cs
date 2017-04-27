using System.Collections.Generic;

namespace StatusMaker.Business
{
    public interface IEmail
    {
        void SendMail(IEnumerable<string> toRecipients, IEnumerable<string> ccRecipients, string eamilHtmlBody);
    }
}