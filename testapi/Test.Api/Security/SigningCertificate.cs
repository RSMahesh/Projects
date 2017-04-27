// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SigningCertificate.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//  SigningCertificate for signing the Certificate for JWT token.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Security
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.IdentityModel.Tokens;
    using System.Security.Cryptography.X509Certificates;
    using System.Web;

    /// <summary>
    /// SigningCertificate for signing the Certificate for JWT token.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SigningCertificate
    {
        private const string CertificatePath = "PublicCertificatePath";

        /// <summary>
        /// Gets the security key.
        /// </summary>
        /// <value>
        /// The security key.
        /// </value>
        public static X509Certificate2 SecurityKey
        {
            get
            {
                return GetSecurityKey();
            }
        }

        /// <summary>
        /// Gets the security token.
        /// </summary>
        /// <value>
        /// The security token.
        /// </value>
        public static X509SecurityToken SecurityToken
        {
            get
            {
                return GetSecurityToken();
            }
        }

        private static X509Certificate2 GetSecurityKey()
        {
            var securityCertificate = new X509Certificate2(
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings[CertificatePath]));

            return securityCertificate;
        }

        private static X509SecurityToken GetSecurityToken()
        {
            return new X509SecurityToken(SecurityKey);
        }
    }
}
