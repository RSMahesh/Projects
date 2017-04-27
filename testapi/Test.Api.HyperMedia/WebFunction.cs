// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebFunction.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The web function.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.HyperMedia
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a Function in the <see cref="JsonHomeDocument" />.
    /// </summary>
    [DataContract]
    public class WebFunction : IWebFunction
    {
        private readonly IDictionary<string, object> _hints;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebFunction"/> class.
        /// </summary>
        /// <param name="href">
        /// The relative url for the function.
        /// </param>
        /// <param name="relation">
        /// The relation.
        /// </param>
        /// <param name="templated">
        /// Flag indicating whether the function is a template.
        /// </param>
        /// <param name="hints">
        /// The hints.
        /// </param>
        public WebFunction(string href, string relation, bool templated, IDictionary<string, object> hints, ulong requiredPermissions)
        {
            this.Templated = templated;
            this.Relation = relation;
            this.Href = href;
            this._hints = hints;
            this.RequiredPermissions = requiredPermissions;
        }

        /// <summary>
        /// Gets the relationship of the web call.
        /// </summary>
        [DataMember(Name = "rel")]
        public string Relation { get; private set; }

        /// <summary>
        /// Gets the relative Url of the web call.
        /// </summary>
        [DataMember(Name = "href")]
        public string Href { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IWebFunction"/> is a template.
        /// </summary>
        /// <value>
        /// <c>true</c> if the url is a template; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "templated")]
        public bool Templated { get; private set; }

        /// <summary>
        /// Permissions required to use this function.
        /// </summary>
        public ulong RequiredPermissions { get; private set; }

        /// <summary>
        /// Gets the hints for calling the function.
        /// </summary>
        [DataMember(Name = "hints")]
        public IReadOnlyDictionary<string, object> Hints
        {
            get { return new ReadOnlyDictionary<string, object>(this._hints); }
        }
    }
}