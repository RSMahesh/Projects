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
            var jarFiles = Directory.GetFiles(ExecutionLocation, "*.jar");
            Assembly assembly = null;
            foreach (var jarFile in jarFiles)
            {
                assembly = GetAssemblyFromJarFile(jarFile, args);
                if (assembly != null)
                {
                    return assembly;
                }
            }

            return null;
        }

        private System.Reflection.Assembly GetAssemblyFromJarFile(string jarFile, ResolveEventArgs args)

        {
            IJarFileFactory jarFileFactory = new JarFileFactory();

            using (var reader = jarFileFactory.GetJarFileReader(jarFile))
            {

                var jarFileItem = reader.GetNextFile();

                string resourceName = new AssemblyName(args.Name).Name;

                File.AppendAllText("Log.txt", "Getting :" + resourceName + Environment.NewLine);

                while (jarFileItem != null)
                {
                    if (jarFileItem.Headers.ContainsKey("FileName"))
                    {
                        if (jarFileItem.Headers["FileName"] == resourceName + ".dll" || 
                            jarFileItem.Headers["FileName"] == resourceName + ".exe"                            )
                        {
                            return Assembly.Load(jarFileItem.Containt);
                        }
                    }

                    jarFileItem = reader.GetNextFile();
                }

                File.AppendAllText("Log.txt", "Not Found :  " + resourceName + Environment.NewLine);
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

    }
}
