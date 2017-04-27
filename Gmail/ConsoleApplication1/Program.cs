using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediaValet;

namespace ConsoleApplication1
{
    class Program
    {
        public const string Host = "pop.gmail.com";
        public const int Port = 995;

        static void Main(string[] args)
        {
            MailChecker mailChecker = new MailChecker("bailwalmediavalet@gmail.com", "MB248001");

        var mails =    mailChecker.GetMail(
                "A Zipped File has been sent to you",
                DateTime.Now.AddDays(-2),
                TimeSpan.FromMinutes(2));

            if (mails.Any())
            {
                if (mails.FirstOrDefault().Body.Contains("my name is"))
                {
                    var gg = "dsdd";
                }
            }
   
        }
    }
}
