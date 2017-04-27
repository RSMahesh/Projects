// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDataProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The UserDataProvider class illustrates the common method implementation of provider's interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.IdentityProvider
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    using Test.Api.Business;
    using Test.Api.Services;

    /// <summary>
    /// The UserDataProvider class illustrates the common method implementation of provider's interface.
    /// </summary>
    public class UserDataProvider : IUserDataProvider
    {
        /// <summary>
        /// The _user service.
        /// </summary>
        private IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataProvider"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service.
        /// </param>
        public UserDataProvider(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Method to authenticate the users.
        /// </summary>
        /// <param name="username">
        /// <see cref="EmailAddress"/> representing a user's EmailAddress.
        /// </param>
        /// <param name="password">
        /// <see cref="Password"/> representing a user's Password.
        /// </param>
        /// <returns>
        /// <see cref="User"/> representing a user class.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public async Task<AuthenticatedUser> AuthenticateAsync(string username, string password)
        {
            if (!this.ValidateUser(username, password))
            {
                return AuthenticatedUser.Empty;
            }

            var user = _userService.GetUser(username, Guid.Empty);

            var userAcls = _userService.GetUserPermissions(user);

            var acls =
                userAcls.Select(a => new PermissionSet((SecurableObjectType)a.SecurableObjectType, a.ID, a.Permissions));

            var authenticatedUser = new AuthenticatedUser(user.Id, new EmailAddress(username), acls)
                                        {
                                            OrgUnitId =
                                                user
                                                .OrgUnitId,
                                            RoleId =
                                                user.RoleId,
                                            EmailAddress =
                                                user
                                                .EmailAddress
                                        };

            return authenticatedUser;
        }

        /// <summary>
        /// The validate user.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public bool ValidateUser(string userName, string password)
        {
            return _userService.ValidateUser(userName, password, Guid.Empty);
        }
    }
}