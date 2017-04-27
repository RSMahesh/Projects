// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Password.cs" company="">
//   
// </copyright>
// <summary>
//   Represents an authentication password.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System.Collections.Generic;
 
    using Test.Api.Core;

    /// <summary>
    /// Represents an authentication password<see cref="Password" />
    /// </summary>
    public sealed class Password
    {
        /// <summary>
        /// The data validation rules.
        /// </summary>
        private static readonly IList<Validator<string>> DataValidationRules = new List<Validator<string>>
                                                                                   {
                                                                                       rawString
                                                                                       =>
                                                                                       !string
                                                                                            .IsNullOrWhiteSpace
                                                                                            (
                                                                                                rawString)
                                                                                           ? (
                                                                                             IRuleResult
                                                                                             )
                                                                                             RulePassed
                                                                                                 .Passed
                                                                                           : new RuleFatallyViolated
                                                                                                 (
                                                                                                 "Password can not be null or white spaces only."), 
                                                                                       rawString
                                                                                       =>
                                                                                       rawString
                                                                                       != string
                                                                                              .Empty
                                                                                           ? (
                                                                                             IRuleResult
                                                                                             )
                                                                                             RulePassed
                                                                                                 .Passed
                                                                                           : new RuleFatallyViolated
                                                                                                 (
                                                                                                 "Password can not be blank."), 
                                                                                       rawString
                                                                                       =>
                                                                                       rawString
                                                                                           .Length
                                                                                       <= 128
                                                                                           ? (
                                                                                             IRuleResult
                                                                                             )
                                                                                             RulePassed
                                                                                                 .Passed
                                                                                           : new RuleCriticallyViolated
                                                                                                 (
                                                                                                 "Password can not contains more than 128 characters."), 
                                                                                       rawString
                                                                                       =>
                                                                                       rawString
                                                                                           .Length
                                                                                       >= 8
                                                                                           ? (
                                                                                             IRuleResult
                                                                                             )
                                                                                             RulePassed
                                                                                                 .Passed
                                                                                           : new RuleCriticallyViolated
                                                                                                 (
                                                                                                 "Password should contains at least 8 characters."), 
                                                                                       rawString
                                                                                       =>
                                                                                       rawString
                                                                                           .HasNumeric
                                                                                           ()
                                                                                           ? (
                                                                                             IRuleResult
                                                                                             )
                                                                                             RulePassed
                                                                                                 .Passed
                                                                                           : new RuleCriticallyViolated
                                                                                                 (
                                                                                                 "Password should contains at least one numeric."), 
                                                                                   };

        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The _empty instance.
        /// </summary>
        private static volatile Password _emptyInstance;

        /// <summary>
        /// The _password.
        /// </summary>
        private readonly string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="Password"/> class.
        /// </summary>
        /// <param name="password">
        /// The password.
        /// </param>
        public Password(string password)
        {
            var dataValidation = new DataValidationService<string>(DataValidationRules);

            var dataValidationResult = dataValidation.Validate(password);

            if (dataValidationResult.HasErrors())
            {
                throw new InvalidPasswordException(dataValidationResult.ToString());
            }

            this._password = password;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Password"/> class from being created.
        /// </summary>
        private Password()
        {
            this._password = string.Empty;
        }

        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>The empty.</value>
        public static Password Empty
        {
            get
            {
                return BuildOrRetrieveEmptyInstance();
            }
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left"><see cref="Password" /> that is on the left side of the equation.</param>
        /// <param name="right"><see cref="Password" /> that is on the right side of the equation.</param>
        /// <returns><c>true</c> if the specified <see cref="Password" /> is equal to the current <see cref="Password" />;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator ==(Password left, Password right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left"><see cref="Password" /> that is on the left side of the equation.</param>
        /// <param name="right"><see cref="Password" /> that is on the right side of the equation.</param>
        /// <returns><c>true</c> if the specified <see cref="Password" /> is not equal to the current <see cref="Password" />;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator !=(Password left, Password right)
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

            return obj.GetType() == this.GetType() && this.Equals((Password)obj);
        }

        /// <summary>
        /// Determines whether a <see cref="string"/> is equal to the current <see cref="Password"/>.
        /// </summary>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="string"/> is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(string password)
        {
            return this.Equals(new Password(password));
        }

        /// <summary>
        /// Calculate Hash Code.
        /// </summary>
        /// <returns><see cref="int" /> representing the Hash Code.</returns>
        public override int GetHashCode()
        {
            return this._password.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the Password.
        /// </summary>
        /// <returns>String that represents the Password.</returns>
        public override string ToString()
        {
            return this._password;
        }

        /// <summary>
        /// The build or retrieve empty instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Password"/>.
        /// </returns>
        private static Password BuildOrRetrieveEmptyInstance()
        {
            if (_emptyInstance == null)
            {
                lock (Locker)
                {
                    // double check in case _emptyInstance was instantiated while this thread was locked.
                    if (_emptyInstance == null)
                    {
                        _emptyInstance = new Password();
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
        private bool Equals(Password other)
        {
            return this._password.Equals(other._password);
        }
    }
}