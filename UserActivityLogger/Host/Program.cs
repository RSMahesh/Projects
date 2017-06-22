using Core;
using System;
using System.Configuration;
using System.IO;
using FileSystem;
using UserActivityLogger;
using Castle.Windsor;
using ActivityLogger;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            new JarFileAssemblyLoader().Register();
            Start(args);
        }
        private static void Start(string[] args)
        {

            if (SingleInstance.IsApplicationAlreadyRunning("UserActivityLoggerHost"))
            {
                Logger.LogInforamtion("Already Running");
                return;
            }

            if (args.Length > 0 && args[0] == "hidden")
            {
                Logger.LogInforamtion("Running with hidden");
                new UnhandledExceptionHandlercs().Register(Logger.LogError);
                ProcessHelper.RecreateProcessOnExit();

                IWindsorContainer windsorContainer = new WindsorContainer();
                CastleWireUp.WireUp(windsorContainer);
             
                var startUp = windsorContainer.Resolve<IStartUp>();
                startUp.Start(TimeSpan.FromSeconds(2));
            }
            else
            {
                ProcessHelper.RunHidden(System.Reflection.Assembly.GetEntryAssembly().Location);
            }
        }
    }
}
