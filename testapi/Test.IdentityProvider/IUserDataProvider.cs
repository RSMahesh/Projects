// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserDataProvider.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The IUserDataProvider interface illustrates the common method related to user information and authentication.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.IdentityProvider
{
    using System.Threading.Tasks;

    using Test.Api.Business;

    /// <summary>
    /// The IUserDataProvider interface illustrates the common method related to user information and authentication.
    /// </summary>
    public interface IUserDataProvider
    {
        /// <summary>
        /// Method to authenticate the users.
        /// </summary>
        /// <param name="username">The login name of the user.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>User Information.</returns>
        Task<AuthenticatedUser> AuthenticateAsync(string username, string password);
    }
}
