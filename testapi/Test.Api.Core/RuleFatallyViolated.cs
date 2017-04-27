// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleFatallyViolated.cs" company="">
//   
// </copyright>
// <summary>
//   Indicates that a rule has been violated and any rule following should be stopped until
//   this one is fixed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    /// <summary>
    /// Indicates that a rule has been violated and any rule following should be stopped until
    /// this one is fixed.
    /// </summary>
    public sealed class RuleFatallyViolated : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleFatallyViolated"/> class.
        /// </summary>
        /// <param name="message">
        /// Message returned from the rule check describing the result.
        /// </param>
        public RuleFatallyViolated(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Gets the severity of the rule failure.
        /// </summary>
        public override RuleResultSeverity Severity
        {
            get
            {
                return RuleResultSeverity.Fatal;
            }
        }
    }
}