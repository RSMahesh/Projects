// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="">
//   
// </copyright>
// <summary>
//   Represents a user of the systemclass.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Test.Api.Core;

    /// <summary>
    /// Represents a user of the system<see cref="User" />class.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The _empty instance.
        /// </summary>
        private static volatile User _emptyInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <param name="username">
        /// The Username.
        /// </param>
        public User(EntityId id, EmailAddress username)
        {
            Ensure.Argument.IsNotNull(id, "id");
            Ensure.Argument.IsNotNull(username, "username");

            this.Id = id;
            this.UserName = username;
        }

        
    
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        protected User()
        {
            this.Id = EntityId.Empty;
            this.UserName = EmailAddress.Empty;
        }

        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>
        /// The empty.
        /// </value>
        public static User Empty
        {
            get
            {
                return BuildOrRetrieveEmptyInstance();
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public EntityId Id { get; private set; }

        /// <summary>
        /// Gets the Username.
        /// </summary>
        /// <value>
        /// The Username.
        /// </value>
        public EmailAddress UserName { get; private set; }

        /// <summary>
        /// Gets or sets the OrganizationUnitId.
        /// </summary>
        public Guid OrgUnitId { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        public Guid? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the user domain.
        /// </summary>
        /// <value>
        /// The user domain.
        /// </value>
        public SafeString UserDomain { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public SafeString FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public SafeString LastName { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public SafeString Department { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public SafeString Position { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public SafeString Title { get; set; }

        /// <summary>
        /// Gets or sets the office number.
        /// </summary>
        /// <value>
        /// The office number.
        /// </value>
        public PhoneNumber OfficeNumber { get; set; }

        /// <summary>
        /// Gets or sets the cellular number.
        /// </summary>
        /// <value>
        /// The cellular number.
        /// </value>
        public PhoneNumber CellularNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax number.
        /// </summary>
        /// <value>
        /// The fax number.
        /// </value>
        public PhoneNumber FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public EmailAddress EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public StreetAddress Address { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public SafeString Comment { get; set; }

        /// <summary>
        /// Gets or sets the status flags.
        /// </summary>
        /// <value>
        /// The status flags.
        /// </value>
        public UserStatusFlags StatusFlags { get; set; }

        /// <summary>
        /// Gets or sets the password information.
        /// </summary>
        /// <value>
        /// The password information.
        /// </value>
        public PasswordInformation PasswordInformation { get; set; }

        /// <summary>
        /// Gets or sets the default skin.
        /// </summary>
        /// <value>
        /// The default skin.
        /// </value>
        public SafeString DefaultSkin { get; set; }

        /// <summary>
        /// Gets or sets the additional message.
        /// </summary>
        /// <value>
        /// The additional message.
        /// </value>
        public SafeString AdditionalMessage { get; set; }

        /// <summary>
        /// Gets or sets the admin notes.
        /// </summary>
        /// <value>
        /// The admin notes.
        /// </value>
        public SafeString AdminNotes { get; set; }

        /// <summary>
        /// Gets or sets alerts enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> when alerts are enabled for the <see cref="User"/> otherwise false.
        /// </value> 
        public bool AlertsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the last activity date.
        /// </summary>
        /// <value>
        /// The last activity date.
        /// </value>
        public DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Gets or sets the last lockout date.
        /// </summary>
        /// <value>
        /// The last lockout date.
        /// </value>
        public DateTime LastLockoutDate { get; set; }

        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        /// <value>
        /// The last login date.
        /// </value>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the expires at.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is terms and conditions accepted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is terms and conditions accepted; otherwise, <c>false</c>.
        /// </value>
        public bool IsTermsAndConditionsAccepted { get; set; }

        /// <summary>
        /// Gets or sets the terms and conditions accepted at.
        /// </summary>
        /// <value>
        /// The terms and conditions accepted at.
        /// </value>
        public DateTime TermsAndConditionsAcceptedAt { get; set; }

        /// <summary>
        /// Gets or sets the terms and conditions updated.
        /// </summary>
        /// <value>
        /// The terms and conditions updated.
        /// </value>
        public DateTime TermsAndConditionsUpdated { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public SafeString Website { get; set; }

        /// <summary>
        /// Permissions for this user.
        /// </summary>
        public ulong Permissions { get; set; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left">
        /// <see cref="User"/> that is on the left side of the equation.
        /// </param>
        /// <param name="right">
        /// <see cref="User"/> that is on the right side of the equation.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="User"/> is equal to the current <see cref="User"/>;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left">
        /// <see cref="User"/> that is on the left side of the equation.
        /// </param>
        /// <param name="right">
        /// <see cref="User"/> that is on the right side of the equation.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="User"/> is not equal to the current <see cref="User"/>;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether two object instances are equal.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with the current object.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && this.Equals((User)obj);
        }

        /// <summary>
        /// Calculate Hash Code.
        /// </summary>
        /// <returns>
        /// <see cref="int"/> representing the Hash Code.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                return this.UserName.GetHashCode() ^ this.OrgUnitId.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a string that represents the User.
        /// </summary>
        /// <returns>
        /// String that represents the User.
        /// </returns>
        public override string ToString()
        {
            return string.Format(@"{0}", this.UserName);
        }

        /// <summary>
        /// The update id.
        /// </summary>
        /// <param name="newId">
        /// The new id.
        /// </param>
        public void UpdateId(EntityId newId)
        {
            if (this.Id != EntityId.Empty)
            {
                return;
            }

            this.Id = newId;
        }

        /// <summary>
        /// The build or retrieve empty instance.
        /// </summary>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        private static User BuildOrRetrieveEmptyInstance()
        {
            if (_emptyInstance == null)
            {
                lock (Locker)
                {
                    // double check in case _emptyInstance was instantiated while this thread was locked.
                    if (_emptyInstance == null)
                    {
                        _emptyInstance = new User();
                    }
                }
            }

            return _emptyInstance;
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool Equals(User other)
        {
            return (this.Id == other.Id) && (this.UserName == other.UserName) && this.OrgUnitId == other.OrgUnitId;
        }
    }
}