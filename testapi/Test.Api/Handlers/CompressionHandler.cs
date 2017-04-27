// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompressionHandler.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   The compression handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediaValet.Api.Compressors;
using MediaValet.Api.Core;

namespace MediaValet.Api.Handlers
{
    /// <summary>
    /// The compression handler.
    /// </summary>
    public class CompressionHandler : DelegatingHandler
    {
        /// <summary>
        /// Gets the compressors.
        /// </summary>
        public Collection<ICompressor> Compressors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionHandler"/> class.
        /// </summary>
        public CompressionHandler()
        {
            Compressors = new Collection<ICompressor>
            {
                new GZipCompressor(), 
                new DeflateCompressor()
            };
        }

        /// <summary>
        /// The send async.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (request.Headers.AcceptEncoding.IsNullOrEmpty() || !response.IsSuccessStatusCode)
            {
                return response;
            }

            var encoding = request.Headers.AcceptEncoding.First();

            var compressor =
                Compressors.FirstOrDefault(
                    c => c.EncodingType.Equals(encoding.Value, StringComparison.InvariantCultureIgnoreCase));

            if (compressor != null)
            {
                response.Content = new CompressedContent(response.Content, compressor);
            }

            return response;
        }
    }
}