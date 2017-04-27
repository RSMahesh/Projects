// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidEmailAddressException.cs" company="">
//   
// </copyright>
// <summary>
//   Exception indicating an invalid Username Address has been entered.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System;

    /// <summary>
    /// Exception indicating an invalid Username Address has been entered.
    /// </summary>
    [Serializable]
    public class InvalidEmailAddressException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEmailAddressException"/> class.
        /// </summary>
        /// <param name="message">
        /// String description of the validation rules that were violated.
        /// </param>
        public InvalidEmailAddressException(string message)
            : base(message)
        {
        }
    }
}