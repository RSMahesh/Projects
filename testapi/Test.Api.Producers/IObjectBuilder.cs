// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObjectBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The ObjectBuilder interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers
{
    using System;

    /// <summary>
    /// The ObjectBuilder interface.
    /// </summary>
    public interface IObjectBuilder
    {
        /// <summary>
        /// Gets the builds object for.
        /// </summary>
        Type BuildsObjectFor { get; }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="args">
        /// The arguments needed to create the object.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Build<T>(params object[] args);
    }
}