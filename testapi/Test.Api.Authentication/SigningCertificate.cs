// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SigningCertificate.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Provides the functionality to sign the JWT token.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Authentication
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.IdentityModel.Tokens;
    using System.Security.Cryptography.X509Certificates;
    using System.Web;

    /// <summary>
    /// Provides the functionality to sign the JWT token.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SigningCertificate
    {
        private const string CertificatePath = "privateCertificatePath";
        private const string CertificatePassword = "certificatePassword";

        /// <summary>
        /// Gets the security key used for signing the JWT token.
        /// </summary>
        /// <value>
        /// The security key used to sign the token.
        /// </value>
        public static X509AsymmetricSecurityKey SecurityKey
        {
            get { return GetSecurityKey(); }
        }
        
        private static X509AsymmetricSecurityKey GetSecurityKey()
        {
            var certificate = new X509Certificate2(
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings[CertificatePath]), 
                ConfigurationManager.AppSettings[CertificatePassword]);
            var securityKey = new X509AsymmetricSecurityKey(certificate);

            return securityKey;
        }
    }
}