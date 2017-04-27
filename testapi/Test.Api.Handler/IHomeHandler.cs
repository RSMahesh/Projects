
namespace Test.Api.Handler
{
    using Test.Api.Business;
    using Test.Api.HyperMedia;

    /// <summary>
    /// Defines the behavior of the handler for dealing with the Home resource endpoint.
    /// </summary>
    public interface IHomeHandler
    {
        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="TestContext">The request context.</param>
        /// <returns>
        /// Fully built <see cref="JsonHomeDocument"/>.
        /// </returns>
        JsonHomeDocument HandleGet(ITestApiContext testApiContext);
    }
}
