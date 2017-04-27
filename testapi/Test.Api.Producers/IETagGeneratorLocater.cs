// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IETagGeneratorLocater.cs" company="">
//   
// </copyright>
// <summary>
//   Definition of a locater to find ETagGenerators for different types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers
{
    /// <summary>
    /// Definition of a locater to find ETagGenerators for different types.
    /// </summary>
    public interface IETagGeneratorLocater
    {
        /// <summary>
        /// Returns an ETagGenerator instance for the type of T.
        /// </summary>
        /// <typeparam name="T">Type to return the ETagGenerator for.</typeparam>
        /// <returns>Instance of ETagGenerator{T}.</returns>
        IETagGenerator<T> Find<T>();
    }
}