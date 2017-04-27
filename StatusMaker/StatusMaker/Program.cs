using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Castle.Windsor;
using StatusMaker.UI;
using StatusMaker.UI.Properties;
using System.Threading.Tasks;

namespace StatusMaker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Task.Run(() => CheckForUpdate());
            //if (CheckForUpdate())
            //{
            //    //Do Not return as update exe need paremt process id
            //    //return;
            //}

            //   AppDomain currentDomain = AppDomain.CurrentDomain;
            Application.ThreadException += Application_ThreadException1;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AddRuntimeSetings();

            IWindsorContainer windsorContainer = new WindsorContainer();
            CastleWireUp.WireUp(windsorContainer);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = (Form)windsorContainer.Resolve<IMainForm>();

            ((IMainForm)mainForm).VersionNumber = GetVersionNumber();

            Application.Run(mainForm);
        }

        private static void Application_ThreadException1(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            MessageBox.Show(e.Message);
        }

        private static void AddRuntimeSetings()
        {
            ConfigurationManager.AppSettings["excelDownloadFilePath"] =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Progress Tracking.xlsx");
        }

        private static bool CheckForUpdate()
        {
            var startup = GetStartUpInstance();
            if (startup.IsUpdateRequired())
            {
                startup.LaunchProcess();
                return true;
            }

            return false;
        }
        private static dynamic GetStartUpInstance()
        {
            Assembly assembly = Assembly.LoadFrom("Updater.exe");

            Type type = assembly.GetType("AppUpdater.Host.StartUp");

            dynamic startUp = Activator.CreateInstance(type);

            return startUp;
        }

        private static string GetVersionNumber()
        {
            return File.ReadAllText("Version.txt");
        }
    }
}
