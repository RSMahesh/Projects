// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataStore.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   The IDataStore interface illustrates the common method declaration for CRUD operation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using InfoWebTicketSystem.DAL;

namespace MediaValet.Data.Core
{
    /// <summary>
    /// The IDataStore interface illustrates the common method declaration for CRUD operation.
    /// </summary>
    /// <typeparam name="T">
    /// Object type this data store represents.
    /// </typeparam>
    public interface IDataStore : IReadDataStore, IWriteDataStore, IDeleteDataStore
    {
    }
}
