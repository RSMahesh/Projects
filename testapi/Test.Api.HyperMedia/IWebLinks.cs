// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebLinks.cs" company="">
//   
// </copyright>
// <summary>
//   Class implementation for IWebLinks.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.HyperMedia
{
    using System.Collections.Generic;

    /// <summary>
    /// The WebLinks interface.
    /// </summary>
    public interface IWebLinks
    {
        /// <summary>
        ///     Function pointing to this instance.
        /// </summary>
        string Self { get; set; }

        /// <summary>
        ///     Gets the links.
        /// </summary>
        /// <value>
        ///     The links.
        /// </value>
        IEnumerable<string> Functions { get; }

        /// <summary>
        /// Adds the function.
        /// </summary>
        /// <param name="function">
        /// The function.
        /// </param>
        void AddFunction(string function);

        /// <summary>
        ///     Clears the links.
        /// </summary>
        void ClearFunctions();

        /// <summary>
        /// Removes the links.
        /// </summary>
        /// <param name="function">
        /// The function.
        /// </param>
        void RemoveFunction(string function);
    }
}