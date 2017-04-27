// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailAddress.cs" company="">
//   
// </copyright>
// <summary>
//   Represents an Username address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System.Collections.Generic;

    using Test.Api.Core;

    /// <summary>
    /// Represents an Username address.
    /// </summary>
    public sealed class EmailAddress
    {
        /// <summary>
        /// The data validation rules.
        /// </summary>
        private static readonly IList<Validator<string>> DataValidationRules = new List<Validator<string>>
                                                                                   {
                                                                                       rawString
                                                                                       =>
                                                                                       !string
                                                                                            .IsNullOrEmpty
                                                                                            (
                                                                                                rawString)
                                                                                           ? (
                                                                                             IRuleResult
                                                                                             )
                                                                                             RulePassed
                                                                                                 .Passed
                                                                                           : new RuleFatallyViolated
                                                                                                 (
                                                                                                 "Email Address can not be blank."), 
                                                                                       rawString
                                                                                       =>
                                                                                       rawString
                                                                                           .Contains
                                                                                           ("@")
                                                                                           ? (
                                                                                             IRuleResult
                                                                                             )
                                                                                             RulePassed
                                                                                                 .Passed
                                                                                           : new RuleCriticallyViolated
                                                                                                 (
                                                                                                 "Email Address must have an '@' in the string."), 
                                                                                       rawString
                                                                                       =>
                                                                                       rawString
                                                                                           .Contains
                                                                                           (".")
                                                                                           ? (
                                                                                             IRuleResult
                                                                                             )
                                                                                             RulePassed
                                                                                                 .Passed
                                                                                           : new RuleCriticallyViolated
                                                                                                 (
                                                                                                 "Email Address must have at least one '.' in the string."), 
                                                                                   };

        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The _empty instance.
        /// </summary>
        private static EmailAddress _emptyInstance;

        /// <summary>
        /// The _address.
        /// </summary>
        private readonly string _address;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        public EmailAddress(string address)
        {
            //My check: just avoid exception in case email is null
            if(string.IsNullOrEmpty(address))
                return;

            var dataValidation = new DataValidationService<string>(DataValidationRules);

            var dataValidationResult = dataValidation.Validate(address);

            if (dataValidationResult.HasErrors())
            {
                throw new InvalidEmailAddressException(dataValidationResult.ToString());
            }

            this._address = address;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="EmailAddress"/> class from being created.
        /// </summary>
        private EmailAddress()
        {
            this._address = string.Empty;
        }

        /// <summary>
        /// Gets an empty instance.
        /// </summary>
        public static EmailAddress Empty
        {
            get
            {
                return BuildOrRetrieveEmptyInstance();
            }
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left">
        /// <see cref="EmailAddress"/> that is on the left side of the equation.
        /// </param>
        /// <param name="right">
        /// <see cref="EmailAddress"/> that is on the right side of the equation.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="EmailAddress"/> is equal to the current <see cref="EmailAddress"/>; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public static bool operator ==(EmailAddress left, EmailAddress right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left">
        /// <see cref="EmailAddress"/> that is on the left side of the equation.
        /// </param>
        /// <param name="right">
        /// <see cref="EmailAddress"/> that is on the right side of the equation.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="EmailAddress"/> is not equal to the current <see cref="EmailAddress"/>; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public static bool operator !=(EmailAddress left, EmailAddress right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Returns a string that represents the Username Address.
        /// </summary>
        /// <returns>
        /// String that represents the Username Address.
        /// </returns>
        public override string ToString()
        {
            return this._address;
        }

        /// <summary>
        /// Determines whether a <see cref="string"/> is equal to the current <see cref="EmailAddress"/>.
        /// </summary>
        /// <param name="address">
        /// The <see cref="string"/> to compare with the current <see cref="EmailAddress"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(string address)
        {
            if (address == null)
            {
                return this.Equals((object)null);
            }

            var newEmailAddress = new EmailAddress(address);

            return this.Equals(newEmailAddress);
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

            return obj.GetType() == this.GetType() && this.Equals((EmailAddress)obj);
        }

        /// <summary>
        /// Calculate Hash Code.
        /// </summary>
        /// <returns>
        /// <see cref="int"/> representing the Hash Code.
        /// </returns>
        public override int GetHashCode()
        {
            return this._address.GetHashCode();
        }

        /// <summary>
        /// The build or retrieve empty instance.
        /// </summary>
        /// <returns>
        /// The <see cref="EmailAddress"/>.
        /// </returns>
        private static EmailAddress BuildOrRetrieveEmptyInstance()
        {
            lock (Locker)
            {
                if (_emptyInstance == null)
                {
                    _emptyInstance = new EmailAddress();
                }
            }

            return _emptyInstance;
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool Equals(EmailAddress emailAddress)
        {
            return string.Equals(this._address, emailAddress._address);
        }
    }
}