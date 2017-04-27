// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityPatcher.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Apply the instructions from a PATCH request to an entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using Test.Api.Business;

using Newtonsoft.Json;

using Test.Api.Business;

namespace Test.Api.Producers
{
    /// <summary>
    /// Apply the instructions from a PATCH request to an entity.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EntityPatcher : IEntityPatcher
    {
        /// <summary>
        /// Apply the changes in <paramref name="operations"/> to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">
        /// The object to apply the operations too.
        /// </param>
        /// <param name="operations">
        /// A list of operations that was passed in with the PATCH request.
        /// </param>
        /// <param name="context">
        /// Context containing information about the request.
        /// </param>
        public void Patch(object entity, IEnumerable<PatchOperationBase> operations, ITestApiContext context)
        {
            foreach (dynamic operation in operations)
            {
                ApplyOperation(entity, operation, context);
            }
        }

        private static void ApplyOperation<T>(T entity, AddPatchOperation operation, ITestApiContext context)
        {
            var path = new Queue<string>(operation.Path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries));
            var value = operation.Value;

            UpdateField(entity, path, value, context);
        }

        private static void ApplyOperation<T>(T entity, ReplacePatchOperation operation, ITestApiContext context)
        {
            var path = new Queue<string>(operation.Path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries));
            var value = operation.Value;

            UpdateField(entity, path, value, context);
        }

        private static void ApplyOperation<T>(T entity, TestPatchOperation operation, ITestApiContext context)
        {
            var path = new Queue<string>(operation.Path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries));
            var value = operation.Value;

            UpdateField(entity, path, value, context);
        }

        private static void ApplyOperation<T>(T entity, MovePatchOperation operation, ITestApiContext context)
        {
            var path = new Queue<string>(operation.Path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries));
        }

        private static void ApplyOperation<T>(T entity, CopyPatchOperation operation, ITestApiContext context)
        {
            var path = new Queue<string>(operation.Path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries));
        }

        private static void ApplyOperation<T>(T entity, RemovePatchOperation operation, ITestApiContext context)
        {
            var path = new Queue<string>(operation.Path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries));

            UpdateField(entity, path, default(T), context);
        }

        private static void UpdateField(object entity, Queue<string> path, object value, ITestApiContext context)
        {
            var entityType = entity.GetType();
            var propertyName = path.Dequeue();

            var property = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
            {
                context.AddError(
                    string.Format("Property Name {0} does not exist in {1} Model", propertyName, entityType.Name));
                return;
            }

            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                var dictionary = GetDictionaryPropertyValue(entity, path, value, property);
                value = dictionary;
            }

            value = GetCollectionPropertyValue(value, property);

            if (path.Any())
            {
                var propertyValue = property.GetValue(entity);

                UpdateField(propertyValue, path, value, context);
                return;
            }

            var strongTypedValue =
               value is IConvertible
                   ? Convert.ChangeType(value, property.PropertyType)
                   : value;

            property.SetValue(entity, strongTypedValue);
        }

        private static object GetCollectionPropertyValue(object value, PropertyInfo property)
        {
            if (!property.PropertyType.IsGenericType || property.PropertyType.GetGenericTypeDefinition() != typeof(IEnumerable<>))
            {
                return value;
            }

            var itemType = property.PropertyType.GetGenericArguments()[0];

            if (itemType == typeof(string))
            {
                value = JsonConvert.DeserializeObject<IEnumerable<string>>(value.ToString());
            }

            return value;
        }

        private static IDictionary GetDictionaryPropertyValue(object entity, Queue<string> path, object value, PropertyInfo property)
        {
            var dictionary = (IDictionary)property.GetValue(entity);
            var name = path.Dequeue();
            var dKey = Guid.Parse(name);

            dictionary[dKey] = value.ToString();

            return dictionary;
        }
    }
}
