// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The guid extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The guid extensions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class GuidExtensions
    {
        /// <summary>
        /// The to entity id.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The <see cref="EntityId"/>.
        /// </returns>
        public static EntityId ToEntityId(this Guid guid)
        {
            return guid == Guid.Empty ? EntityId.Empty : new EntityId(guid);
        }
    }
}