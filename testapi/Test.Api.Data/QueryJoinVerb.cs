// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryJoinVerb.cs" company="">
//   
// </copyright>
// <summary>
//   Verb used to join one or more  objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Data
{
    /// <summary>
    /// Verb used to join one or more <see cref="ITestApiQuery"/> objects.
    /// </summary>
    public enum QueryJoinVerb
    {
        /// <summary>
        /// No verb needed.
        /// </summary>
        None, 

        /// <summary>
        /// Joins two <see cref="ITestApiQuery"/> objects together with an and.
        /// </summary>
        And, 

        /// <summary>
        /// Joins two <see cref="ITestApiQuery"/> objects together with an or.
        /// </summary>
        Or
    }
}