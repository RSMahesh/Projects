
namespace Test.Api.Producers
{
    using Test.Api.Business;
    using Test.Api.HyperMedia;

    /// <summary>
    /// Defines behavior for a class to build a <see cref="JsonHomeDocument"/>.
    /// </summary>
    public interface IJsonHomeDocumentBuilder
    {
        /// <summary>
        /// Builds the resources for.
        /// </summary>
        /// <param name="context">Context containing information about the request.</param>
        /// <returns>
        /// Full built <see cref="JsonHomeDocument"/>.
        /// </returns>
        JsonHomeDocument BuildResourcesFor(ITestApiContext context);
    }
}
