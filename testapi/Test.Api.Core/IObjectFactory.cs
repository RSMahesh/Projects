// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObjectFactory.Core.IObjectFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The ObjectFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Core
{
    /// <summary>
    /// The ObjectFactory interface.
    /// </summary>
    public interface IObjectFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Create<T>(params object[] args);
    }
}