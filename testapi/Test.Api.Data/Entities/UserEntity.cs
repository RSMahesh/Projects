// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="">
//   
// </copyright>
// <summary>
//   Represents a user of the systemclass.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Data.Entities
{
    using System;

    /// <summary>
    /// Represents a user of the system<see cref="UserEntity" />class.
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the Username.
        /// </summary>
        /// <value>
        /// The Username.
        /// </value>
        public string UserName { get; set; }

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
        public string UserDomain { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the office number.
        /// </summary>
        /// <value>
        /// The office number.
        /// </value>
        public string OfficeNumber { get; set; }

        /// <summary>
        /// Gets or sets the cellular number.
        /// </summary>
        /// <value>
        /// The cellular number.
        /// </value>
        public string CellularNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax number.
        /// </summary>
        /// <value>
        /// The fax number.
        /// </value>
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the status flags.
        /// </summary>
        /// <value>
        /// The status flags.
        /// </value>
        public int StatusFlags { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password answer.
        /// </summary>
        public string PasswordAnswer { get; set; }

        /// <summary>
        /// Gets or sets the password format.
        /// </summary>
        public int PasswordFormat { get; set; }

        /// <summary>
        /// Gets or sets the password question.
        /// </summary>
        public string PasswordQuestion { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the default skin.
        /// </summary>
        /// <value>
        /// The default skin.
        /// </value>
        public string DefaultSkin { get; set; }

        /// <summary>
        /// Gets or sets the additional message.
        /// </summary>
        /// <value>
        /// The additional message.
        /// </value>
        public string AdditionalMessage { get; set; }

        /// <summary>
        /// Gets or sets the admin notes.
        /// </summary>
        /// <value>
        /// The admin notes.
        /// </value>
        public string AdminNotes { get; set; }

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
        public DateTime? LastActivityDate { get; set; }

        /// <summary>
        /// Gets or sets the last lockout date.
        /// </summary>
        /// <value>
        /// The last lockout date.
        /// </value>
        public DateTime? LastLockoutDate { get; set; }

        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        /// <value>
        /// The last login date.
        /// </value>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the expires at.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        public DateTime? ExpiresAt { get; set; }

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
        public DateTime? TermsAndConditionsAcceptedAt { get; set; }

        /// <summary>
        /// Gets or sets the terms and conditions updated.
        /// </summary>
        /// <value>
        /// The terms and conditions updated.
        /// </value>
        public DateTime? TermsAndConditionsUpdated { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public string Website { get; set; }

        /// <summary>
        /// Permissions for this user.
        /// </summary>
        public ulong Permissions { get; set; }
    }
}