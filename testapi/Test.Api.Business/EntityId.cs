// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityId.cs" company="">
//   
// </copyright>
// <summary>
//   Represents an Id for any entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System;

    /// <summary>
    /// Represents an Id for any entity.
    /// </summary>
    public sealed class EntityId
    {
        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The _empty instance.
        /// </summary>
        private static volatile EntityId _emptyInstance;

        /// <summary>
        /// The _id.
        /// </summary>
        private readonly Guid _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityId"/> class.
        /// </summary>
        /// <param name="id">
        /// <see cref="Guid"/> representation of the Identity.
        /// </param>
        public EntityId(Guid id)
        {
            this._id = id;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="EntityId"/> class from being created.
        /// </summary>
        private EntityId()
        {
            this._id = Guid.Empty;
        }

        /// <summary>
        /// Gets an empty instance.
        /// </summary>
        public static EntityId Empty
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
        /// <see cref="EntityId"/> that is on the left side of the equation.
        /// </param>
        /// <param name="right">
        /// <see cref="EntityId"/> that is on the right side of the equation.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="EntityId"/> is equal to the current <see cref="EntityId"/>; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public static bool operator ==(EntityId left, EntityId right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left">
        /// <see cref="EntityId"/> that is on the left side of the equation.
        /// </param>
        /// <param name="right">
        /// <see cref="EntityId"/> that is on the right side of the equation.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="EntityId"/> is not equal to the current <see cref="EntityId"/>;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(EntityId left, EntityId right)
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

            return obj.GetType() == this.GetType() && this.Equals((EntityId)obj);
        }

        /// <summary>
        /// Determines whether a <see cref="Guid"/> is equal to the current <see cref="EntityId"/>.
        /// </summary>
        /// <param name="id">
        /// The <see cref="Guid"/> to compare with the current <see cref="EntityId"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Guid id)
        {
            return this._id.Equals(id);
        }

        /// <summary>
        /// Calculate Hash Code.
        /// </summary>
        /// <returns>
        /// <see cref="int"/> representing the Hash Code.
        /// </returns>
        public override int GetHashCode()
        {
            return this._id.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the Id.
        /// </summary>
        /// <returns>
        /// String that represents the Id.
        /// </returns>
        public override string ToString()
        {
            return this._id.ToString();
        }

        /// <summary>
        /// The build or retrieve empty instance.
        /// </summary>
        /// <returns>
        /// The <see cref="EntityId"/>.
        /// </returns>
        private static EntityId BuildOrRetrieveEmptyInstance()
        {
            if (_emptyInstance == null)
            {
                lock (Locker)
                {
                    // double check incase _emptyInstance was instansiated while this thread was locked.
                    if (_emptyInstance == null)
                    {
                        _emptyInstance = new EntityId();
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
        private bool Equals(EntityId other)
        {
            return this._id.Equals(other._id);
        }

        public Guid ToGuid()
        {
            return _id;
        }


        //I did just to check implict operator
        public static implicit operator Guid(EntityId d)
        {
            return d._id;
        }

        public static implicit operator EntityId(Guid d)
        {
            return new EntityId(d);
        }
    }
}