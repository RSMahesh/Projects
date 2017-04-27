// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntityPatcher.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Contract to enable objects to be updated from a PATCH request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Test.Api.Business;

namespace Test.Api.Producers
{
    /// <summary>
    /// Contract to enable objects to be updated from a PATCH request.
    /// </summary>
    public interface IEntityPatcher
    {
        /// <summary>
        /// Apply the changes in <paramref name="operations"/> to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">
        /// The object to apply the operations too.
        /// </param>
        /// <param name="operations">
        /// A list of operations that was passed in with the PATCH request.
        /// </param>
        /// <param name="context">
        /// Context containing information about the request.
        /// </param>
        void Patch(object entity, IEnumerable<PatchOperationBase> operations, ITestApiContext context);
    }
}