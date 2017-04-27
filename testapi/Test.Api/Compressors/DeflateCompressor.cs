// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeflateCompressor.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   The deflate compressor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.IO.Compression;

namespace MediaValet.Api.Compressors
{
    /// <summary>
    /// The deflate compressor.
    /// </summary>
    public class DeflateCompressor : Compressor
    {
        private const string DeflateEncoding = "deflate";

        /// <summary>
        /// Gets the encoding type.
        /// </summary>
        public override string EncodingType
        {
            get { return DeflateEncoding; }
        }

        /// <summary>
        /// The create compression stream.
        /// </summary>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/>.
        /// </returns>
        public override Stream CreateCompressionStream(Stream output)
        {
            return new DeflateStream(output, CompressionMode.Compress, true);
        }

        /// <summary>
        /// The create decompression stream.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/>.
        /// </returns>
        public override Stream CreateDecompressionStream(Stream input)
        {
            return new DeflateStream(input, CompressionMode.Decompress, true);
        }
    }
}