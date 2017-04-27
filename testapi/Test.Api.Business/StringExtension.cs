// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="">
//   
// </copyright>
// <summary>
//   Extension methods for string.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    /// <summary>
    /// Extension methods for string.
    /// </summary>
    public static class StringExtension
    {
        public static EmailAddress ToEmailAddress(this string value)
        {
            return string.IsNullOrEmpty(value) ? EmailAddress.Empty : new EmailAddress(value);
        }
    }
}