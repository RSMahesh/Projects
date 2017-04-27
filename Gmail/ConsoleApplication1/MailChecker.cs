using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace MediaValet
{
    /// <summary>
    /// The mail checker.
    /// </summary>
    public class MailChecker
    {
        private const string Host = "pop.gmail.com";

        public const int Port = 995;

        private readonly string _password;

        private readonly string _userName;

        private Pop3Client _pop3Client;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailChecker"/> class.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public MailChecker(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        /// <summary>
        /// The get mail.
        /// </summary>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="receveidAfter">
        /// The receveid after.
        /// </param>
        /// <param name="waitSpan">
        /// The wait span.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Email> GetMail(string subject, DateTime receveidAfter, TimeSpan waitSpan)
        {
            const int sleepInterval = 2000;

            var waitedSoFar = 0;

            while (waitedSoFar < waitSpan.TotalMilliseconds)
            {
                _pop3Client = new Pop3Client(Host, Port, _userName, _password, true);
                _pop3Client.Connect();

                IEnumerable<Email> emails =
                    GetMailAfterTime(receveidAfter)
                        .Where(m => m.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase));

                if (emails.Any())
                {
                    return emails.ToList();
                }

                _pop3Client.Dispose();

                Thread.Sleep(sleepInterval);
                waitedSoFar += sleepInterval;
            }

            return Enumerable.Empty<Email>();
        }

        /// <summary>
        /// The get mail after time.
        /// </summary>
        /// <param name="receveidAfter">
        /// The receveid after.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private List<Email> GetMailAfterTime(DateTime receveidAfter)
        {
            int mailCount = _pop3Client.GetEmailCount();
            _pop3Client.FetchEmail(mailCount);

            var emails = new List<Email>();

            int index = mailCount;

            while (true)
            {
                Email email = _pop3Client.FetchEmail(index);

                if (email == null || email.UtcDateTime < receveidAfter.ToUniversalTime())
                {
                    break;
                }

                email.Body = GetMessageBody(index);

                emails.Add(email);
                index--;
            }

            return emails;
        }

        /// <summary>
        /// The get message body.
        /// </summary>
        /// <param name="emailIndex">
        /// The email index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetMessageBody(int emailIndex)
        {
            return _pop3Client.FetchMessageParts(emailIndex);
        }
    }

    /// <summary>
    /// The pop 3 client.
    /// </summary>
    public class Pop3Client : IDisposable
    {
        private TcpClient _client;

        private Stream _clientStream;

        private string _email;

        private string _host;

        private bool _isSecure;

        private string _password;

        private int _port;

        private StreamReader _reader;

        private StreamWriter _writer;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaValet.Pop3Client"/> class.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public Pop3Client(string host, int port, string email, string password)
            : this(host, port, email, password, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaValet.Pop3Client"/> class.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="secure">
        /// The secure.
        /// </param>
        public Pop3Client(string host, int port, string email, string password, bool secure)
        {
            _host = host;
            _port = port;
            _email = email;
            _password = password;
            _isSecure = secure;
        }


        public void Close()
        {
            if (_client != null)
            {
                if (_client.Connected)
                {
                    Logout();
                }

                _client.Close();
                _client = null;
            }

            if (_clientStream != null)
            {
                _clientStream.Close();
                _clientStream = null;
            }

            if (_writer != null)
            {
                _writer.Close();
                _writer = null;
            }

            if (_reader != null)
            {
                _reader.Close();
                _reader = null;
            }

            _disposed = true;
        }

        /// <summary>
        /// The connect.
        /// </summary>
        public void Connect()
        {
            if (_client == null)
            {
                _client = new TcpClient();
            }

            if (!_client.Connected)
            {
                _client.Connect(_host, _port);
            }

            if (_isSecure)
            {
                var secureStream = new SslStream(_client.GetStream());
                secureStream.AuthenticateAsClient(_host);
                _clientStream = secureStream;
                secureStream = null;
            }
            else
            {
                _clientStream = _client.GetStream();
            }

            _writer = new StreamWriter(_clientStream);
            _reader = new StreamReader(_clientStream);

            ReadLine();
            Login();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                Close();
            }
        }

        /// <summary>
        /// The fetch email.
        /// </summary>
        /// <param name="emailId">
        /// The email id.
        /// </param>
        /// <returns>
        /// The <see cref="_email"/>.
        /// </returns>
        public MediaValet.Email FetchEmail(int emailId)
        {
            if (IsResponseOk(SendCommand("TOP " + emailId + " 0")))
            {
                return new MediaValet.Email(ReadLines());
            }

            return null;
        }

        /// <summary>
        /// The fetch email list.
        /// </summary>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<MediaValet.Email> FetchEmailList(int start, int count)
        {
            var emails = new List<MediaValet.Email>(count);
            for (int i = start; i < (start + count); i++)
            {
                Email email = FetchEmail(i);
                if (email != null)
                {
                    emails.Add(email);
                }
            }

            return emails;
        }

        /// <summary>
        /// The fetch message parts.
        /// </summary>
        /// <param name="emailId">
        /// The email id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public string FetchMessageParts(int emailId)
        {
            if (IsResponseOk(SendCommand("RETR " + emailId)))
            {
                return ReadLines();
            }

            return null;
        }

        /// <summary>
        /// The get email count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetEmailCount()
        {
            int count = 0;
            string response = SendCommand("STAT");
            if (IsResponseOk(response))
            {
                string[] arr = response.Substring(4).Split(' ');
                count = Convert.ToInt32(arr[0]);
            }
            else
            {
                count = -1;
            }

            return count;
        }

        /// <summary>
        /// The is response ok.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        protected static bool IsResponseOk(string response)
        {
            if (response.StartsWith("+OK"))
            {
                return true;
            }

            if (response.StartsWith("-ERR"))
            {
                return false;
            }

            throw new Exception("Cannot understand server response: " + response);
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        protected void Login()
        {
            if (!IsResponseOk(SendCommand("USER " + _email))
                || !IsResponseOk(SendCommand("PASS " + _password)))
            {
                throw new Exception("User/password not accepted");
            }
        }

        /// <summary>
        /// The logout.
        /// </summary>
        protected void Logout()
        {
            SendCommand("RSET");
        }

        /// <summary>
        /// The read line.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string ReadLine()
        {
            return _reader.ReadLine() + "\r\n";
        }

        /// <summary>
        /// The read lines.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string ReadLines()
        {
            var b = new StringBuilder();
            while (true)
            {
                var line = ReadLine();

                if (line == ".\r\n" || line.IndexOf("-ERR") != -1)
                {
                    break;
                }

                b.Append(line);
            }

            return b.ToString();
        }

        /// <summary>
        /// The send command.
        /// </summary>
        /// <param name="cmdtext">
        /// The cmdtext.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string SendCommand(string cmdtext)
        {
            _writer.WriteLine(cmdtext);
            _writer.Flush();
            return ReadLine();
        }
    }

    /// <summary>
    /// The email.
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaValet.Email"/> class.
        /// </summary>
        /// <param name="emailText">
        /// The email text.
        /// </param>
        public Email(string emailText)
        {
            Headers = ParseHeaders(emailText);
            ContentType = Headers["Content-Type"];
            From = Headers["From"];
            To = Headers["To"];
            Subject = Headers["Subject"];

            if (Headers["Date"] != null)
            {
                try
                {
                    UtcDateTime = ConvertStrToUtcDateTime(Headers["Date"]);
                }
                catch (FormatException)
                {
                    UtcDateTime = DateTime.MinValue;
                }
            }
            else
            {
                UtcDateTime = DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        public string ContentType { get; protected set; }

        /// <summary>
        /// Gets or sets the from.
        /// </summary>
        public string From { get; protected set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        public NameValueCollection Headers { get; protected set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; protected set; }

        /// <summary>
        /// Gets or sets the to.
        /// </summary>
        public string To { get; protected set; }

        /// <summary>
        /// Gets or sets the utc date time.
        /// </summary>
        public DateTime UtcDateTime { get; protected set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        public string Body { get; set; }

        private static Regex UtcDateTimeRegex =
            new Regex(
                @"^(?:\w+,\s+)?(?<day>\d+)\s+(?<month>\w+)\s+(?<year>\d+)\s+(?<hour>\d{1,2})"
                + @":(?<minute>\d{1,2}):(?<second>\d{1,2})\s+(?<offsetsign>\-|\+)(?<offsethours>"
                + @"\d{2,2})(?<offsetminutes>\d{2,2})(?:.*)$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static DateTime ConvertStrToUtcDateTime(string str)
        {
            Match m = UtcDateTimeRegex.Match(str);
       
            if (!m.Success)
            {
                throw new FormatException("Incompatible date/time string format");
            }

            var offsetSign = m.Groups["offsetsign"].Value;
           
            var offsetHours = Convert.ToInt32(m.Groups["offsethours"].Value);
            
            var offsetMinutes = Convert.ToInt32(m.Groups["offsetminutes"].Value);
           
            var dateTime = new  DateTime(
                Convert.ToInt32(m.Groups["year"].Value),
                GetMonth(m.Groups["month"].Value),
                Convert.ToInt32(m.Groups["day"].Value),
                Convert.ToInt32(m.Groups["hour"].Value),
                Convert.ToInt32(m.Groups["minute"].Value),
                Convert.ToInt32(m.Groups["second"].Value));

            if (offsetSign == "+")
            {
                dateTime.AddHours(offsetHours);
                dateTime.AddMinutes(offsetMinutes);
            }
            else if (offsetSign == "-")
            {
                dateTime.AddHours(-offsetHours);
                dateTime.AddMinutes(-offsetMinutes);
            }

            return dateTime;
        }

        private static NameValueCollection ParseHeaders(string headerText)
        {
            string line;
            string headerName = string.Empty;
            string headerValue;
            int colonIndx;

            var headers = new NameValueCollection();
            var reader = new StringReader(headerText);
        
            while ((line = reader.ReadLine()) != null)
            {
                if (line == String.Empty)
                {
                    break;
                }

                if (Char.IsLetterOrDigit(line[0]) && (colonIndx = line.IndexOf(':')) != -1)
                {
                    headerName = line.Substring(0, colonIndx);
                    headerValue = line.Substring(colonIndx + 1).Trim();
                    headers.Add(headerName, headerValue);
                }
                else if (headerName != string.Empty)
                {
                    headers[headerName] += " " + line.Trim();
                }
                else
                {
                    throw new FormatException("Could not parse headers");
                }
            }

            return headers;
        }

        private static int GetMonth(string monthY)
        {
            int month;

            switch (monthY)
            {
                case "Jan":
                    month = 1;
                    break;
                case "Feb":
                    month = 2;
                    break;
                case "Mar":
                    month = 3;
                    break;
                case "Apr":
                    month = 4;
                    break;
                case "May":
                    month = 5;
                    break;
                case "Jun":
                    month = 6;
                    break;
                case "Jul":
                    month = 7;
                    break;
                case "Aug":
                    month = 8;
                    break;
                case "Sep":
                    month = 9;
                    break;
                case "Oct":
                    month = 10;
                    break;
                case "Nov":
                    month = 11;
                    break;
                case "Dec":
                    month = 12;
                    break;
                default:
                    throw new FormatException("Unknown month.");
            }

            return month;
        }
    }
}