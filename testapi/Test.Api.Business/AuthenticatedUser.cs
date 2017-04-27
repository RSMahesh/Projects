// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticatedUser.cs" company="">
//   
// </copyright>
// <summary>
//   The authenticated user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;

    using Test.Api.Core;

    /// <summary>
    /// The authenticated user.
    /// </summary>
    public sealed class AuthenticatedUser : User
    {
        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The _empty instance.
        /// </summary>
        private static volatile AuthenticatedUser _emptyInstance;

        /// <summary>
        /// The _permissions.
        /// </summary>
        private readonly IList<PermissionSet> _permissions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUser"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="username">
        /// The Username.
        /// </param>
        /// <param name="permissions">
        /// The permissions.
        /// </param>
        public AuthenticatedUser(EntityId id, EmailAddress username, IEnumerable<PermissionSet> permissions)
            : base(id, username)
        {
            Ensure.Argument.IsNotNull(permissions, "permissions");

            this._permissions = permissions.ToList();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuthenticatedUser"/> class from being created.
        /// </summary>
        private AuthenticatedUser()
        {
            this._permissions = new List<PermissionSet>();
        }

        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>
        /// The empty.
        /// </value>
        public static new AuthenticatedUser Empty
        {
            get
            {
                return BuildOrRetrieveEmptyInstance();
            }
        }

        /// <summary>
        /// Gets the user's permissions.
        /// </summary>
        /// <value>
        /// Permissions for the user.
        /// </value>
        public IReadOnlyCollection<PermissionSet> Permissions
        {
            get
            {
                return this._permissions.ToReadOnlyCollection();
            }
        }

        /// <summary>
        /// The build or retrieve empty instance.
        /// </summary>
        /// <returns>
        /// The <see cref="AuthenticatedUser"/>.
        /// </returns>
        private static AuthenticatedUser BuildOrRetrieveEmptyInstance()
        {
            if (_emptyInstance == null)
            {
                lock (Locker)
                {
                    // double check in case _emptyInstance was instantiated while this thread was locked.
                    if (_emptyInstance == null)
                    {
                        _emptyInstance = new AuthenticatedUser();
                    }
                }
            }

            return _emptyInstance;
        }
    }
}