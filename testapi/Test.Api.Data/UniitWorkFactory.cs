// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniitWorkFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The unit work factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Data
{
    using Repository;

    /// <summary>
    /// The unit work factory.
    /// </summary>
    public static class UnitWorkFactory
    {
        /// <summary>
        /// The get unit of work.
        /// </summary>
        /// <returns>
        /// The <see cref="IUnitOfWork"/>.
        /// </returns>
        public static IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork();
        }
    }
}