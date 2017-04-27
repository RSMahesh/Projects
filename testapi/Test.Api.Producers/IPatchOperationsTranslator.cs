// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPatchOperationsTranslator.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The PatchOperationsTranslator interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Test.Api.Business;

using Test.Api.Business;
using Test.Api.WebModels;

namespace Test.Api.Producers
{
    /// <summary>
    /// The PatchOperationsTranslator interface.
    /// </summary>
    public interface IPatchOperationsTranslator
    {
        /// <summary>
        /// The translate.
        /// </summary>
        /// <param name="operations">
        /// The operations.
        /// </param>
        /// <param name="context">
        /// Context containing information about the request.
        /// </param>
        /// <returns>
        /// The patch operations in business object form.
        /// </returns>
        IEnumerable<PatchOperationBase> Translate(IEnumerable<PatchOperation> operations, ITestApiContext context);
    }
}