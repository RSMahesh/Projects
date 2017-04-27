using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityLogger.Tests
{
   // [TestFixture]
    public class TestForBest
    {

        [Test]
        public void AppendFiles123()
        {
            string name = "mahesh.bailwal";
            var rrr = new string ( name.Reverse().ToArray());
            var userFullName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').LastOrDefault();
            userFullName = userFullName.Replace(".", "DOT");
            var rr = new string(userFullName.Reverse().ToArray());
            var rrUndo = new string(rr.Reverse().ToArray());

        }
    }
}
