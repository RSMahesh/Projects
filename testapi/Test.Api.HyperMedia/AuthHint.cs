

namespace Test.Api.HyperMedia
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an Authorization Hint.
    /// </summary>
    [DataContract]
    public class AuthHint
    {
        /// <summary>
        /// Gets or sets the scheme.
        /// </summary>
        /// <value>
        /// The scheme.
        /// </value>
        [DataMember(Name = "scheme", EmitDefaultValue = false)]
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the realms.
        /// </summary>
        /// <value>
        /// The realms.
        /// </value>
        [DataMember(Name = "realms", EmitDefaultValue = false)]
        public string[] Realms { get; set; }
    }
}