// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordInformationDetail.cs" company="">
//   
// </copyright>
// <summary>
//   The password information detail.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.WebModels
{
    using System;
    using System.Runtime.Serialization;

    using Test.Api.Business;

    /// <summary>
    /// The password information detail.
    /// </summary>
    [DataContract]
    public class PasswordInformationDetail
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        /// <value>
        /// The password salt.
        /// </value>
        [DataMember(Name = "passwordsalt")]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the recovery question.
        /// </summary>
        /// <value>
        /// The recovery question.
        /// </value>
        [DataMember(Name = "recoveryquestion")]
        public string RecoveryQuestion { get; set; }

        /// <summary>
        /// Gets or sets the recovery answer.
        /// </summary>
        /// <value>
        /// The recovery answer.
        /// </value>
        [DataMember(Name = "recoveryanswer")]
        public string RecoveryAnswer { get; set; }

        /// <summary>
        /// Gets or sets the failed login attempt windows start.
        /// </summary>
        /// <value>
        /// The failed login attempt windows start.
        /// </value>
        [DataMember(Name = "failedLoginattemptWindowsstart")]
        public DateTime FailedLoginAttemptWindowsStart { get; set; }

        /// <summary>
        /// Gets or sets the failed answer attempt windows start.
        /// </summary>
        /// <value>
        /// The failed answer attempt windows start.
        /// </value>
        [DataMember(Name = "failedanswerattemptWindowsstart")]
        public DateTime FailedAnswerAttemptWindowsStart { get; set; }

        /// <summary>
        /// Gets or sets the last changed at.
        /// </summary>
        /// <value>
        /// The last changed at.
        /// </value>
        [DataMember(Name = "lastchangedat")]
        public DateTime LastChangedAt { get; set; }

        /// <summary>
        /// Gets or sets the password status.
        /// </summary>
        /// <value>
        /// The password status.
        /// </value>
        [DataMember(Name = "passwordstatus")]
        public PasswordStatus PasswordStatus { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        [DataMember(Name = "passwordformat")]
        public PasswordFormat PasswordFormat { get; set; }

        /// <summary>
        /// Gets or sets the consecutive failed login attempts.
        /// </summary>
        [DataMember(Name = "consecutiveFailedLoginAttempts")]
        public int ConsecutiveFailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the failed login attempt window start.
        /// </summary>
        [DataMember(Name = "failedLoginAttemptWindowStart")]
        public DateTime FailedLoginAttemptWindowStart { get; set; }

        /// <summary>
        /// Gets or sets the consecutive failed answer attempts.
        /// </summary>
        [DataMember(Name = "consecutiveFailedAnswerAttempts")]
        public int ConsecutiveFailedAnswerAttempts { get; set; }

        /// <summary>
        /// Gets or sets the failed answer attempts window start.
        /// </summary>
        [DataMember(Name = "failedAnswerAttemptsWindowStart")]
        public DateTime FailedAnswerAttemptsWindowStart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether must change password.
        /// </summary>
        [DataMember(Name = "mustChangePassword")]
        public bool MustChangePassword { get; set; }
    }
}