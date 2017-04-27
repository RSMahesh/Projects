// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRecordCounts.cs" company="">
//   
// </copyright>
// <summary>
//   Contract for dealing with record counts in the response object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Contract for dealing with record counts in the response object.
    /// </summary>
    public interface IRecordCounts
    {
        /// <summary>
        /// Gets or sets the total records found.
        /// </summary>
        [DataMember(Name = "totalRecordsFound")]
        int TotalRecordsFound { get; set; }

        /// <summary>
        /// Gets or sets the index of the starting record.
        /// </summary>
        [DataMember(Name = "startingRecord")]
        int StartingRecord { get; set; }

        /// <summary>
        /// Gets or sets the number of records returned.
        /// </summary>
        [DataMember(Name = "recordsReturned")]
        int RecordsReturned { get; set; }
    }
}