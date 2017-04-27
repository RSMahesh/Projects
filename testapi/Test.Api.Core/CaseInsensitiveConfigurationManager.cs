// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaseInsensitiveConfigurationManager.cs" company="">
//   
// </copyright>
// <summary>
//   The case insensitive configuration manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Core
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The case insensitive configuration manager.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class CaseInsensitiveConfigurationManager
    {
        /// <summary>
        /// The internal app settings.
        /// </summary>
        private static readonly NameValueCollection InternalAppSettings;

        /// <summary>
        /// Initializes static members of the <see cref="CaseInsensitiveConfigurationManager"/> class.
        /// </summary>
        static CaseInsensitiveConfigurationManager()
        {
            InternalAppSettings = new NameValueCollection(StringComparer.InvariantCultureIgnoreCase);

            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                InternalAppSettings.Add(key, ConfigurationManager.AppSettings[key]);
            }
        }

        /// <summary>
        /// Gets the app settings.
        /// </summary>
        public static NameValueCollection AppSettings
        {
            get
            {
                return InternalAppSettings;
            }
        }
    }
}