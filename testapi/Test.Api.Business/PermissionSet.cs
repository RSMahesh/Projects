
namespace Test.Api.Business
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represent permission set of a user of the system.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class PermissionSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionSet"/> class.
        /// </summary>
        /// <param name="securableObjectType">assetType of the securable object.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <param name="permissions">The permissions.</param>
        public PermissionSet(SecurableObjectType securableObjectType, Guid objectId, ulong permissions)
        {
            this.Permissions = permissions;
            this.ObjectId = objectId;
            this.SecurableObjectType = securableObjectType;
        }

        /// <summary>
        /// Gets or sets the type of the securable object.
        /// </summary>
        /// <value>
        /// The type of the securable object.
        /// </value>
        public SecurableObjectType SecurableObjectType { get; private set; }

        /// <summary>
        /// Gets or sets the object identifier.
        /// </summary>
        /// <value>
        /// The object identifier.
        /// </value>
        public Guid ObjectId { get; private set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public ulong Permissions { get; private set; }
    }
}
