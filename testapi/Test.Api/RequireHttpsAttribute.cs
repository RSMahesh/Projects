// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequireHttpsAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   The require https attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <summary>
    /// The require https attribute.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RequireHttpsAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The request context that does not need https.
        /// </summary>
        private static readonly Type RequestContextThatDoesNotNeedHttps = typeof(HttpActionContext);

        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!RequestIsOk(actionContext))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }

        /// <summary>
        /// The request is ok.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool RequestIsOk(HttpActionContext actionContext)
        {
            var retValue = IsBatchRequest(actionContext)
                           || actionContext.Request.RequestUri.Scheme == Uri.UriSchemeHttps;

            return retValue;
        }

        /// <summary>
        /// The is batch request.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsBatchRequest(HttpActionContext actionContext)
        {
            return actionContext.Request.Properties.ContainsKey("MS_BatchRequest")
                   && actionContext.Request.Properties["MS_BatchRequest"].Equals(true);
        }
    }
}