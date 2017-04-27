// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateToEnum.cs" company="">
//   
// </copyright>
// <summary>
//   Class implementation for TranslateToEnum.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Translators
{
    using System;
    using System.Collections.Generic;

    using Test.Api.Core;

    /// <summary>
    /// The translate to enum.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class TranslateToEnum<T> : ITranslator
        where T : struct, IConvertible
    {
        /// <summary>
        /// The translations.
        /// </summary>
        private static readonly IDictionary<Type, Func<object, T>> Translations =
            new Dictionary<Type, Func<object, T>>
                {
                    { typeof(int), source => (T)source }, 
                    {
                        typeof(string), source =>
                            {
                                var sourceString = (string)source;
                                T status;

                                if (sourceString.IsWholeNumber())
                                {
                                    status = (T)(object)int.Parse(sourceString);
                                }
                                else
                                {
                                    Enum.TryParse(sourceString, true, out status);
                                }

                                return status;
                            }
                    }, 
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
            var destinationType = typeof(T);
            if (Translations.ContainsKey(sourceType) && destinationType.IsEnum)
            {
                return Translations[sourceType](source);
            }

            throw new InvalidTranslationException(sourceType, destinationType);
        }
    }
}