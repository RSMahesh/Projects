// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RulePassed.cs" company="">
//   
// </copyright>
// <summary>
//   Indicates that a rule has passed the test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    /// <summary>
    /// Indicates that a rule has passed the test.
    /// </summary>
    public sealed class RulePassed : RuleResult
    {
        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The _empty instance.
        /// </summary>
        private static RulePassed _emptyInstance;

        /// <summary>
        /// Prevents a default instance of the <see cref="RulePassed"/> class from being created.
        /// </summary>
        private RulePassed()
            : base(string.Empty)
        {
        }

        /// <summary>
        /// Gets an empty instance.
        /// </summary>
        public static RulePassed Passed
        {
            get
            {
                return BuildOrRetrieveEmptyInstance();
            }
        }

        /// <summary>
        /// Gets the severity of the rule failure.
        /// </summary>
        public override RuleResultSeverity Severity
        {
            get
            {
                return RuleResultSeverity.Passed;
            }
        }

        /// <summary>
        /// The build or retrieve empty instance.
        /// </summary>
        /// <returns>
        /// The <see cref="RulePassed"/>.
        /// </returns>
        private static RulePassed BuildOrRetrieveEmptyInstance()
        {
            lock (Locker)
            {
                if (_emptyInstance == null)
                {
                    _emptyInstance = new RulePassed();
                }
            }

            return _emptyInstance;
        }
    }
}