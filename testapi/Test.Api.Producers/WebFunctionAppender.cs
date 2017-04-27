// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebFunctionAppender.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Represent a WebFunctionAppender class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Test.Api.HyperMedia;
using Test.Api.Core;
using Test.Api.Business;

namespace Test.Api.Producers
{
    /// <summary>
    /// Represent a WebFunctionAppender class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class WebFunctionAppender : IWebFunctionAppender
    {
        private readonly IDictionary<Type, IFunctionAppender> _linkCreators;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebFunctionAppender"/> class.
        /// </summary>
        /// <param name="linkCreator">
        /// The link creator.
        /// </param> 
        public WebFunctionAppender(IEnumerable<IFunctionAppender> linkCreator)
        {
            var dictionary = linkCreator.ToDictionary(creator => creator.CreatesLinksFor);
            _linkCreators = dictionary;
        }

        /// <summary>
        /// Appends links to an outgoing <see cref="IWebLinkable"/> object.
        /// </summary>
        /// <param name="webLinkable">
        /// The object to add links to.
        /// </param>
        /// <param name="context">
        /// The authenticated user.
        /// </param>
        [ExcludeFromCodeCoverage]
        public void AppendLinksTo(IWebLinkable webLinkable, ApiResourceEndPoint endPoint, ITestApiContext context)
        {
            var weblinkType = webLinkable.GetType();
            if (!_linkCreators.ContainsKey(webLinkable.GetType()))
            {
                return;
            }

            _linkCreators[weblinkType].AppendFunctionsTo(webLinkable, endPoint, context);
        }
    }
}