// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateToEntityId.cs" company="">
//   
// </copyright>
// <summary>
//   The translate to entity id.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers.Translators
{
    using System;
    using System.Collections.Generic;

    using Test.Api.Business;

    /// <summary>
    ///     The translate to entity id.
    /// </summary>
    internal class TranslateToEntityId : ITranslator
    {
        /// <summary>
        /// The translations.
        /// </summary>
        private static readonly IDictionary<Type, Func<object, EntityId>> Translations =
            new Dictionary<Type, Func<object, EntityId>>
                {
                    { typeof(Guid), source => new EntityId((Guid)source) }, 
                    {
                        typeof(string), 
                        source => SafeCreateEntityId((string)source)
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

            throw new InvalidTranslationException(sourceType, typeof(EntityId));
        }

        /// <summary>
        /// The safe create entity id.
        /// </summary>
        /// <param name="entityId">
        /// The entity id.
        /// </param>
        /// <returns>
        /// The <see cref="EntityId"/>.
        /// </returns>
        private static EntityId SafeCreateEntityId(string entityId)
        {
            return string.IsNullOrEmpty(entityId) ? EntityId.Empty : new EntityId(new Guid(entityId));
        }
    }
}