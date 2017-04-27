// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleCriticallyViolated.cs" company="">
//   
// </copyright>
// <summary>
//   Indicates that a rule has been violated but should not cause errors for the violation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    /// <summary>
    /// Indicates that a rule has been violated but should not cause errors for the violation.
    /// </summary>
    public sealed class RuleCriticallyViolated : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleCriticallyViolated"/> class.
        /// </summary>
        /// <param name="message">
        /// Message returned from the rule check describing the result.
        /// </param>
        public RuleCriticallyViolated(string message)
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
                return RuleResultSeverity.Critical;
            }
        }
    }
}