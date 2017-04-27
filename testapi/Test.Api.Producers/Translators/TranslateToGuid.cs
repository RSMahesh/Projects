// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateToGuid.cs" company="">
//   
// </copyright>
// <summary>
//   The translate to guid.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Translators
{
    using System;
    using System.Collections.Generic;

    using Test.Api.Business;

    /// <summary>
    ///     The translate to Guid.
    /// </summary>
    internal class TranslateToGuid : ITranslator
    {
        /// <summary>
        /// The type to translate to.
        /// </summary>
        private static readonly Type TypeToTranslateTo = typeof(Guid);

        /// <summary>
        /// The translations.
        /// </summary>
        private static readonly IDictionary<Type, Func<object, Guid>> Translations =
            new Dictionary<Type, Func<object, Guid>>
                {
                    { typeof(EntityId), source => ((EntityId)source).ToGuid() }, 
                    { typeof(string), source => Guid.Parse((string)source) }
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