
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AppUpdater.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            new StartUp().StartUpdate(args);
        }
    }
}
