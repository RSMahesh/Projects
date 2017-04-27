// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResponseEnvelope.cs" company="">
//   
// </copyright>
// <summary>
//   Wraps common information around the data returned from the API.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Test.Api.Business;

namespace Test.Api.WebModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.Serialization;

    
    using Test.Api.Core;

    /// <summary>
    /// Wraps common information around the data returned from the API.
    /// </summary>
    /// <typeparam name="T">
    /// <see cref="Type"/> of data to be returned in the <see cref="ResponseEnvelope{T}"/>.
    /// </typeparam>
    [DataContract]
    public class ResponseEnvelope<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseEnvelope{T}"/> class.
        /// </summary>
        public ResponseEnvelope()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            this.ApiVersion = fileVersionInfo.ProductVersion;
        }

        /// <summary>
        /// Gets the version of the API being called.
        /// </summary>
        [DataMember(Name = "apiVersion")]
        public string ApiVersion { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="MetaDataEnvelope"/> of the request.
        /// </summary>
        [DataMember(Name = "meta")]
        public IMetaDataEnvelope Meta { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DebugInformation"/> for the API call.
        /// </summary>
        [DataMember(Name = "debug", EmitDefaultValue = false)]
        public IDebugInformation Debug { get; set; }

        /// <summary>
        /// Gets or sets the wrapper for the counts for this request.
        /// </summary>
        [DataMember(Name = "recordCount", EmitDefaultValue = false)]
        public IRecordCounts RecordCount { get; set; }

        /// <summary>
        /// Gets or sets the warnings created by this request.
        /// </summary>
        [DataMember(Name = "warnings", EmitDefaultValue = false)]
        public IEnumerable<string> Warnings { get; set; }

        /// <summary>
        /// Gets or sets the warnings created by this request.
        /// </summary>
        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Gets or sets the object to be returned for the request.
        /// </summary>
        [DataMember(Name = "payload", EmitDefaultValue = false)]
        public T Payload { get; set; }
    }
}