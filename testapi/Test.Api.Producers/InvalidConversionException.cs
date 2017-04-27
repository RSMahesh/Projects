// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidConversionException.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The invalid conversion exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The invalid conversion exception.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class InvalidConversionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConversionException"/> class.
        /// </summary>
        /// <param name="sourceType">
        /// The SourceType.
        /// </param>
        /// <param name="destinationType">
        /// The DestinationType.
        /// </param>
        public InvalidConversionException(Type sourceType, Type destinationType)
            : base(string.Format(@"Cannot convert from {0} to {1}", sourceType == null ? "[NULL]" : sourceType.Name, destinationType.Name))
        {
            this.SourceType = sourceType;
            this.DestinationType = destinationType;
        }

        /// <summary>
        /// Gets the SourceType type.
        /// </summary>
        public Type SourceType { get; private set; }

        /// <summary>
        /// Gets the DestinationType type.
        /// </summary>
        public Type DestinationType { get; private set; }
    }
}