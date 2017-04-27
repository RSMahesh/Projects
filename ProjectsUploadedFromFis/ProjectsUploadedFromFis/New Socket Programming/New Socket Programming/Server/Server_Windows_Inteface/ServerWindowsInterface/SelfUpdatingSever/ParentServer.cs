using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
namespace SelfUpdatingSever
{
    public sealed class ParentServer
    {
        AppDomain workerAppDomain = null;

        public void Start(ServerDLL._ServerType serverType, string port)
        {
            bool load = true;
            AppDomain workerAppDomain;
            ParameterizedThreadStart thStrat = new ParameterizedThreadStart(LoadServerDll);
            object[] args ={ 2, port };
            Thread childThread = new Thread(thStrat);
            Thread.Sleep(6000);
            while (true)
            {
                if (load)
                {
                    load = false;
                    GetLatestDll(@"\\risinas\Temp\Mark\ServerDLL.dll");
                    childThread.Start(args);
                }

                Thread.Sleep(6000);

                if (IsNewDLLAvailable(@"\\risinas\Temp\Mark\ServerDLL.dll"))
                {
                    Process notePad = new Process();
                   
                    notePad.StartInfo.FileName = @"ServerWindowsInterface.exe";
                    notePad.Start();
                    UnLoadDomain();
                    workerAppDomain = null;
                    Process.GetCurrentProcess().Kill();
                }

            }
        }

        public void StartDirectly(ServerDLL._ServerType serverType, string port)
        {
            ServerDLL.Server server = new ServerDLL.Server();
            server.Start((int)serverType, port);
        }

        public void StartWise(string clientIP)
        {
            ServerDLL.Server server = new ServerDLL.Server();
            if(LocalPing(clientIP))
            server.Start((int)ServerDLL._ServerType.TCPServer, "1020");
            else
            server.Start((int)ServerDLL._ServerType.SyncFileServer,@"\\risinas\Temp\RDC");
        }

        private bool  LocalPing(string IP)
        {
            // Ping's the local machine.
            Ping pingSender = new Ping();
            //str address = new IPAddress(
            PingReply reply = pingSender.Send(IP);
            return reply.Status == IPStatus.Success;
            
        }

        private bool IsNewDLLAvailable(string path)
        {
            return true;
            if (!File.Exists(path))
                return false;

            if (!File.Exists("ServerDLL.dll"))
                return true;

            FileInfo newDll = new FileInfo(@"\\risinas\Temp\Mark\ServerDLL.dll");
            FileInfo oldDll = new FileInfo(@"ServerDLL.dll");

            return (newDll.LastWriteTime > oldDll.LastWriteTime);

        }

        private void GetLatestDll(string path)
        {
            if (!File.Exists(path))
                return;

            if (!File.Exists("ServerDLL.dll"))
            {
                File.Copy(path, "ServerDLL.dll");
                return;
            }

            FileInfo newDll = new FileInfo(@"\\risinas\Temp\Mark\ServerDLL.dll");
            FileInfo oldDll = new FileInfo(@"ServerDLL.dll");

            //  if (newDll.LastWriteTime > oldDll.LastWriteTime)
            {
                File.Delete("ServerDLL.dll");
                File.Copy(path, "ServerDLL.dll");
            }
        }


        private void LoadServerDll(object parameter)
        {
            AppDomainSetup ads = new AppDomainSetup();
            workerAppDomain = AppDomain.CreateDomain("FileSever", null, ads);

            object[] args = (object[])parameter;
            object obj = workerAppDomain.CreateInstanceAndUnwrap(@"ServerDLL", "ServerDLL.Server");
            Type type = obj.GetType();
            MethodInfo[] mi = type.GetMethods();
            object Result = type.InvokeMember("Start",
              BindingFlags.Default | BindingFlags.InvokeMethod,
                   null,
                   obj,
                   args);
        }

        private void UnLoadDomain()
        {
            if (workerAppDomain != null)
                AppDomain.Unload(workerAppDomain);

        }
        public void Dispose()
        {
        }

        public static Object InvokeMethodSlow(string AssemblyName,
           string ClassName, string MethodName, Object[] args)
        {
            // load the assemly
            Assembly assembly = Assembly.LoadFrom(AssemblyName);

            // Walk through each type in the assembly looking for our class
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass == true)
                {
                    if (type.FullName.EndsWith("." + ClassName))
                    {
                        // create an instance of the object
                        object ClassObj = Activator.CreateInstance(type);


                        MethodInfo[] mi = type.GetMethods();
                        foreach (MethodInfo m in mi)
                        {
                            Console.WriteLine(m.Name);
                        }

                        // Dynamically Invoke the method
                        object Result = type.InvokeMember(MethodName,
                          BindingFlags.Default | BindingFlags.InvokeMethod,
                               null,
                               ClassObj,
                               args);
                        return (Result);
                    }
                }
            }
            throw (new System.Exception("could not invoke method"));
        }


    }
}
