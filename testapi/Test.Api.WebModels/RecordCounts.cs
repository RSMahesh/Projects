
namespace Test.Api.WebModels
{
    using System.Runtime.Serialization;

    using Test.Api.Core;

    /// <summary>
    /// Wraps the counts needed for the <see cref="ResponseEnvelope{T}"/>.
    /// </summary>
    [DataContract]
    public class RecordCounts : IRecordCounts
    {
        /// <summary>
        /// Gets or sets the total records found.
        /// </summary>
        [DataMember(Name = "totalRecordsFound")]
        public int TotalRecordsFound { get; set; }

        /// <summary>
        /// Gets or sets the index of the starting record.
        /// </summary>
        [DataMember(Name = "startingRecord")]
        public int StartingRecord { get; set; }

        /// <summary>
        /// Gets or sets the number of records returned.
        /// </summary>
        [DataMember(Name = "recordsReturned")]
        public int RecordsReturned { get; set; }
    }
}