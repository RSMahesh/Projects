// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableStorageAuthorizationProvider.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   TableStorageAuthorizationProvider class illustrates about the generation of Bearer Token and
//   user authentication from existing table storage.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Authentication
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.OAuth;

    using Newtonsoft.Json;

    using Test.Api.Business;
    using Test.Api.Services;

    /// <summary>
    /// TableStorageAuthorizationProvider class illustrates about the generation of Bearer Token and
    ///     user authentication from existing table storage.
    /// </summary>
    public class CustomAuthorizationProvider : OAuthAuthorizationServerProvider
    {
        private IUserService _userService;

        public CustomAuthorizationProvider(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Called to validate that the origin of the request is a registered "client_id", and that the correct credentials for
        ///     that client are
        ///     present on the request. If the web application accepts Basic authentication credentials,
        ///     context.TryGetBasicCredentials(out clientId, out clientSecret) may be called to acquire those values if present in
        ///     the request header. If the web
        ///     application accepts "client_id" and "client_secret" as form encoded POST parameters,
        ///     context.TryGetFormCredentials(out clientId, out clientSecret) may be called to acquire those values if present in
        ///     the request body.
        ///     If context.Validated is not called the request will not proceed further.
        /// </summary>
        /// <param name="context">
        /// The context of the event carries information in and results out.
        /// </param>
        /// <returns>
        /// Task to enable asynchronous execution.
        /// </returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password". This occurs when the user
        ///     has provided name and password
        ///     credentials directly into the client application's user interface, and the client application is using those to
        ///     acquire an "access_token" and
        ///     optional "refresh_token". If the web application supports the
        ///     resource owner credentials grant type it must validate the context.Username and context.Password as appropriate. To
        ///     issue an
        ///     access token the context.Validated must be called with a new ticket containing the claims about the resource owner
        ///     which should be associated
        ///     with the access token. The application should take appropriate measures to ensure that the endpoint isn’t abused by
        ///     malicious callers.
        ///     The default behavior is to reject this grant type.
        ///     See also http://tools.ietf.org/html/rfc6749#section-4.3.2.
        /// </summary>
        /// <param name="context">
        /// The context of the event carries information in and results out.
        /// </param>
        /// <returns>
        /// Task to enable asynchronous execution.
        /// </returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            try
            {

                var userDataProvider = new Test.IdentityProvider.UserDataProvider(_userService);
                var response = await userDataProvider.AuthenticateAsync(context.UserName, context.Password);

                VerifyAuthentication(context, response);
            }
            catch (Exception ex)
            {
                context.SetError(ex.ToString());
            }
        }

        private static void VerifyAuthentication(BaseValidatingTicketContext<OAuthAuthorizationServerOptions> context, AuthenticatedUser response)
        {
            if (response != null && response.UserName != EmailAddress.Empty)
            {
                BuildToken(context, response);
            }
            else
            {
                context.SetError("Autorization Error", "The username or password is incorrect!");
                context.Response.Headers.Add("AuthorizationResponse", new[] { "401" });
            }
        }

        private static void BuildToken(BaseValidatingTicketContext<OAuthAuthorizationServerOptions> context, AuthenticatedUser response)
        {   
            var claims = new ClaimsIdentity(context.Options.AuthenticationType);
            claims.AddClaim(new Claim("Permissions", JsonConvert.SerializeObject(response.Permissions)));
            claims.AddClaim(new Claim("UserOrgUnitId", JsonConvert.SerializeObject(response.OrgUnitId)));
            claims.AddClaim(new Claim("UserId", JsonConvert.SerializeObject(new Guid(response.Id.ToString()))));
            claims.AddClaim(new Claim("UserName", response.UserName.ToString()));
            claims.AddClaim(new Claim("RoleId", JsonConvert.SerializeObject(response.RoleId)));
            claims.AddClaim(new Claim("Email", response.EmailAddress.ToString()));
           // claims.AddClaim(new Claim("SId", JsonConvert.SerializeObject(response.SId)));
            claims.AddClaim(new Claim("IpAddress", context.Request.RemoteIpAddress));
            context.Validated(claims);
        }
    }
}