// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateToNullableDateTime.cs" company="">
//   
// </copyright>
// <summary>
//   The translate to nullable date time.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Translators
{
    using System;

    /// <summary>
    /// The translate to nullable date time.
    /// </summary>
    public class TranslateToNullableDateTime : ITranslator
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
            return Equals(source, null) ? (DateTime?)null : DateTime.Parse(source.ToString());
        }
    }
}