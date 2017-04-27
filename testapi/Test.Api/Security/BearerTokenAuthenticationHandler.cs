// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BearerTokenAuthenticationHandler.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Message Handler for decoding the bearer token.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Security
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    using Newtonsoft.Json;

    using Test.Api.Business;

    /// <summary>
    /// Message Handler for decoding the bearer token.
    /// </summary>
    public class BearerTokenAuthenticationHandler : DelegatingHandler
    {
//#if AUTHORIZE
        private const string Issuer = "IssuerUrl";
        private const string Audience = "AudienceUrl";
//#endif

        // ReSharper disable RedundantOverridenMember 

        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">
        /// The HTTP request message to send to the server.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token to cancel operation.
        /// </param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1"/>. The task object representing the asynchronous operation.
        /// </returns>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
//#if AUTHORIZE
            string jwtToken;

            var requestUriAsString = request.RequestUri.ToString().ToLowerInvariant();
            if (requestUriAsString.Contains("public/") || requestUriAsString.Contains("swagger"))
            {
                return base.SendAsync(request, cancellationToken);
            }

            if (!TryRetrieveToken(request, out jwtToken))
            {
                var response = BuildResponseErrorMessage(HttpStatusCode.Unauthorized);
                return Task.FromResult(response);
            }

            try
            {
                var principal = ValidateJwtToken(jwtToken);

                var tokenIpAddress = principal.Claims.FirstOrDefault(claim => claim.Type == "IpAddress");

                if (tokenIpAddress == null || tokenIpAddress.Value != this.GetClientIp(request))
                {
                    var response = BuildResponseErrorMessage(HttpStatusCode.Unauthorized);
                    return Task.FromResult(response);
                }

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                    GetClaimObjects();
                }
                else
                {
                    request.GetRequestContext().Principal = principal;
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(ex.Message)
                };

                return Task.FromResult(response);
            }
//#endif
            return base.SendAsync(request, cancellationToken);
        }
        // ReSharper restore RedundantOverridenMember 
//#if AUTHORIZE

        private string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            if (!request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                return null;
            }

            var prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];

            return prop.Address;
        }

        private static ClaimsPrincipal ValidateJwtToken(string jwtToken)
        {
            var validationParams = new TokenValidationParameters
            {
                ValidIssuer = ConfigurationManager.AppSettings[Issuer],
                ValidAudience = ConfigurationManager.AppSettings[Audience],
                IssuerSigningToken = SigningCertificate.SecurityToken
            };
            var recipientTokenHandler = new JwtSecurityTokenHandler();

            SecurityToken validatedToken;

            return recipientTokenHandler.ValidateToken(jwtToken, validationParams, out validatedToken);
        }

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;

            if (!request.Headers.Contains("Authorization"))
            {
                return false;
            }

            var authzHeader = request.Headers.GetValues("Authorization").First();

            // Verify Authorization header contains 'Bearer' scheme
            token = authzHeader.StartsWith("bearer ", StringComparison.OrdinalIgnoreCase) ? authzHeader.Split(' ')[1] : null;
            return !string.IsNullOrEmpty(token);
        }

        private static HttpResponseMessage BuildResponseErrorMessage(HttpStatusCode statusCode)
        {
            var response = new HttpResponseMessage(statusCode);
            var authenticateHeader = new AuthenticationHeaderValue(
                "Bearer",
                "authorization_uri=\"" + "Testurl" + "\"" + "," + "resource_id=" + "audience");
            response.Headers.WwwAuthenticate.Add(authenticateHeader);
            return response;
        }

        private static void GetClaimObjects()
        {
            var userClaims = HttpContext.Current.User as ClaimsPrincipal;

            if (userClaims != null)
            {
                var userPermissionList = JsonConvert.DeserializeObject<IList<PermissionSet>>(
                    userClaims.Claims.Where(m => m.Value.Contains("Permissions")).Select(p => p.Value).FirstOrDefault());
                var orgUnit = userPermissionList.FirstOrDefault(
                 p => p.SecurableObjectType.Equals(SecurableObjectType.Organization));
                var userId = JsonConvert.DeserializeObject(userClaims.Claims.Where(p => p.Type.Contains("UserId")).Select(k => k.Value).FirstOrDefault());
                var email = userClaims.Claims.Where(p => p.Type.Contains("Email")).Select(k => k.Value).FirstOrDefault();
                var userName = userClaims.Claims.Where(p => p.Type.Contains("UserName")).Select(k => k.Value).FirstOrDefault();
                var authenticatedUser = new AuthenticatedUser(
                    Guid.Parse(userId.ToString()).ToEntityId(),
                    new EmailAddress(userName),
                    userPermissionList)
                                            {
                                                OrgUnitId = orgUnit != null ? orgUnit.ObjectId : Guid.Empty,
                                                EmailAddress = new EmailAddress(email)
                                            };

                if (HttpContext.Current.Items.Contains("AuthenticatedUser") && ((AuthenticatedUser)HttpContext.Current.Items["AuthenticatedUser"]).Id == authenticatedUser.Id)
                {
                    return;
                }

                HttpContext.Current.Items.Remove("AuthenticatedUser");
                HttpContext.Current.Items.Add("AuthenticatedUser", authenticatedUser);
            }
        }

        //private static object GetUserInformation(Guid userId, string propertyName)
        //{
        //    var currentUser = new AppUser();
        //    var loadUserById = currentUser.LoadUserById(userId);
        //    return loadUserById ? currentUser.GetType().GetProperty(propertyName).GetValue(currentUser) : null;
        //}
//#endif
    }
}