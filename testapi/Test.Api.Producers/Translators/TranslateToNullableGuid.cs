// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateToNullableGuid.cs" company="">
//   
// </copyright>
// <summary>
//   The translate to guid.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Translators
{
    using System;

    /// <summary>
    ///     The translate to guid?.
    /// </summary>
    internal class TranslateToNullableGuid : ITranslator
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
            return Equals(source, null) ? (Guid?)null : new Guid(source.ToString());
        }
    }
}