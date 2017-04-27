// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Translate.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The translate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers.Translators
{
    using System;
    using System.Reflection;

    using Test.Api.Core;

    /// <summary>
    /// The translate.
    /// </summary>
    public static class Translate
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
        /// The <see cref="ObjectMapper{TSource}"/>.
        /// </returns>
        public static ObjectMapper<TSource> From<TSource>(TSource source)
        {
            Ensure.Argument.IsNotNull(source, "source");

            return new ObjectMapper<TSource>(source);
        }

        /// <summary>
        /// The object mapper.
        /// </summary>
        /// <typeparam name="TSource">
        /// </typeparam>
        public class ObjectMapper<TSource>
        {
            private readonly PropertyMapper _propertyMapper;
            private readonly object _source;

            /// <summary>
            /// Initializes a new instance of the <see cref="ObjectMapper{TSource}"/> class.
            /// </summary>
            /// <param name="source">
            /// The source.
            /// </param>
            public ObjectMapper(TSource source)
            {
                this._source = source;
                this._propertyMapper = new PropertyMapper();
            }

            /// <summary>
            /// The to.
            /// </summary>
            /// <typeparam name="TDestination">
            /// </typeparam>
            /// <returns>
            /// The <see cref="TDestination"/>.
            /// </returns>
            public TDestination To<TDestination>() where TDestination : class
            {
                var destinationType = typeof(TDestination);

                var destination = Activator.CreateInstance<TDestination>();

                this.EnumeratePropertiesAndSetValues(destinationType, destination);

                return destination;
            }

            public TDestination MappProperties<TDestination>(TDestination destination) where TDestination : class
            {
                var destinationType = typeof(TDestination);
               
                this.EnumeratePropertiesAndSetValues(destinationType, destination);

                return destination;
            }


            private static object SafeGetValue(PropertyInfo sourceProperty, object source)
            {
                var sourceValue = sourceProperty.GetValue(source);

                if (sourceValue != null)
                {
                    return sourceValue;
                }

                var sourceType = source.GetType();
                return sourceType.IsValueType ? Activator.CreateInstance(sourceType) : null;
            }

            private void EnumeratePropertiesAndSetValues(Type destinationType, object destination)
            {
                var sourceType = this._source.GetType();

                var destinationProperties = destinationType.GetProperties();
                foreach (var destinationProperty in destinationProperties)
                {
                    var sourcePropertyName = this._propertyMapper.DeterminePropertyInSource(
                        sourceType,
                        destinationProperty.Name,
                        destinationType);

                    if (IgnoreSourceProperty(sourcePropertyName) )
                    {
                        continue;
                    }
                        
                    var destinationPropertyType = destinationProperty.PropertyType;

                    if (destinationPropertyType.IsInterface)
                    {
                        continue;
                    }

                    var destinationValue = this.DetermineDestinationValue(sourcePropertyName, destinationPropertyType);

                    destinationProperty.SetValue(destination, destinationValue);
                }
            }

            private static bool IgnoreSourceProperty(string sourcePropertyName)
            {
                return sourcePropertyName.IsNullOrEmpty() || 
                    sourcePropertyName.ToLower() == "attributes" || 
                    sourcePropertyName.ToLower() == "smil" || 
                    sourcePropertyName.ToLower() == "relatedassets";
            }

            private object DetermineDestinationValue(string sourcePropertyName, Type destinationPropertyType)
            {
                var sourceProperty = this._source.GetType().GetProperty(sourcePropertyName);
                var sourceValue = SafeGetValue(sourceProperty, this._source);
                if (sourceValue == null)
                {
                    return null;
                }

                var sourcePropertyType = sourceProperty.PropertyType;

                return CanAssignDirectly(sourcePropertyType, destinationPropertyType, sourceValue)
                    ? sourceValue
                    : TranslateDataType.Translate(sourceValue, destinationPropertyType);
            }

            private static bool CanAssignDirectly(Type sourceType, Type destinationPropertyType, object sourceValue)
            {
                return sourceValue.GetType() == destinationPropertyType || sourceType.IsAssignableFrom(destinationPropertyType);
            }
        }
    }
}