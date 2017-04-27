// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataStoreBase.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The IDataStoreBase interace define property to return type of object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;

namespace Test.Api.Data
{
    /// <summary>
    /// The IDataStoreBase interace define property to return type of object.
    /// </summary>
    public interface IDataStoreBase
    {
        /// <summary>
        /// Property to return type of object.
        /// </summary>
        Type ObjectType { get;}
    }
}
