// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceNotFoundException.cs" company="">
//   
// </copyright>
// <summary>
//   Exception indicating resource not found.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Exception indicating resource not found.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ResourceNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class.
        /// </summary>
        /// <param name="resourceName">
        /// The resource Name.
        /// </param>
        public ResourceNotFoundException(string resourceName)
            : base(string.Format("Resource name {0} not found.", resourceName))
        {
        }
    }
}