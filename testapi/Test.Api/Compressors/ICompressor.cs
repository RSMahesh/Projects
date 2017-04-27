// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICompressor.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   The Compressor interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace MediaValet.Api.Compressors
{
    /// <summary>
    /// The Compressor interface.
    /// </summary>
    public interface ICompressor
    {
        /// <summary>
        /// Gets the encoding type.
        /// </summary>
        string EncodingType { get; }

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
        Task Compress(Stream source, Stream destination);

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
        Task Decompress(Stream source, Stream destination);
    }
}