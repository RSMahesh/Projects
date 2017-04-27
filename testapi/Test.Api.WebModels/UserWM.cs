// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserWM.cs" company="">
//   
// </copyright>
// <summary>
//   The user wm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.WebModels
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The user wm.
    /// </summary>
    [DataContract]
    public sealed class UserWM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserWM"/> class.
        /// </summary>
        public UserWM()
        {
            // Links = new WebLinks();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        [DataMember(Name = "roleId")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the user domain.
        /// </summary>
        [DataMember(Name = "userDomain")]
        public string UserDomain { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        [DataMember(Name = "department")]
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [DataMember(Name = "position")]
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the office number.
        /// </summary>
        [DataMember(Name = "officeNumber")]
        public string OfficeNumber { get; set; }

        /// <summary>
        /// Gets or sets the cellular number.
        /// </summary>
        [DataMember(Name = "cellularNumber")]
        public string CellularNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax number.
        /// </summary>
        [DataMember(Name = "faxNumber")]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [DataMember(Name = "emailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the password information.
        /// </summary>
        /// <value>
        /// The password information.
        /// </value>
        [DataMember(Name = "passwordinformation")]
        public PasswordInformationDetail PasswordInformation { get; set; }

        /// <summary>
        /// Gets or sets the additional message.
        /// </summary>
        [DataMember(Name = "additionalMessage")]
        public string AdditionalMessage { get; set; }

        /// <summary>
        /// Gets or sets the admin notes.
        /// </summary>
        [DataMember(Name = "adminNotes")]
        public string AdminNotes { get; set; }

        /// <summary>
        /// Gets or sets the last active at.
        /// </summary>
        [DataMember(Name = "lastActiveAt")]
        public DateTime LastActiveAt { get; set; }

        /// <summary>
        /// Gets or sets the last locked out at.
        /// </summary>
        [DataMember(Name = "lastLockedOutAt")]
        public DateTime LastLockedOutAt { get; set; }

        /// <summary>
        /// Gets or sets the last login at.
        /// </summary>
        [DataMember(Name = "lastLoginAt")]
        public DateTime LastLoginAt { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the expires at.
        /// </summary>
        [DataMember(Name = "expiresAt")]
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the terms and conditions updated.
        /// </summary>
        /// <value>
        /// The terms and conditions updated.
        /// </value>
        public DateTime TermsAndConditionsUpdated { get; set; }

        /// <summary>
        /// Gets or sets the terms and conditions accepted at.
        /// </summary>
        /// <value>
        /// The terms and conditions accepted at.
        /// </value>
        [DataMember(Name = "termsandconditionsacceptedat")]
        public DateTime TermsAndConditionsAcceptedAt { get; set; }

        /// <summary>
        /// Gets the terms conditions acceptance status.
        /// </summary>
        /// <value>
        /// The terms conditions acceptance status.
        /// </value>
        [DataMember(Name = "isTermsAndConditionsAccepted")]
        public bool IsTermsAndConditionsAccepted { get; set; }

        /// <summary>
        /// Gets or sets the web site.
        /// </summary>
        /// <value>
        /// The web site.
        /// </value>
        [DataMember(Name = "website")]
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the alerts enabled.
        /// </summary>
        [DataMember(Name = "alertsEnabled")]
        public bool AlertsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>        
        /// Gets or sets the Organization name.
        /// </summary>
        [DataMember(Name = "organizationName")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is suspended.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is suspended; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "isSuspended")]
        public bool IsSuspended { get; set; }
    }
}