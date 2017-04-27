// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebFunctionAppender.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Represent a IWebFunctionAppender Interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using Test.Api.Business;
using Test.Api.Core;
using Test.Api.HyperMedia;

namespace Test.Api.Producers
{
    /// <summary>
    /// Represent a IWebFunctionAppender Interface.
    /// </summary>
    public interface IWebFunctionAppender
    {
        /// <summary>
        /// Appends links to an outgoing <see cref="IWebLinkable"/> object.
        /// </summary>
        /// <param name="webLinkable">
        /// The object to add links to.
        /// </param>
        /// <param name="context">
        /// Context containing information about the request.
        /// </param>
        void AppendLinksTo(IWebLinkable webLinkable, ApiResourceEndPoint endPoint, ITestApiContext context);
    }
}
