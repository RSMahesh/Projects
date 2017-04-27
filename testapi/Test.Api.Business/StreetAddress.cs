// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreetAddress.cs" company="">
//   
// </copyright>
// <summary>
//   Represents a street address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    /// <summary>
    /// Represents a street address.
    /// </summary>
    public sealed class StreetAddress
    {
        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The _empty instance.
        /// </summary>
        private static StreetAddress _emptyInstance;

        /// <summary>
        /// The _street.
        /// </summary>
        private readonly string _street;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreetAddress"/> class.
        /// </summary>
        /// <param name="street">
        /// Fully qualified street address.
        /// </param>
        public StreetAddress(string street)
        {
            this._street = street;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="StreetAddress"/> class from being created.
        /// </summary>
        private StreetAddress()
        {
            this._street = string.Empty;
        }

        /// <summary>
        /// Gets an empty instance.
        /// </summary>
        public static StreetAddress Empty
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
        /// <see cref="StreetAddress"/> that is on the left side of the equation.
        /// </param>
        /// <param name="right">
        /// <see cref="StreetAddress"/> that is on the right side of the equation.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="StreetAddress"/> is equal to the current <see cref="StreetAddress"/>;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(StreetAddress left, StreetAddress right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left">
        /// <see cref="StreetAddress"/> that is on the left side of the equation.
        /// </param>
        /// <param name="right">
        /// <see cref="StreetAddress"/> that is on the right side of the equation.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="StreetAddress"/> is not equal to the current <see cref="StreetAddress"/>;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(StreetAddress left, StreetAddress right)
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

            return obj.GetType() == this.GetType() && this.Equals((StreetAddress)obj);
        }

        /// <summary>
        /// Determines whether a <see cref="string"/> is equal to the current <see cref="StreetAddress"/>.
        /// </summary>
        /// <param name="street">
        /// The <see cref="string"/> to compare with the current <see cref="StreetAddress"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="string"/> is equal to the current <see cref="StreetAddress"/>; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public bool Equals(string street)
        {
            return this.Equals(new StreetAddress(street));
        }

        /// <summary>
        /// Calculate Hash Code.
        /// </summary>
        /// <returns>
        /// <see cref="int"/> representing the Hash Code.
        /// </returns>
        public override int GetHashCode()
        {
            return this._street.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the street address.
        /// </summary>
        /// <returns>
        /// String that represents the street address.
        /// </returns>
        public override string ToString()
        {
            return this._street;
        }

        /// <summary>
        /// The build or retrieve empty instance.
        /// </summary>
        /// <returns>
        /// The <see cref="StreetAddress"/>.
        /// </returns>
        private static StreetAddress BuildOrRetrieveEmptyInstance()
        {
            if (_emptyInstance == null)
            {
                lock (Locker)
                {
                    // double check incase _emptyInstance was instansiated while this thread was locked.
                    if (_emptyInstance == null)
                    {
                        _emptyInstance = new StreetAddress();
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
        private bool Equals(StreetAddress other)
        {
            return this._street.Equals(other._street);
        }
    }
}