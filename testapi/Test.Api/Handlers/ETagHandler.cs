using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MediaValet.Api
{
    public class ETagHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
           HttpRequestMessage request,
           CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (ShouldReturnOriginalResponse(request, response))
            {
                return response;
            }

            response.Content = null;

            response.StatusCode = HttpStatusCode.NotModified;

            return response;
        }

        private static bool ShouldReturnOriginalResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            return request.Method != HttpMethod.Get || ResponseHasChanged(request, response.Headers.ETag);
        }

        private static bool ResponseHasChanged(HttpRequestMessage request, EntityTagHeaderValue etag)
        {
            if (etag == null || request.Headers.IfNoneMatch == null)
            {
                return true;
            }

            var retValue = request.Headers.IfNoneMatch.Contains(etag);

            return !retValue;
        }
    }
}