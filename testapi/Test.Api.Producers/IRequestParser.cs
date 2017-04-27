// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestParser.cs" company="">
//   
// </copyright>
// <summary>
//   The RequestParser interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Producers
{
    using System.Collections.Generic;

    using Test.Api.Business;
    using Test.Api.WebModels;

  
    public interface IRequestParser
    {
        ITestApiContext ParseBodyToContext(
            RequestBody body, 
            IEnumerable<KeyValuePair<string, string>> queryKeyValuePairs, 
            AuthenticatedUser authenticatedUser, 
            EntityId organizationUnitId, 
            EntityId repositoryId);

  
        ITestApiContext ParseQueryStringToContext(
            IEnumerable<KeyValuePair<string, string>> queryKeyValuePairs, 
            AuthenticatedUser authenticatedUser, 
            EntityId organizationUnitId, 
            EntityId repositoryId);
    }
}