using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Core;

namespace FileSystem
{
    public class JarFileAssemblyLoader
    {
        public void Register()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            IJarFileFactory jarFileFactory = new JarFileFactory();

            using (var reader = jarFileFactory.GetJarFileReader(RuntimeHelper.MapToCurrentExecutionLocation("Include.jar")))
            {

                var jarFileItem = reader.GetNextFile();

                string resourceName = new AssemblyName(args.Name).Name + ".dll";

                File.AppendAllText("Log.txt", "Getting :" + resourceName + Environment.NewLine);

                while (jarFileItem != null)
                {
                    if (jarFileItem.Headers.ContainsKey("FileName"))
                    {
                        if (jarFileItem.Headers["FileName"] == resourceName)
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

        private System.Reflection.Assembly CurrentDomain_AssemblyResolve12(object sender, ResolveEventArgs args)
        {
            string resourceName = new AssemblyName(args.Name).Name + ".dll";
            string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                Byte[] assemblyData = new Byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }
    }
}
