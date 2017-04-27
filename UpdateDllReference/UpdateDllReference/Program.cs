using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UpdateDllReference
{
    class Program
    {
        private static bool fileUpdated = false;
        static void Main(string[] args)
        {
            UpdateAllProjectFile(@"D:\temp", ConfigurationManager.AppSettings["CodePath"]);
        }

        static void UpdateAllProjectFile(string dllFolder, string solutionFolder)
        {
            var excludeFiles = new[] { "Examples" };

            var files = Directory.GetFiles(solutionFolder, "*.csproj", SearchOption.AllDirectories);
            foreach (var projfile in files)
            {
                if (!excludeFiles.Contains(Path.GetFileNameWithoutExtension(projfile)))
                {
                    UpdateAll(dllFolder, projfile);
                }
            }

        }

        private static void UpdateAll(string dllFolder, string csProjfile)
        {
            var files = Directory.GetFiles(dllFolder);
            XElement xelement = XElement.Load(csProjfile);
            foreach (var dll in files)
            {
                fileUpdated = false;
                UpdateDllReference(xelement, dll);
            }

            if (!fileUpdated)
            {
                return;
            }



            var ww = new XmlTextWriter(csProjfile, Encoding.UTF8);
            ww.Formatting = Formatting.Indented;
            ww.WriteStartDocument();

            xelement.WriteTo(ww);
            ww.Flush();
            ww.Close();

            Console.WriteLine(string.Empty);
            Console.WriteLine("Update " + csProjfile + " refernce " + string.Join(",", files));
            
        }

        static void UpdateDllReference(XElement xelement, string dllPath)
        {
            var asm = Assembly.LoadFrom(dllPath).GetName();
            var dllVesrion = asm.Version.ToString();

            var dllName = asm.Name;

            IEnumerable<XElement> elements = xelement.Elements().ToList();
            // Read the entire XML
            foreach (var element in elements)
            {
                if (element.Name.LocalName == "ItemGroup")
                {
                    var eles = element.Elements().ToList();

                    foreach (var ele in eles)
                    {
                        var includeValue = ele.Attribute("Include").Value;
                        if (includeValue.Contains(dllName + ",") || includeValue.Equals(dllName))
                        {
                            UpdateDllVesrion(ele, dllVesrion);
                            UpdateHintPath(ele, dllPath);

                        }
                    }
                }
            }
        }

        private static void UpdateDllVesrion(XElement ele, string newVersionNumber)
        {
            var includeValue = ele.Attribute("Include").Value;
            var arr = includeValue.Split(',');

            foreach (var ar in arr)
            {
                if (ar.Contains("="))
                {
                    if (ar.Split('=')[0].Trim() == "Version")
                    {
                        fileUpdated = true;
                        var oldVersion = "Version=" + ar.Split('=')[1];
                        var newVersion = "Version=" + newVersionNumber;
                        ele.Attribute("Include").Value = includeValue.Replace(oldVersion, newVersion);
                    }
                }
            }
        }

        private static void UpdateHintPath(XElement ele, string ddlPath)
        {
            var ff = from kk in ele.Descendants() where kk.Name.LocalName == "HintPath" select kk;
            var hintPath = ff.FirstOrDefault();
            if(hintPath !=null)
            hintPath.Value = ddlPath;
        }
    }
}
