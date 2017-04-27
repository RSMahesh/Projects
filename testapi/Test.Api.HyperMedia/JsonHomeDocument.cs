// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonHomeDocument.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Represents a JsonHomeDocument class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.HyperMedia
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a JsonHomeDocument.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Json is a type.")]
    [DataContract]
    public class JsonHomeDocument
    {
        private readonly IDictionary<string, IWebFunction> _functions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonHomeDocument"/> class.
        /// </summary>
        public JsonHomeDocument()
        {
            this._functions = new Dictionary<string, IWebFunction>();
        }

        /// <summary>
        /// Gets the functions.
        /// </summary>
        /// <value>
        /// The functions.
        /// </value>
        [DataMember(Name = "functions")]
        public IReadOnlyDictionary<string, IWebFunction> Functions
        {
            get
            {
                return new ReadOnlyDictionary<string, IWebFunction>(this._functions);
            }
        }

        /// <summary>
        /// Adds a function to the document.
        /// </summary>
        /// <param name="name">Name of the function.</param>
        /// <param name="function">The function to add.</param>
        public void AddFunction(string name, IWebFunction function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            this._functions[name] = function;
        }

        /// <summary>
        /// Clears the functions.
        /// </summary>
        public void ClearFunctions()
        {
            this._functions.Clear();
        }

        /// <summary>
        /// Removes a function from the document.
        /// </summary>
        /// <param name="name">Name of the function.</param>
        public void RemoveFunction(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this._functions.Remove(name);
        }
    }
}
