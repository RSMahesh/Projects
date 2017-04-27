// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationErrorStorage.cs" company="">
//   
// </copyright>
// <summary>
//   Queue to hold failed results from rule checks.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Queue to hold failed results from rule checks.
    /// </summary>
    public sealed class ValidationErrorStorage
    {
        /// <summary>
        /// The _violations.
        /// </summary>
        private readonly Queue<IRuleResult> _violations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrorStorage"/> class.
        /// </summary>
        internal ValidationErrorStorage()
        {
            this._violations = new Queue<IRuleResult>();
        }

        /// <summary>
        /// Adds two queues together.
        /// </summary>
        /// <param name="left">Left side parameter.</param>
        /// <param name="right">Right side parameter.</param>
        /// <returns>Joined queue.</returns>
        /// <example> Example of using the adding 2 queues
        ///     <code title="C#" lang="C#" removeRegionMarkers="true">
        ///         <code source="..\Examples\RuleCheckingServiceExample.cs" region="QueueAddition_CreateRule" />
        ///         <code source="..\Examples\RuleCheckingServiceExample.cs" region="QueueAddition_InstantiateService" />
        ///         <code source="..\Examples\RuleCheckingServiceExample.cs" region="QueueAddition_Validate" />
        ///         <code source="..\Examples\RuleCheckingServiceExample.cs" region="QueueAddition_CheckErrorsAndReport" />
        ///     </code>
        /// </example>
        public static ValidationErrorStorage operator +(ValidationErrorStorage left, ValidationErrorStorage right)
        {
            var newQueue = new ValidationErrorStorage();

            foreach (var ruleViolation in left._violations)
            {
                newQueue.AddAViolation(ruleViolation);
            }

            foreach (var ruleViolation in right._violations)
            {
                newQueue.AddAViolation(ruleViolation);
            }

            return newQueue;
        }

        /// <summary>
        /// Add a violation to the queue.
        /// </summary>
        /// <param name="result">
        /// IRuleResult derived class to add.
        /// </param>
        public void AddAViolation(IRuleResult result)
        {
            if (result.Severity > 0)
            {
                this._violations.Enqueue(result);
            }
        }

        /// <summary>
        /// Determines if the queue contains failed results.
        /// </summary>
        /// <returns><c>True</c> for failed results.  <c>False</c> for no failed results.</returns>
        public bool HasErrors()
        {
            return this._violations.Count > 0;
        }

        /// <summary>
        /// Translates the failed results to a string.
        /// </summary>
        /// <returns>String representation of the failed results.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            var newline = string.Empty;
            foreach (var ex in this._violations)
            {
                sb.Append(newline);
                sb.Append(ex);

                newline = Environment.NewLine;
            }

            return sb.ToString();
        }
    }
}