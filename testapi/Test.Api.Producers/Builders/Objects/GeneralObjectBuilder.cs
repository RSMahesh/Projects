// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneralObjectBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The general object builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Builders.Objects
{
    using System;

    using Test.Api.Producers.Translators;

    /// <summary>
    /// The general object builder.
    /// </summary>
    public class GeneralObjectBuilder
    {
        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Build<T>(params object[] args)
        {
          
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}