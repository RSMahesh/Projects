// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordInformation.cs" company="">
//   
// </copyright>
// <summary>
//   The password information.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System;

    using Test.Api.Core;

    /// <summary>
    /// The password information.
    /// </summary>
    public sealed class PasswordInformation
    {
        /// <summary>
        /// Gets the password format.
        /// </summary>
        /// <value>
        /// The password format.
        /// </value>
        public PasswordFormat PasswordFormat { get; set; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public Password Password { get; set; }

        /// <summary>
        /// Gets the password salt.
        /// </summary>
        /// <value>
        /// The password salt.
        /// </value>
        public SafeString PasswordSalt { get; set; }

        /// <summary>
        /// Gets the recovery question.
        /// </summary>
        /// <value>
        /// The recovery question.
        /// </value>
        public SafeString RecoveryQuestion { get; set; }

        /// <summary>
        /// Gets the recovery answer.
        /// </summary>
        /// <value>
        /// The recovery answer.
        /// </value>
        public SafeString RecoveryAnswer { get; set; }

        /// <summary>
        /// Gets the consecutive failed login attempts.
        /// </summary>
        /// <value>
        /// The consecutive failed login attempts.
        /// </value>
        public int ConsecutiveFailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets the failed login attempt windows start.
        /// </summary>
        /// <value>
        /// The failed login attempt windows start.
        /// </value>
        public DateTime FailedLoginAttemptWindowsStart { get; set; }

        /// <summary>
        /// Gets the consecutive failed answer attempts.
        /// </summary>
        /// <value>
        /// The consecutive failed answer attempts.
        /// </value>
        public int ConsecutiveFailedAnswerAttempts { get; set; }

        /// <summary>
        /// Gets the failed answer attempt windows start.
        /// </summary>
        /// <value>
        /// The failed answer attempt windows start.
        /// </value>
        public DateTime FailedAnswerAttemptWindowsStart { get; set; }

        /// <summary>
        /// Gets the last changed at.
        /// </summary>
        /// <value>
        /// The last changed at.
        /// </value>
        public DateTime LastChangedAt { get; set; }

        /// <summary>
        /// Gets the password status.
        /// </summary>
        /// <value>
        /// The password status.
        /// </value>
        public PasswordStatus PasswordStatus { get; set; }
    }
}