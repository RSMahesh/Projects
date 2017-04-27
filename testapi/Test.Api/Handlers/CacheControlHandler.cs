using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MediaValet.Api
{
    /// <summary>
    /// The cache control attribute.
    /// </summary>
    public class CacheControlHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            response.Headers.CacheControl = new CacheControlHeaderValue
            {
                Private = true,
                NoCache = true
            };

            response.Headers.Add("Pragma", "no-cache");

            return response;
        }
    }
}