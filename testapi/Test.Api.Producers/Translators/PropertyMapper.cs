// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyMapper.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Maps the names of properties from one class to another.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers.Translators
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Maps the names of properties from one class to another.
    /// </summary>
    internal class PropertyMapper
    {
        private static readonly IList<string[]> PropertyNameList = new List<string[]>
        {
            new[] { "CategoryContainer.Id", "Category.ContainerId" },
            new[] { "CategoryTreeInformation.Path", "Category.ClassificationTreePath" },
            new[] { "CategoryTreeInformation.Name", "Category.LocalName" },
            new[] { "SubCategoryInformation.Total", "Category.ChildCount" },
            new[] { "RecordDetails.CreatedAt", "Category.AddedOn" },
            new[] { "RecordDetails.ModifiedAt", "Category.ModifiedOn" },
            new[] { "UserSummary.Id", "Category.AddedById", "Category.LastModifiedById" },
            new[] { "Folder.Name", "Category.LocalName" },
            new[] { "Folder.SubFolderCount", "Category.ChildCount" },
            new[] { "Folder.LastModifiedAt", "Category.ModifiedOn" },
            new[] { "Category.ChildCount", "Classification.ChildrenCount" },
            new[] { "Category.AddedOn", "Classification.DateAdded" },
            new[] { "Category.AddedById", "Classification.AddedByUserId" },
            new[] { "Category.ModifiedOn", "Classification.LastModificationDate" },
            new[] { "Category.LastModifiedById", "Classification.LastModifiedByUserId" },
            new[] { "RecordInformation.CreatedAt", "AssetRecordDetails.CreatedAt" },
            new[] { "RecordInformation.ModifiedAt", "AssetRecordDetails.ModifiedAt" },
            new[] { "MediaInformation.AssetType", "MediaInformationDetail.Type" },
            new[] { "ShareSettings.Text", "MessageDetail.Text" },
            new[] { "ShareSettings.Recipients", "MessageDetail.Recipients" },
            new[] { "Lightbox.ModifiedAt", "RecordDetails.ModifiedAt" },
            new[] { "VersionInformation.Current", "AssetVersionInformation.ActiveAssetId"},
            new[] { "VersionInformation.Head", "AssetVersionInformation.HeadAssetId"},
            new[] { "VersionInformation.ParentId", "AssetVersionInformation.ParentAssetId"},
            new[] { "KeywordDetail.Id", "Keyword.KeywordName" }
        };

        private static readonly IDictionary<string, Dictionary<string, string>> PropertyMappingHash;

        /// <summary>
        /// Initializes static members of the <see cref="PropertyMapper"/> class.
        /// </summary>
        static PropertyMapper()
        {
            PropertyMappingHash =
                new Dictionary<string, Dictionary<string, string>>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var propertyList in PropertyNameList)
            {
                ProcessMappingList(propertyList);
            }
        }

        /// <summary>
        /// Determines the Property mapping from one class to another.
        /// </summary>
        /// <param name="sourceType">
        /// The <see cref="Type"/> of the source class.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property the data is in.
        /// </param>
        /// <param name="destinationType">
        /// The <see cref="Type"/> of the destination class.
        /// </param>
        /// <returns>
        /// The name of the property to put the data in.
        /// </returns>
        public string DeterminePropertyInSource(Type sourceType, string propertyName, Type destinationType)
        {
            var typeFieldName = string.Format("{0}.{1}", destinationType.Name, propertyName);

            if (!PropertyMappingHash.ContainsKey(typeFieldName))
            {
                return MapPropertyToWithSameNameOrUseEmptyString(sourceType, propertyName);
            }

            var sourceTypeName = sourceType.Name;
            return PropertyMappingHash[typeFieldName].ContainsKey(sourceTypeName) 
                ? PropertyMappingHash[typeFieldName][sourceTypeName]
                : MapPropertyToWithSameNameOrUseEmptyString(sourceType, propertyName);
        }

        private static bool PropertyExistsInSource(string propertyName, Type destinationType)
        {
            return destinationType.GetProperty(propertyName) != null;
        }

        private static string MapPropertyToWithSameNameOrUseEmptyString(Type sourceType, string propertyName)
        {
            return PropertyExistsInSource(propertyName, sourceType) 
                ? propertyName 
                : string.Empty;
        }

        private static void ProcessMappingList(string[] propertyList)
        {
            foreach (var propertyName in propertyList)
            {
                var mapToPropertyType = propertyName.Split('.')[0];
                var propMapping = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                foreach (var splitName in PropertlyListSplit(propertyName, propertyList))
                {
                    var mapFromPropertyType = splitName[0];

                    // No need to map properties within the same type
                    if (!PropertiesAreFromTheSameClass(mapToPropertyType, mapFromPropertyType))
                    {
                        propMapping[mapFromPropertyType] = splitName[1];
                    }
                }

                if (PropertyMappingHash.ContainsKey(propertyName))
                {
                    AddMappingsToExisting(propMapping, propertyName);
                }
                else
                {
                    PropertyMappingHash[propertyName] = propMapping;
                }
            }
        }

        private static bool PropertiesAreFromTheSameClass(string mapToPropertyType, string mapFromPropertyType)
        {
            return mapToPropertyType == mapFromPropertyType;
        }

        private static void AddMappingsToExisting(Dictionary<string, string> propMapping, string propertyName)
        {
            foreach (var fieldMap in propMapping)
            {
                PropertyMappingHash[propertyName].Add(fieldMap.Key, fieldMap.Value);
            }
        }

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1501:StatementMustNotBeOnSingleLine", Justification = "Dumb Rule")]
        private static IEnumerable<string[]> PropertlyListSplit(string currentProperty, IEnumerable<string> propertyList)
        {
            var propertlyListSplit = propertyList
                .Where(p => p != currentProperty)
                .Select(remainingProperty =>
                        remainingProperty.Split(
                        new[] { '.' },
                        StringSplitOptions.RemoveEmptyEntries));
            return propertlyListSplit;
        }
    }
}