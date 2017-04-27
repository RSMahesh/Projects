// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateToLong.cs" company="">
//   
// </copyright>
// <summary>
//   The translate to long.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Translators
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The translate to long.
    /// </summary>
    public class TranslateToLong : ITranslator
    {
        /// <summary>
        /// The type to translate to.
        /// </summary>
        private static readonly Type TypeToTranslateTo = typeof(long);

        /// <summary>
        /// The translations.
        /// </summary>
        private static readonly IDictionary<Type, Func<object, long>> Translations =
            new Dictionary<Type, Func<object, long>> { // {
                                                         // typeof(SizeOnDisk), 
                                                         // source => ((SizeOnDisk)source).SizeIn(DiskSizeUnit.Byte)
                                                         // }, 
                                                         // {
                                                         // typeof(RecordVersion), 
                                                         // source => long.Parse(source.ToString())
                                                         { typeof(string), source => long.Parse((string)source) }
                    
                                                         // }, 
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

            throw new InvalidTranslationException(sourceType, TypeToTranslateTo);
        }
    }
}