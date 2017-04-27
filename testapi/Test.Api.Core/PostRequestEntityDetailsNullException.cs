// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostRequestEntityDetailsNullException.cs" company="">
//   
// </copyright>
// <summary>
//   Exception indicating a Category Details properties are null.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Exception indicating a CategoryDetails properties are null.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PostRequestEntityDetailsNullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostRequestEntityDetailsNullException"/> class.
        /// </summary>
        public PostRequestEntityDetailsNullException()
            : base(string.Format("Required entity details are not supplied in request."))
        {
        }
    }
}