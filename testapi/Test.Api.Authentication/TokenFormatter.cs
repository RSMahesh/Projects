// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenFormatter.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   TokenFormatter class is responsible to cretae JWT token.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Authentication
{
    using System;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.IdentityModel.Tokens;

    using Microsoft.Owin.Security;

    /// <summary>
    /// Provides the JWT token.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TokenFormatter : ISecureDataFormat<AuthenticationTicket>
    {
        private const string JwtLifeTime = "JWTLifeTime";
        private const string Issuer = "IssuerUrl";
        private const string Audience = "AudienceUrl";

        /// <summary>
        /// Create the JWT token and sign it.
        /// </summary>
        /// <param name="data">The authentication data.</param>
        /// <returns>a <see cref="string"/> representation of the token.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="data"/> is null.</exception>
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            var jwtToken = new JwtSecurityToken(
                ConfigurationManager.AppSettings[Issuer],
                ConfigurationManager.AppSettings[Audience],
                data.Identity.Claims,
                DateTime.UtcNow, 
                DateTime.UtcNow.AddDays(Convert.ToDouble(ConfigurationManager.AppSettings[JwtLifeTime])),
                new SigningCredentials(SigningCertificate.SecurityKey, SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest));

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(jwtToken);
        }

        /// <summary>
        /// Unprotects the specified protected text.
        /// </summary>
        /// <param name="protectedText">The protected text.</param>
        /// <returns>
        /// Authentication data.
        /// </returns>
        /// <exception cref="System.NotImplementedException">This method has not been implemented.</exception>
        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
