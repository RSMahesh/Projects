// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTime.cs" company="">
//   
// </copyright>
// <summary>
//   Logs timing events for the .
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    /// <summary>
    /// Wrapper for recording elapsed time checkpoints throughout the call.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [DataContract]
    public sealed class EventTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventTime"/> class.
        /// </summary>
        /// <param name="description">
        /// Description of the timer event.
        /// </param>
        /// <param name="elapsedTime">
        /// Elapsed Time when the event took place.
        /// </param>
        public EventTime(string description, long elapsedTime)
        {
            this.Description = description;
            this.ElapsedTimeInMS = elapsedTime;
        }

        /// <summary>
        /// Gets the description of the timer event.
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; private set; }

        /// <summary>
        /// Gets the elapsed time in the call of this event.
        /// </summary>
        /// <remarks>
        /// Elapsed time starts in the Handler.
        /// </remarks>
        [DataMember(Name = "elapsedTimeInMS", EmitDefaultValue = false)]
        public long ElapsedTimeInMS { get; private set; }

        /// <summary>
        /// Returns a string that represents the EventTime.
        /// </summary>
        /// <returns>
        /// String that represents the EventTime.
        /// </returns>
        public override string ToString()
        {
            return string.Format(@"{0}({1})", this.Description, this.ElapsedTimeInMS);
        }
    }
}