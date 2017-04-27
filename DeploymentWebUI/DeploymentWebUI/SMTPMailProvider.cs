using System;
using System.Net;
using System.Net.Mail;

namespace DeploymentWebUI
{
    internal class SMTPMailProvider
    {
        private readonly string _smtp;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _ssl;

        public event Action<Exception> OnException;

        public SMTPMailProvider(string smtp, int port, string username, string password, bool ssl)
        {
            this._smtp = smtp;
            this._port = port;
            this._username = username;
            this._password = password;
            this._ssl = ssl;
        }

        public bool SendMail(MailMessage message)
        {
            try
            {
                using (var sC = new SmtpClient(this._smtp))
                {
                    sC.Port = Convert.ToInt32(this._port);
                    sC.Credentials = new NetworkCredential(this._username, this._password);
                    sC.EnableSsl = this._ssl;
                    sC.Send(message);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return true;
        }
    }
}