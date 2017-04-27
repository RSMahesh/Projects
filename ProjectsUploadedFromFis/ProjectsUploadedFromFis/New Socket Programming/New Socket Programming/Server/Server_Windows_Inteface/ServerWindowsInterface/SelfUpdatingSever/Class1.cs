using System;
using WorkerShared;

namespace WorkerManager
{
    public class Manager
    {
        private const string CONFIG_ASSEMBLY_POOL = @"C:\BlogProjects\AssemblyPool";
        private const string CONFIG_DYNAMIC_ASSEMBLY_PROJECT = "DisconnectedWorker";
        private const string CONFIG_DYNAMIC_ASSEMBLY_FULLY_QUALIFIED_NAME = "DisconnectedWorker.Worker";

        private const string FORMAT_WORKER_DOMAIN_FRIENDLY_NAME = "Dynamic Worker Domain";
        private const string FORMAT_WORKER_DOMAIN_CREATED = "Created '{0}' AppDomain";
        private const string FORMAT_WORKER_DOMAIN_UNLOADED = "Unloaded '{0}' AppDomain";
        private const string FORMAT_WORK_COMPLETE = "All work complete.";
        private const string FORMAT_START_ASSEMBLIES = "Starting Assemblies Loaded:";
        private const string FORMAT_END_ASSEMBLIES = "Post-unload Assemblies Loaded:";

        public void RunAppDomainExample()
        {
            // Show current assemblies before we start:
            Console.WriteLine(FORMAT_START_ASSEMBLIES);
            Utilities.WriteCurrentLoadedAssemblies();

            // create display name for appDomain
            string workerName = string.Format(FORMAT_WORKER_DOMAIN_FRIENDLY_NAME);

            // Construct and setup appDomain settings:
            AppDomainSetup ads = new AppDomainSetup();

            ads.ApplicationBase = CONFIG_ASSEMBLY_POOL;
            ads.DisallowBindingRedirects = false;
            ads.DisallowCodeDownload = true;
            ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            // Create domain
            Console.WriteLine();
            AppDomain workerAppDomain = AppDomain.CreateDomain("FileServer", null, ads);
            Console.WriteLine(FORMAT_WORKER_DOMAIN_CREATED, workerName) ;

            // do work on proxy
            IWorker workerInstance =
                (IWorker)
                workerAppDomain.CreateInstanceAndUnwrap(CONFIG_DYNAMIC_ASSEMBLY_PROJECT,
                            &nbs p;                           CONFIG_D YNAMIC_ASSEMBLY_FULLY_QUALIFIED_NAME);

            // Execute the task by invoking method on the interface instance
            workerInstance.DoWork();

            // Unload worker appDomain
            AppDomain.Unload(workerAppDomain);
            Console.WriteLine(FORMAT_WORKER_DOMAIN_UNLOADED, workerName) ;
            Console.WriteLine();

            // Show current assemblies before we start:
            Console.WriteLine(FORMAT_END_ASSEMBLIES);
            Utilities.WriteCurrentLoadedAssemblies();

            Console.WriteLine(FORMAT_WORK_COMPLETE);
            Console.ReadLine();
        }
    }
}