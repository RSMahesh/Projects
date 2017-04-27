// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateToString.cs" company="">
//   
// </copyright>
// <summary>
//   The translate to string.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Translators
{
    /// <summary>
    ///     The translate to string.
    /// </summary>
    internal class TranslateToString : ITranslator
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
        public object From<TSource>(TSource source)
        {
            return source.ToString();
        }
    }
}