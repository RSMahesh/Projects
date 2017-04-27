// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITranslator.cs" company="">
//   
// </copyright>
// <summary>
//   The Translator interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Translators
{
    /// <summary>
    ///     The Translator interface.
    /// </summary>
    internal interface ITranslator
    {
        /// <summary>
        /// The from.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object From<TSource>(TSource source);
    }
}