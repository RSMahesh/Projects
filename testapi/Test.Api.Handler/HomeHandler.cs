

namespace Test.Api.Handler
{
   
    using Test.Api.Business;
    using Test.Api.HyperMedia;
    using Test.Api.Producers;

    /// <summary>
    /// Handles the logic for the Home resource end-point.
    /// </summary>
    public class HomeHandler : IHomeHandler
    {
        private readonly IJsonHomeDocumentBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeHandler"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public HomeHandler(IJsonHomeDocumentBuilder builder)
        {
            this._builder = builder;
        }

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="TestContext">The request context.</param>
        /// <returns>
        /// Fully built <see cref="JsonHomeDocument"/>.
        /// </returns>
        public JsonHomeDocument HandleGet(ITestApiContext testApiContext)
        {
            return this._builder.BuildResourcesFor(testApiContext);
        }
    }
}
