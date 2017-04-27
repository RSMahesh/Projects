// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhoneNumber.cs" company="">
//   
// </copyright>
// <summary>
//   Represents a phone number.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    /// <summary>
    /// Represents a phone number.
    /// </summary>
    public sealed class PhoneNumber
    {
        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The _empty instance.
        /// </summary>
        private static volatile PhoneNumber _emptyInstance;

        /// <summary>
        /// The _number.
        /// </summary>
        private readonly string _number;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumber"/> class.
        /// </summary>
        /// <param name="number">
        /// The phone number in its raw format.
        /// </param>
        public PhoneNumber(string number)
        {
            this._number = ClearFormatting(number);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PhoneNumber"/> class from being created.
        /// </summary>
        private PhoneNumber()
        {
            this._number = string.Empty;
        }

        /// <summary>
        /// Gets an empty instance.
        /// </summary>
        public static PhoneNumber Empty
        {
            get
            {
                return BuildOrRetrieveEmptyInstance();
            }
        }

        /// <summary>
        /// Returns a string that represents the PhoneNumber.
        /// </summary>
        /// <returns>
        /// String that represents the PhoneNumber.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(this._number) || this._number.Length < 10
                       ? string.Empty
                       : string.Format(
                           "{0}-{1}-{2}", 
                           this._number.Substring(0, 3), 
                           this._number.Substring(3, 3), 
                           this._number.Substring(6, 4));
        }

        /// <summary>
        /// Determines whether a <see cref="string"/> is equal to the current <see cref="PhoneNumber"/>.
        /// </summary>
        /// <param name="phone">
        /// The <see cref="string"/> to compare with the current <see cref="PhoneNumber"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(string phone)
        {
            var newNumber = new PhoneNumber(phone);

            return this.Equals(newNumber);
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

            return obj.GetType() == this.GetType() && this.Equals((PhoneNumber)obj);
        }

        /// <summary>
        /// Calculate Hash Code.
        /// </summary>
        /// <returns>
        /// <see cref="int"/> representing the Hash Code.
        /// </returns>
        public override int GetHashCode()
        {
            return this._number != null ? this._number.GetHashCode() : 0;
        }

        /// <summary>
        /// The build or retrieve empty instance.
        /// </summary>
        /// <returns>
        /// The <see cref="PhoneNumber"/>.
        /// </returns>
        private static PhoneNumber BuildOrRetrieveEmptyInstance()
        {
            if (_emptyInstance == null)
            {
                lock (Locker)
                {
                    if (_emptyInstance == null)
                    {
                        _emptyInstance = new PhoneNumber();
                    }
                }
            }

            return _emptyInstance;
        }

        /// <summary>
        /// The clear formatting.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ClearFormatting(string number)
        {
            if (number == null)
            {
                return null;
            }

            var tmp = number;

            tmp = tmp.Replace("(", string.Empty);
            tmp = tmp.Replace(")", string.Empty);
            tmp = tmp.Replace("-", string.Empty);
            tmp = tmp.Replace(" ", string.Empty);

            return tmp;
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="phoneNumber">
        /// The phone number.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool Equals(PhoneNumber phoneNumber)
        {
            return string.Equals(this._number, phoneNumber._number);
        }
    }
}