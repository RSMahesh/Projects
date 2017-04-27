// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryChildren.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   The Categories children model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using MediaValet.Api.Business;
using MediaValet.Api.Core;

namespace MediaValet.Api.Models
{
    /// <summary>
    /// The Categories children model.
    /// </summary>
    public class CategoryChildren
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryChildren"/> class.
        /// </summary>
        /// <param name="subCategories">The sub categories.</param>
        /// <param name="assets">The assets.</param>
        public CategoryChildren(IEnumerable<Category> subCategories, IEnumerable<Asset> assets)
        {
            Ensure.Argument.NotNull(subCategories, "subCategories");
            Ensure.Argument.NotNull(assets, "assets");

            Assets = assets;
            SubCategories = subCategories;
        }

        /// <summary>
        /// Gets the sub categories.
        /// </summary>
        /// <value>
        /// The sub categories.
        /// </value>
        public IEnumerable<Category> SubCategories { get; private set; }

        /// <summary>
        /// Gets the assets.
        /// </summary>
        /// <value>
        /// The assets.
        /// </value>
        public IEnumerable<Asset> Assets { get; private set; }
    }
}