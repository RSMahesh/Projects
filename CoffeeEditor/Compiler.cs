using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediaProcessor.ServiceLibrary.Common;

namespace CoffeeEditor
{
    public static class Compiler
    {
   //     static string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "temp.coffee");
        /// <summary>
        /// The complie.
        /// </summary>
        public static string Complie(string coffeText)
        {
            string javaScriptText;
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid()+"temp.coffee");
   
            try
            {
                File.WriteAllText(tempFile, coffeText);
                javaScriptText = ExeRunner.Execute(Helper.GetCoffeePath(), @"--compile --print " + tempFile);
                File.Delete(tempFile);
            }
            catch (ExeExecutionException ex)
            {
                throw;
            }
            return javaScriptText;
        }

    }
}
