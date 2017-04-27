// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="">
//   
// </copyright>
// <summary>
//   Extension methods for string.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    using System.Linq;

    /// <summary>
    /// Extension methods for string.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Checks the string for Numeric value.
        /// </summary>
        /// <param name="value">
        /// String - String which needs to scan.
        /// </param>
        /// <returns>
        /// True if contains numeric otherwise return false.
        /// </returns>
        public static bool HasNumeric(this string value)
        {
            return value.Any(char.IsNumber);
        }

        /// <summary>
        /// The is whole number.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsWholeNumber(this string value)
        {
            return value.All(char.IsNumber);
        }

        /// <summary>
        /// The to safe string.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="SafeString"/>.
        /// </returns>
        public static SafeString ToSafeString(this string value)
        {
            return string.IsNullOrEmpty(value) ? SafeString.Empty : new SafeString(value);
        }
    }
}