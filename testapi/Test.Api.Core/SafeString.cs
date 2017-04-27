// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeString.cs" company="">
//   
// </copyright>
// <summary>
//   Represent a proper name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    /// <summary>
    /// Represents a proper name<see cref="SafeString" />class.    
    /// </summary>
    public sealed class SafeString
    {
        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The _empty instance.
        /// </summary>
        private static volatile SafeString _emptyInstance;

        /// <summary>
        /// The _value.
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeString"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public SafeString(string name)
        {
            this._value = string.IsNullOrWhiteSpace(name) ? string.Empty : name;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="SafeString"/> class from being created.
        /// </summary>
        private SafeString()
        {
            this._value = string.Empty;
        }

        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>The empty.</value>
        public static SafeString Empty
        {
            get
            {
                return BuildOrRetrieveEmptyInstance();
            }
        }

        /// <summary>
        /// Returns a SafeString with the internalValue converted to lowercase using the casing rules of the invariant culture.
        /// </summary>
        /// <returns>
        /// The <see cref="SafeString"/>.
        /// </returns>
        public SafeString ToLowerInvariant()
        {
            return new SafeString(this._value.ToLowerInvariant());
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left"><see cref="SafeString" /> that is on the left side of the equation.</param>
        /// <param name="right"><see cref="SafeString" /> that is on the right side of the equation.</param>
        /// <returns><c>true</c> if the specified <see cref="SafeString" /> is equal to the current <see cref="SafeString" />;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator ==(SafeString left, SafeString right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left"><see cref="SafeString" /> that is on the left side of the equation.</param>
        /// <param name="right"><see cref="SafeString" /> that is on the right side of the equation.</param>
        /// <returns><c>true</c> if the specified <see cref="SafeString" /> is not equal to the current <see cref="SafeString" />;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator !=(SafeString left, SafeString right)
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

            return obj.GetType() == this.GetType() && this.Equals((SafeString)obj);
        }

        /// <summary>
        /// Determines whether a <see cref="string"/> is equal to the current <see cref="SafeString"/>.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="string"/> is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(string name)
        {
            return this.Equals(new SafeString(name));
        }

        /// <summary>
        /// Calculate Hash Code.
        /// </summary>
        /// <returns><see cref="int" /> representing the Hash Code.</returns>
        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the SafeString.
        /// </summary>
        /// <returns>String that represents the SafeString.</returns>
        public override string ToString()
        {
            return this._value;
        }

        /// <summary>
        /// The build or retrieve empty instance.
        /// </summary>
        /// <returns>
        /// The <see cref="SafeString"/>.
        /// </returns>
        private static SafeString BuildOrRetrieveEmptyInstance()
        {
            if (_emptyInstance == null)
            {
                lock (Locker)
                {
                    // double check in case _emptyInstance was instantiated while this thread was locked.
                    if (_emptyInstance == null)
                    {
                        _emptyInstance = new SafeString();
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
        private bool Equals(SafeString other)
        {
            return this._value.Equals(other._value);
        }
    }
}