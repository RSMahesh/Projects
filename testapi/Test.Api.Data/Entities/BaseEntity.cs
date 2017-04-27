// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="">
//   
// </copyright>
// <summary>
//   The base entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The base entity.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// </summary>
        public BaseEntity()
        {
            this.CreateDate = DateTime.Now;
            this.LastUpdateDate = DateTime.Now;
        }

        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        public DateTime? CreateDate { get; set; }

        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated user id.
        /// </summary>
        public Guid? LastUpdatedUserID { get; set; }

        /// <summary>
        /// Gets or sets the last updated user.
        /// </summary>
        [ForeignKey("LastUpdatedUserID")]
        public virtual UserEntity LastUpdatedUser { get; set; }
    }
}