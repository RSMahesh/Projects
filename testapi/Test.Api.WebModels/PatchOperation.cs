// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatchOperation.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The patch operation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.Serialization;
using System.Text;

using Newtonsoft.Json.Linq;
using Test.Api.Core;

namespace Test.Api.WebModels
{
    /// <summary>
    /// The patch operation.
    /// </summary>
    [DataContract]
    public class PatchOperation
    {
        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        [DataMember(Name = "op")]
        public string Operation { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        [DataMember(Name = "path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DataMember(Name = "value")]
        public JToken Value { get; set; }

        /// <summary>
        /// Gets or sets the from.
        /// </summary>
        [DataMember(Name = "from")]
        public string From { get; set; }

        /// <summary>
        /// Returns a string that represents the Patch Operation.
        /// </summary>
        /// <returns>
        /// String that represents the Patch Operation.
        /// </returns>
        public override string ToString()
        {
            var message = new StringBuilder("{ ");
            var comma = string.Empty;
            if (!this.Operation.IsNullOrEmpty())
            {
                message.AppendFormat(@"""op"": ""{0}""", this.Operation);
                comma = ", ";
            }

            if (!this.Path.IsNullOrEmpty())
            {
                message.AppendFormat(@"{1}""path"": ""{0}""", this.Path, comma);
                comma = ", ";
            }

            if (this.Value !=null)
            {
                message.AppendFormat(@"{1}""value"": ""{0}""", this.Value, comma);
                comma = ", ";
            }

            if (!this.From.IsNullOrEmpty())
            {
                message.AppendFormat(@"{1}""from"": ""{0}""", this.From, comma);
            }

            message.Append(" }");

            return message.ToString();
        }
    }
}