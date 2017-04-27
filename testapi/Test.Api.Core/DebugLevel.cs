// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugLevel.cs" company="">
//   
// </copyright>
// <summary>
//   Used to decide which debug information needs to be included in the response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Core
{
    /// <summary>
    /// Used to decide which debug information needs to be included in the response.
    /// </summary>
    public enum DebugLevel
    {
        /// <summary>
        /// Include no debug information.
        /// </summary>
        NoDebug, 

        /// <summary>
        /// Include all debug information.
        /// </summary>
        FullDebug, 

        /// <summary>
        /// Include only the representation of the internal query.
        /// </summary>
        InternalQueryOnly, 

        /// <summary>
        /// Include elapsed times at checkpoints through the request.
        /// </summary>
        TimingsOnly
    }
}