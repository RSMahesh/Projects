// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetaDataEnvelope.cs" company="">
//   
// </copyright>
// <summary>
//   Contains information to be send back to the client that is not part of the requested information.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    using Test.Api.Core;

    /// <summary>
    /// Contains information to be send back to the client that is not part of the requested information.
    /// </summary>
    [DataContract]
    [KnownType(typeof(ReadOnlyDictionary<string, object>))]
    public class MetaDataEnvelope : IMetaDataEnvelope
    {
        /// <summary>
        /// The _meta information.
        /// </summary>
        private readonly IDictionary<string, object> _metaInformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataEnvelope"/> class.
        /// </summary>
        public MetaDataEnvelope()
        {
            this._metaInformation = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets additional information to be passed back from the API.
        /// </summary>
        [DataMember(Name = "metaInformation")]
        public IReadOnlyDictionary<string, object> MetaInformation
        {
            get
            {
                return new ReadOnlyDictionary<string, object>(this._metaInformation);
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets the date and time this <see cref="MetaDataEnvelope"/> object was created.
        /// </summary>
        [ExcludeFromCodeCoverage]
        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt
        {
            get
            {
                return DateTime.UtcNow;
            }

            set
            {
            }
        }

        /// <summary>
        /// Add a key/value to the MetaDataEnvelope Dictionary.
        /// </summary>
        /// <param name="key">
        /// The key of the object to be added.
        /// </param>
        /// <param name="value">
        /// The value of the object to be added.
        /// </param>
        public void AddMetaDataInformation(string key, object value)
        {
            this._metaInformation[key] = value;
        }
    }
}