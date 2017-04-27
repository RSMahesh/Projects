// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataValidationService.cs" company="">
//   
// </copyright>
// <summary>
//   A service that checks an object/value against a predefined set of rules.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Core
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Delegate that is used to express what a rule check should look like.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Parameter type of the object/value being checked.
    /// </typeparam>
    /// <param name="obj">
    /// Parameter being checked.
    /// </param>
    /// <returns>
    /// An IRuleResult inherited class.
    /// </returns>
    public delegate IRuleResult Validator<in TEntity>(TEntity obj);

    /// <summary>
    /// A service that checks an object/value against a predefined set of rules.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Parameter type of the object/value being checked.
    /// </typeparam>
    public sealed class DataValidationService<TEntity>
    {
        /// <summary>
        /// The _checks.
        /// </summary>
        private readonly IList<Validator<TEntity>> _checks;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataValidationService{TEntity}"/> class.
        /// </summary>
        /// <param name="rules">
        /// List of rules that the object/value is to checked against.
        /// </param>
        /// <example>
        /// Example of creating a RuleCheckingService object
        ///     <code title="C#" lang="C#" removeRegionMarkers="true">
        /// <code source="..\Examples\RuleCheckingServiceExample.cs" region="VerifyRules_CreateRules">
        /// </code>
        /// <code source="..\Examples\RuleCheckingServiceExample.cs" region="VerifyRules_InstantiateService">
        /// </code>
        /// </code>
        /// </example>
        public DataValidationService(IList<Validator<TEntity>> rules)
        {
            this._checks = rules;
        }

        /// <summary>
        /// Validate the set of rules against the object/value.
        /// </summary>
        /// <param name="arg">
        /// The object/value being checked.
        /// </param>
        /// <returns>
        /// Queue of rule violations found.
        /// </returns>
        /// <example>
        /// Example of using the RuleCheckingService
        ///     <code title="C#" lang="C#" removeRegionMarkers="true">
        /// <code source="..\Examples\RuleCheckingServiceExample.cs" region="VerifyRules_CreateRules">
        /// </code>
        /// <code source="..\Examples\RuleCheckingServiceExample.cs" region="VerifyRules_InstantiateService">
        /// </code>
        /// <code source="..\Examples\RuleCheckingServiceExample.cs" region="VerifyRules_Validate">
        /// </code>
        /// <code source="..\Examples\RuleCheckingServiceExample.cs" region="VerifyRules_CheckErrorsAndReport">
        /// </code>
        /// </code>
        /// </example>
        public ValidationErrorStorage Validate(TEntity arg)
        {
            var queue = new ValidationErrorStorage();

            foreach (var result in this._checks.Select(checkRule => checkRule(arg)))
            {
                queue.AddAViolation(result);

                if (result.Severity == RuleResultSeverity.Fatal)
                {
                    return queue;
                }
            }

            return queue;
        }
    }
}