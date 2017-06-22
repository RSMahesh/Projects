using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


namespace FileSystem
{
    public class JarFileAssemblyLoader
    {
        public void Register()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var arr = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var ass in arr)
            {
                if(ass.GetName().Name == new AssemblyName(args.Name).Name)
               {
                    return ass;
                }
            }

            return LoadAssembly(new AssemblyName(args.Name).Name);
        }

        public Assembly LoadAssembly(string assemblyName)
        {
            var jarFiles = Directory.GetFiles(ExecutionLocation, "*.jar");
            Assembly assembly = null;

            foreach (var jarFile in jarFiles)
            {
                assembly = GetAssemblyFromJarFile(jarFile, assemblyName);
                if (assembly != null)
                {
                    return assembly;
                }
            }

            return null;
        }

        private System.Reflection.Assembly GetAssemblyFromJarFile(string jarFile, string assemblyName)
        {
            IJarFileFactory jarFileFactory = new JarFileFactory();

            using (var reader = jarFileFactory.GetJarFileReader(jarFile))
            {
                var jarFileItem = reader.GetNextFile();

                WriteLog("Getting :" + assemblyName + Environment.NewLine);

                while (jarFileItem != null)
                {
                    if (jarFileItem.Headers.ContainsKey("FileName"))
                    {
                        if (jarFileItem.Headers["FileName"] == assemblyName + ".dll" ||
                            jarFileItem.Headers["FileName"] == assemblyName + ".exe")
                        {
                            return Assembly.Load(jarFileItem.Containt);
                        }
                    }

                    jarFileItem = reader.GetNextFile();
                }

                WriteLog("Not Found :  " + assemblyName + Environment.NewLine);
            }

            return null;
        }

        //Below methods copyed from core.RuntimeHelper to avoid core dll reference

        public static string MapToCurrentExecutionLocation(string filePath)
        {
            return Path.Combine(ExecutionLocation, filePath);
        }

        public static string ExecutionLocation
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }
        }

        private void WriteLog(string text)
        {
            try
            {
                var logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SysHealth", "JarFileAssemblyLoader.log");
                File.AppendAllText(logFilePath, text);
            }
            catch
            {

            }
        }

    }
}
