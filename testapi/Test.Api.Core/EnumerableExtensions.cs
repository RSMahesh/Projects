// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Extension methods for <see cref="System.Collections.Generic.IEnumerable{T}" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Test.Api.Core
{
    /// <summary>
    /// Extension methods for <see cref="System.Collections.Generic.IEnumerable{T}"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Performs an action on each value of the enumerable.
        /// </summary>
        /// <typeparam name="T">
        /// Type of object for the <see cref="IEnumerable{T}"/>.
        /// </typeparam>
        /// <param name="enumerable">
        /// Sequence on which to perform action.
        /// </param>
        /// <param name="action">
        /// Action to perform on every item.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when given null <paramref name="enumerable"/> or
        ///     <paramref name="action"/>.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Ensure.Argument.IsNotNull(enumerable, "enumerable");
            Ensure.Argument.IsNotNull(action, "action");

            foreach (var value in enumerable)
            {
                action(value);
            }
        }

        /// <summary>
        /// Convenience method for retrieving a specific page of items within a collection.
        /// </summary>
        /// <typeparam name="T">
        /// Type of object for the <see cref="IEnumerable{T}"/>.
        /// </typeparam>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="pageIndex">
        /// The index of the page to get.
        /// </param>
        /// <param name="pageSize">
        /// The size of the pages.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            Ensure.Argument.IsNotNull(source, "source");
            Ensure.Argument.Is(pageIndex >= 0, "The page index cannot be negative.");
            Ensure.Argument.Is(pageSize > 0, "The page size must be greater than zero.");

            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Converts an enumerable into a readonly collection.
        /// </summary>
        /// <typeparam name="T">
        /// Type of object for the <see cref="IEnumerable{T}"/>.
        /// </typeparam>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ReadOnlyCollection<T>(enumerable.ToList());
        }

        /// <summary>
        /// Validates that the <paramref name="enumerable"/> is not null and contains items.
        /// </summary>
        /// <typeparam name="T">
        /// Type of object for the <see cref="IEnumerable{T}"/>.
        /// </typeparam>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <typeparam name="T">
        /// Type of object for the <see cref="IEnumerable{T}"/>.
        /// </typeparam>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="separator">
        /// The separator.
        /// </param>
        /// <returns>
        /// A string that consists of the members of <paramref name="values"/> delimited by the
        ///     <paramref name="separator"/> string. If values has no members, the method returns null.
        /// </returns>
        public static string JoinOrDefault<T>(this IEnumerable<T> values, string separator)
        {
            Ensure.Argument.NotNullOrEmpty(separator, "separator");

            return values == null ? default(string) : string.Join(separator, values);
        }

        /// <summary>
        /// Convert a byte list representing a hash value to a string.
        /// </summary>
        /// <param name="hash">List of bytes representing the hash value.</param>
        /// <returns>The string representation of the hash.</returns>
        public static string HashToString(this IEnumerable<byte> hash)
        {
            var sb = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            foreach (var item in hash)
            {
                sb.Append(item.ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sb.ToString();
        }
    }
}