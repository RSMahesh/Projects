// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidPasswordException.cs" company="">
//   
// </copyright>
// <summary>
//   Exception indicating an invalid password has been entered.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System;

    /// <summary>
    ///   Exception indicating an invalid password has been entered.
    /// </summary>
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordException"/> class.
        /// </summary>
        /// <param name="message">
        /// String description of the validation rules that were violated.
        /// </param>
        public InvalidPasswordException(string message)
            : base(message)
        {
        }
    }
}