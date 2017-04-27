// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Compressor.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   The compressor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace MediaValet.Api.Compressors
{
    /// <summary>
    /// The compressor.
    /// </summary>
    public abstract class Compressor : ICompressor
    {
        /// <summary>
        /// Gets the encoding type.
        /// </summary>
        public abstract string EncodingType { get; }

        /// <summary>
        /// The create compression stream.
        /// </summary>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/>.
        /// </returns>
        public abstract Stream CreateCompressionStream(Stream output);

        /// <summary>
        /// The create decompression stream.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/>.
        /// </returns>
        public abstract Stream CreateDecompressionStream(Stream input);

        /// <summary>
        /// The compress.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual Task Compress(Stream source, Stream destination)
        {
            var compressed = CreateCompressionStream(destination);

            return Pump(source, compressed)
                .ContinueWith(task => compressed.Dispose());
        }

        /// <summary>
        /// The decompress.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual Task Decompress(Stream source, Stream destination)
        {
            var decompressed = CreateDecompressionStream(source);

            return Pump(decompressed, destination)
                .ContinueWith(task => decompressed.Dispose());
        }

        /// <summary>
        /// The pump.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected virtual Task Pump(Stream input, Stream output)
        {
            return input.CopyToAsync(output);
        }
    }
}