// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleResult.cs" company="">
//   
// </copyright>
// <summary>
//   Abstract class representing the results from a rule check.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    /// <summary>
    /// Abstract class representing the results from a rule check.
    /// </summary>
    public abstract class RuleResult : IRuleResult
    {
        /// <summary>
        /// The _message.
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleResult"/> class.
        /// </summary>
        /// <param name="message">
        /// Message returned from the rule check describing the result.
        /// </param>
        protected RuleResult(string message)
        {
            this._message = message;
        }

        /// <summary>
        /// Gets the severity of the rule failure.
        /// </summary>
        public abstract RuleResultSeverity Severity { get; }

        /// <summary>
        /// Returns a string that represents the result.
        /// </summary>
        /// <returns>
        /// String that represents the result.
        /// </returns>
        public override string ToString()
        {
            return this._message;
        }
    }
}