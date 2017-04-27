// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetadataEnvelope.cs" company="">
//   
// </copyright>
// <summary>
//   The MetadataEnvelope interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The MetadataEnvelope interface.
    /// </summary>
    public interface IMetaDataEnvelope
    {
        /// <summary>
        /// Gets additional information to be passed back from the API.
        /// </summary>
        [DataMember(Name = "metaInformation")]
        IReadOnlyDictionary<string, object> MetaInformation { get; }

        /// <summary>
        /// Gets the date and time this <see cref="IMetaDataEnvelope"/> object was created.
        /// </summary>
        [DataMember(Name = "createdAt")]
        DateTime CreatedAt { get; }

        /// <summary>
        /// Add a key/value to the MetadataEnvelope Dictionary.
        /// </summary>
        /// <param name="key">
        /// The key of the object to be added.
        /// </param>
        /// <param name="value">
        /// The value of the object to be added.
        /// </param>
        void AddMetaDataInformation(string key, object value);
    }
}