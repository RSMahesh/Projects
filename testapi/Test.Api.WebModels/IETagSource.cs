// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IETagSource.cs" company="">
//   
// </copyright>
// <summary>
//   The ETagRecord interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.WebModels
{
    using System;

    /// <summary>
    /// The ETagRecord interface.
    /// </summary>
    public interface IETagRecord
    {
        /// <summary>
        /// Gets the modified at.
        /// </summary>
        DateTime ModifiedAt { get; }
    }

    /// <summary>
    /// The ETagSource interface.
    /// </summary>
    public interface IETagSource
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the record.
        /// </summary>
        IETagRecord Record { get; }
    }
}