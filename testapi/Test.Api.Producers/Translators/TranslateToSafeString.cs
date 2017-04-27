// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateToSafeString.cs" company="">
//   
// </copyright>
// <summary>
//   The translate to safe string.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Translators
{
    using System;
    using System.Collections.Generic;

    using Test.Api.Core;

    /// <summary>
    ///     The translate to safe string.
    /// </summary>
    internal class TranslateToSafeString : ITranslator
    {
        /// <summary>
        /// The translations.
        /// </summary>
        private static readonly IDictionary<Type, Func<object, SafeString>> Translations =
            new Dictionary<Type, Func<object, SafeString>>
                {
                    {
                        typeof(string), 
                        source => new SafeString((string)source)
                    }
                };

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
        /// <exception cref="InvalidTranslationException">
        /// </exception>
        public object From<TSource>(TSource source)
        {
            var sourceType = source.GetType();
            if (Translations.ContainsKey(sourceType))
            {
                return Translations[sourceType](source);
            }

            throw new InvalidTranslationException(sourceType, typeof(SafeString));
        }
    }
}