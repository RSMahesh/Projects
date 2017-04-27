/*************************************************
* Name: Diagnostics
* Purpose: To provide a common group of diagnostic
*      functions
* Created: 10/28/2004
* Last Modified: 12/13/2004
*************************************************/

using System;
using System.Reflection;

namespace ResellerClub.Common
{
    /// <summary>
    /// A common set of functions for Diagnostics.
    /// </summary>  
    public sealed class Diagnostics
    {
        // Disable new Diagnostics();
        private Diagnostics()
        { }

        /// <summary>
        /// Get the full assembly name, including the namespace.
        /// </summary>
        /// <param name="assembly">The assembly you want to get the name for.</param>
        /// <returns>The string of the assembly (and namespace).</returns>
        public static string GetFullAssemblyName(Assembly assembly)
        {
            string assemblyName = assembly.FullName;
            assemblyName = assemblyName.Substring(0, assemblyName.IndexOf(","));

            return assemblyName;
        }

        /// <summary>
        /// Get the full assembly name, including the namespace.
        /// </summary>
        /// <param name="assembly">The assembly you want to get the name for.</param>
        /// <returns>The string of the assembly.</returns>
        public static string GetAssemblyName(Assembly assembly)
        {
            string assemblyName = GetFullAssemblyName(assembly);
            assemblyName = assemblyName.Substring(assemblyName.LastIndexOf(".") + 1, assemblyName.Length - assemblyName.LastIndexOf(".") - 1);

            return assemblyName;
        }
    }
}
