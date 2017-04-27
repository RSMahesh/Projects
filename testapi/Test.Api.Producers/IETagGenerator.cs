// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IETagGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Contract for classes that will generate ETags for a response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Exposes type of object the ETag hash can be generated for.
    /// </summary>
    public interface IETagGenerator
    {
        /// <summary>
        /// Indicate the type the ETagGenerator is created for.
        /// </summary>
        Type GeneratesETagsFor { get; }
    }

    /// <summary>
    /// Contract for classes that will generate ETags for a response.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IETagGenerator<in T> : IETagGenerator
    {
        /// <summary>
        /// Generate an ETag for a single entity.
        /// </summary>
        /// <param name="data">
        /// Entity to generate the ETag for.
        /// </param>
        /// <param name="apiVersion">
        /// Current runtime version of the API.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GenerateETag(T data, string apiVersion);

        /// <summary>
        /// Generate an ETag for a list of entities.
        /// </summary>
        /// <param name="entityList">
        /// List of entities to generate the ETag for.
        /// </param>
        /// <param name="apiVersion">
        /// Current runtime version of the API.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GenerateETag(IEnumerable<T> entityList, string apiVersion);
    }
}