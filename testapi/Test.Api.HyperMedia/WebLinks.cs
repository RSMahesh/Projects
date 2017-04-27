// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebLinks.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Class implementation for WebLinks.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Test.Api.HyperMedia
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class WebLinks : IWebLinks
    {
        private HashSet<string> _functions = new HashSet<string>();

        [DataMember(Name = "self", EmitDefaultValue = false)]
        public string Self { get; set; }

        /// <summary>
        /// Gets the functions.
        /// </summary>
        [DataMember(Name = "functions")]
        public IEnumerable<string> Functions
        {
            get { return _functions; }
            set { _functions = new HashSet<string>(value); }
        }

        /// <summary>
        /// Add a web function to the object.
        /// </summary>
        /// <param name="function">
        /// The function name to add.
        /// </param>
        public void AddFunction(string function)
        {
           // Ensure.Argument.IsNotNull(function, "function");

            _functions.Add(function);
        }

        /// <summary>
        /// Clear all functions.
        /// </summary>
        public void ClearFunctions()
        {
            _functions.Clear();
        }

        /// <summary>
        /// Remove a function.
        /// </summary>
        /// <param name="function">
        /// The function name to remove.
        /// </param>
        public void RemoveFunction(string function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            _functions.Remove(function);
        }
    }
}