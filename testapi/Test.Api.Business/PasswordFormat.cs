// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordFormat.cs" company="">
//   
// </copyright>
// <summary>
//   The password format.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    /// <summary>
    /// The password format.
    /// </summary>
    public enum PasswordFormat
    {
        /// <summary>
        /// The clear.
        /// </summary>
        Clear, 

        /// <summary>
        /// The hashed.
        /// </summary>
        Hashed, 

        /// <summary>
        /// The encrypted.
        /// </summary>
        Encrypted
    }
}