// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   This class is used to specify components for the application pipeline.
//   OwinStartup attribute will set the startup class runtime in the MediaValet.Api namespace.
//   Further this class illustrates the configuration settings of Provider, AccessTokenExpireTimeSpan, Bearer
//   TokenendpointPath etc.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using MediaValet.Api;
using MediaValet.Api.Authentication;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MediaValet.Api
{
    /// <summary>
    /// This class is used to specify components for the application pipeline.
    ///     OwinStartup attribute will set the startup class runtime in the MediaValet.Api namespace.
    ///     Further this class illustrates the configuration settings of Provider, AccessTokenExpireTimeSpan, Bearer
    ///     TokenendpointPath etc.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string TokenExpireTimeSpan = "TokenExpireTimeSpan";
        private const string TokenEndPointPath = "TokenEndPointPath";

        /// <summary>
        /// This method enables the application to setup the token end point and providers to generate the token.
        /// </summary>
        /// <param name="app">
        /// IAppBuilder builder.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "oAuth is a valid name.")]
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(ConfigurationManager.AppSettings[TokenEndPointPath]),
                AccessTokenExpireTimeSpan =
                    TimeSpan.FromDays(Convert.ToDouble(ConfigurationManager.AppSettings[TokenExpireTimeSpan])),
                Provider = new TableStorageAuthorizationProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}