// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidAuthenticationMiddleware.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The invalid authentication middleware.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Authentication
{
    using System.Threading.Tasks;

    using Microsoft.Owin;

    /// <summary>
    /// The invalid authentication middleware.
    /// </summary>
    public class InvalidAuthenticationMiddleware : OwinMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAuthenticationMiddleware"/> class.
        /// </summary>
        /// <param name="next">
        /// The next.
        /// </param>
        public InvalidAuthenticationMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task Invoke(IOwinContext context)
        {
            await this.Next.Invoke(context);

            if (context.Request != null &&
                context.Request.ContentType != null &&
                !context.Request.ContentType.Contains("multipart/mixed"))
            {
                context.Response.Headers["Cache-Control"] = "private, no-cache";
                context.Response.Headers.Remove("Expires");
            }

            if (context.Response.StatusCode == 400 && context.Response.Headers.ContainsKey("AuthorizationResponse"))
            {
                var code = int.Parse(context.Response.Headers["AuthorizationResponse"]);
                context.Response.Headers.Remove("AuthorizationResponse");

                context.Response.StatusCode = code;
            }
        }
    }
}