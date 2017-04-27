// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateDataType.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The translate data type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers.Translators
{
    using System;
    using System.Collections.Generic;

    using Test.Api.Business;
    using Test.Api.Core;

    /// <summary>
    /// The translate data type.
    /// </summary>
    internal static class TranslateDataType
    {
        private static readonly IDictionary<Type, ITranslator> Translators = new Dictionary<Type, ITranslator>
        {
            { typeof(EntityId), new TranslateToEntityId() }, 
            { typeof(Guid), new TranslateToGuid() }, 
            { typeof(Guid?), new TranslateToNullableGuid() },
            { typeof(SafeString), new TranslateToSafeString() }, 
            { typeof(string), new TranslateToString() },
            { typeof(long), new TranslateToLong() },
            { typeof(DateTime?), new TranslateToNullableDateTime() }
//            { typeof(AssetStatus), new TranslateToEnum<AssetStatus>() }
        };

        /// <summary>
        /// The translate.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="destinationType">
        /// The destination type.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object Translate<TSource>(TSource source, Type destinationType)
        {
        
            if (Translators.ContainsKey(destinationType))
            {
                return Translators[destinationType].From(source);
            }

            if (destinationType.IsPrimitive)
            {
                return Convert.ChangeType(source, destinationType);
            }
          
            var objectMapper = Producers.Translators.Translate.From(source);
            var methodInfo = objectMapper.GetType().GetMethod("To");
            var generic = methodInfo.MakeGenericMethod(destinationType);
            return generic.Invoke(objectMapper, null);
            
        }
    }
}