// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugInformation.cs" company="">
//   
// </copyright>
// <summary>
//   Keep track of any information needed to debug the API.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Test.Api.Core;

    /// <summary>
    /// Keep track of any information needed to debug the API.
    /// </summary>
    /// <remarks>
    /// This information will only be shown to people who have application permission to view it.
    /// </remarks>
    [DataContract]
    public class DebugInformation : IDebugInformation
    {
        /// <summary>
        /// The _event times.
        /// </summary>
        private readonly ConcurrentStack<EventTime> _eventTimes;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugInformation"/> class.
        /// </summary>
        public DebugInformation()
        {
            this._eventTimes = new ConcurrentStack<EventTime>();
        }

        /// <summary>
        /// Gets the timings of each step in the call to the API.
        /// </summary>
        [DataMember(Name = "timings", EmitDefaultValue = false)]
        public IReadOnlyCollection<EventTime> Timings
        {
            get
            {
                return this._eventTimes.ToReadOnlyCollection();
            }
        }

        /// <summary>
        /// Gets or sets the internal representation of the query passed into a request.
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public IQuery Filters { get; set; }

        /// <summary>
        /// Add a <see cref="EventTime"/> to the event collection.
        /// </summary>
        /// <param name="eventTime">
        /// Event to add.
        /// </param>
        public void AddTimingEvent(EventTime eventTime)
        {
            this._eventTimes.Push(eventTime);
        }
    }
}