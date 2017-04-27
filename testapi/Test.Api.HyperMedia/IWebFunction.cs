// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebFunction.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The WebFunction interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.HyperMedia
{
    using System.Collections.Generic;

    /// <summary>
    /// The WebFunction interface.
    /// </summary>
    public interface IWebFunction
    {
        /// <summary>
        /// Gets the relationship of the web call.
        /// </summary>
        string Relation { get; }

        /// <summary>
        /// Gets the relative Url of the web call.
        /// </summary>
        string Href { get; }

        /// <summary>
        /// Gets the hints for calling the function.
        /// </summary>
        IReadOnlyDictionary<string, object> Hints { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IWebFunction"/> is a template.
        /// </summary>
        /// <value>
        ///   <c>true</c> if Url is a template; otherwise, <c>false</c>.
        /// </value>
        bool Templated { get; }
    }
}