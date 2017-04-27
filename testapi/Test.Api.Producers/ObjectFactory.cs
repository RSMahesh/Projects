// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectFactory.cs" company="">
//   
// </copyright>
// <summary>
//   Factory for creating objects within the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Test.Api.Core;
    using Test.Api.Producers.Builders.Objects;

    /// <summary>
    /// Factory for creating objects within the application.
    /// </summary>
    public class ObjectFactory : IObjectFactory
    {
        /// <summary>
        /// The general object builder.
        /// </summary>
        private static readonly GeneralObjectBuilder GeneralObjectBuilder = new GeneralObjectBuilder();

        /// <summary>
        /// The _builders.
        /// </summary>
        private readonly IDictionary<Type, IObjectBuilder> _builders;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFactory"/> class.
        /// </summary>
        /// <param name="builders">
        /// The builders.
        /// </param>
        public ObjectFactory(IEnumerable<IObjectBuilder> builders)
        {
            this._builders = builders.ToDictionary(builder => builder.BuildsObjectFor);
        }

        /// <summary>
        /// Create an object using reflection.
        /// </summary>
        /// <param name="args">
        /// Arguments to be passed into the constructor.
        /// </param>
        /// <typeparam name="T">
        /// <see cref="Type"/> of object to create.
        /// </typeparam>
        /// <returns>
        /// An instance of <see cref="Type"/> T.
        /// </returns>
        public T Create<T>(params object[] args)
        {
            var destinationType = typeof(T);

            return this._builders.ContainsKey(destinationType)
                       ? this._builders[destinationType].Build<T>(args)
                       : GeneralObjectBuilder.Build<T>(args);
        }
    }
}