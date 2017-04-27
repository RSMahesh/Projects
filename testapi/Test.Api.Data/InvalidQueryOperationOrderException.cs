// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidQueryOperationOrderException.cs" company="">
//   
// </copyright>
// <summary>
//   Exception indicating that either an  or
//   operation was tried before a  .
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Data
{
    using System;

    /// <summary>
    /// Indicates that either an <see cref="TestApiQuery.And"/> or <see cref="TestApiQuery.Or"/>
    ///     operation was tried before a  <see cref="TestApiQuery.Where"/>.
    /// </summary>
    public sealed class InvalidQueryOperationOrderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidQueryOperationOrderException"/> class.
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        public InvalidQueryOperationOrderException(string operation)
            : base(string.Format(@"Cannot invoke {0} clause before Where.", operation))
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Gets the operation that caused the Exception.
        /// </summary>
        public string Operation { get; private set; }
    }
}