// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidTranslationException.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Exception indicating a translation between types is not possible.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Exception indicating a translation between types is not possible.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class InvalidTranslationException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidTranslationException" /> class.
        /// </summary>
        /// <param name="sourceType">The <see cref="Type" /> of the SourceType object.</param>
        /// <param name="destinationType">The <see cref="Type" /> of the DestinationType object.</param>
        public InvalidTranslationException(Type sourceType, Type destinationType)
            : base(
                string.Format(@"Unable to translate from {0} to {1}.  Mapping is not found.", sourceType.FullName,
                    destinationType.FullName))
        {
            this.SourceType = sourceType;
            this.DestinationType = destinationType;
        }

        /// <summary>
        ///     The <see cref="Type" /> of the SourceType object.
        /// </summary>
        public Type SourceType { get; private set; }

        /// <summary>
        ///     The <see cref="Type" /> of the DestinationType object.
        /// </summary>
        public Type DestinationType { get; private set; }
    }
}