// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFunctionAppender.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Represent a IFunctionAppender Interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Test.Api.Business;
using Test.Api.Core;

namespace Test.Api.HyperMedia
{
    /// <summary>
    /// Represent a IFunctionAppender Interface.
    /// </summary>
    public interface IFunctionAppender
    {
        /// <summary>
        /// Gets the create links for.
        /// </summary>
        /// <value>
        /// The create links for.
        /// </value>
        Type CreatesLinksFor { get; }

        /// <summary>
        /// Appends the links to.
        /// </summary>
        /// <param name="webLinkable">The <see cref="IWebLinkable"/> object.</param>
        /// <param name="endPoint"></param>
        /// <param name="context">
        /// Context containing information about the request.
        /// </param>
        void AppendFunctionsTo(IWebLinkable webLinkable, ApiResourceEndPoint endPoint, ITestApiContext context);
    }
}
