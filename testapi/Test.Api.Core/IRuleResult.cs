// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRuleResult.cs" company="">
//   
// </copyright>
// <summary>
//   Indicates the severity of the rule check result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    /// <summary>
    /// Indicates the severity of the rule check result.
    /// </summary>
    public enum RuleResultSeverity
    {
        /// <summary>
        /// Rule passed the test.
        /// </summary>
        Passed, 

        /// <summary>
        /// Rule failed, but checking can still be completed.
        /// </summary>
        Critical, 

        /// <summary>
        /// Rule failed and checking must be stopped and error reported.
        /// </summary>
        Fatal
    }

    /// <summary>
    /// Contract for classes returning results of a rule check.
    /// </summary>
    public interface IRuleResult
    {
        /// <summary>
        /// Gets the severity of the rule failure.
        /// </summary>
        RuleResultSeverity Severity { get; }
    }
}