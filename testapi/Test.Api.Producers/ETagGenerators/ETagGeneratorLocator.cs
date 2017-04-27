// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETagGeneratorLocator.cs" company="">
//   
// </copyright>
// <summary>
//   Class implementation for IeTagGeneratorLocater.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers.ETagGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Test.Api.Producers;
    using Test.Api.WebModels;

    /// <summary>
    /// Locate generators to create ETag Hashes.
    /// </summary>
    public class ETagGeneratorLocater : IETagGeneratorLocater
    {
        /// <summary>
        /// The default generator type.
        /// </summary>
        private static readonly Type DefaultGeneratorType = typeof(IETagSource);

        /// <summary>
        /// The _generators.
        /// </summary>
        private readonly IDictionary<Type, IETagGenerator> _generators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ETagGeneratorLocater"/> class. 
        /// Initializes a new instance of the <see cref="QueryParser"/> class.
        /// </summary>
        /// <param name="generators">
        /// List of generators to be able to be searched for.
        /// </param>
        public ETagGeneratorLocater(IEnumerable<IETagGenerator> generators)
        {
            this._generators = generators.ToDictionary(g => g.GeneratesETagsFor);
        }

        /// <summary>
        /// Returns an ETagGenerator instance for the type of T.
        /// </summary>
        /// <typeparam name="T">Type to return the ETagGenerator for.</typeparam>
        /// <returns>Instance of ETagGenerator{T}.</returns>
        public IETagGenerator<T> Find<T>()
        {
            var typeOfT = typeof(T);
            if (this._generators.ContainsKey(typeOfT))
            {
                return (IETagGenerator<T>)this._generators[typeOfT];
            }

            if (!typeOfT.GetInterfaces().Contains(DefaultGeneratorType))
            {
                return null;
            }

            return (IETagGenerator<T>)this._generators[DefaultGeneratorType];
        }
    }
}