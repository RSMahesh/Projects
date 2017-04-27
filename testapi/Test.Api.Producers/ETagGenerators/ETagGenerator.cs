// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETagGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   The asset e tag generator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers.ETagGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using Test.Api.Core;
    using Test.Api.Producers;
    using Test.Api.WebModels;

    /// <summary>
    /// The asset e tag generator.
    /// </summary>
    public class ETagGenerator : IETagGenerator<IETagSource>
    {
        /// <summary>
        /// The valid for type.
        /// </summary>
        private static readonly Type ValidForType = typeof(IETagSource);

        /// <summary>
        /// Gets the generates e tags for.
        /// </summary>
        public Type GeneratesETagsFor
        {
            get
            {
                return ValidForType;
            }
        }

        /// <summary>
        /// The generate e tag.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="apiVersion">
        /// The api Version.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GenerateETag(IETagSource data, string apiVersion)
        {
            return this.GenerateETag(new[] { data }, apiVersion);
        }

        /// <summary>
        /// The generate e tag.
        /// </summary>
        /// <param name="assetList">
        /// The asset list.
        /// </param>
        /// <param name="apiVersion">
        /// The api Version.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GenerateETag(IEnumerable<IETagSource> assetList, string apiVersion)
        {
            var dataAsArray = assetList.ToArray();

            if (!dataAsArray.Any())
            {
                return string.Empty;
            }

            var numberOfItems = dataAsArray.Count();
            var firstKey = dataAsArray[0].Id;
            var lastKey = dataAsArray[numberOfItems - 1].Id;
            var lastModified = dataAsArray.Max(data => data.Record.ModifiedAt);

            var identityString = string.Format(
                @"{0}{1}{2}{3}{4}", 
                firstKey, 
                lastKey, 
                lastModified, 
                numberOfItems, 
                apiVersion);

            return this.HashStringToETag(identityString);
        }

        /// <summary>
        /// The hash string to e tag.
        /// </summary>
        /// <param name="identityString">
        /// The identity string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string HashStringToETag(string identityString)
        {
            using (var md5Hash = MD5.Create())
            {
                var result = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(identityString));

                return string.Format("\"{0}\"", result.HashToString());
            }
        }
    }
}