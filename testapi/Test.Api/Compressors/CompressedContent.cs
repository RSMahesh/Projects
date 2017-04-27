// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompressedContent.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   The compressed content.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MediaValet.Api.Core;
using MediaValet.Core;

namespace MediaValet.Api.Compressors
{
    /// <summary>
    /// The compressed content.
    /// </summary>
    public class CompressedContent : HttpContent
    {
        private readonly HttpContent _content;
        private readonly ICompressor _compressor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressedContent"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="compressor">
        /// The compressor.
        /// </param>
        public CompressedContent(HttpContent content, ICompressor compressor)
        {
            //Ensure.Argument.IsNotNull(content, "content");
            Ensure.Argument.IsNotNull(compressor, "compressor");

            _content = content;
            _compressor = compressor;

            AddHeaders();
        }

        /// <summary>
        /// The try compute length.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        /// <summary>
        /// The serialize to stream async.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            if (_content == null)
            {
                return;
            }

            Ensure.Argument.IsNotNull(stream, "stream");

            using (_content)
            {
                var contentStream = await _content.ReadAsStreamAsync();
                await _compressor.Compress(contentStream, stream);
            }
        }

        private void AddHeaders()
        {
            if (_content == null)
            {
                return;
            }

            foreach (var header in _content.Headers)
            {
                Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            Headers.ContentEncoding.Add(_compressor.EncodingType);
        }
    }
}