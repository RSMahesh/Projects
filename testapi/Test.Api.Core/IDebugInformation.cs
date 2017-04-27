// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDebugInformation.cs" company="">
//   
// </copyright>
// <summary>
//   Defines behavior for classes reporting debug information.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines behavior for classes reporting debug information.
    /// </summary>
    public interface IDebugInformation
    {
        /// <summary>
        /// Gets the timings of each step in the call to the API.
        /// </summary>
        [DataMember(Name = "timings", EmitDefaultValue = false)]
        IReadOnlyCollection<EventTime> Timings { get; }

        /// <summary>
        /// Gets or sets the internal representation of the query passed into a request.
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        IQuery Filters { get; set; }

        /// <summary>
        /// Add a <see cref="EventTime"/> to the eventTime collection.
        /// </summary>
        /// <param name="eventTime">
        /// Event to add.
        /// </param>
        void AddTimingEvent(EventTime eventTime);
    }
}