

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Test.Api.Security
{
    using Test.Api.Business;

    /// <summary>
    /// Authorization Attribute for limiting access to Controllers or Controller Methods to specific roles.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TestApiAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly ulong _permissions;
        private readonly SecurableObjectType _securableObjectType;
        private readonly string _securableObjectName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestAuthorizationAttribute"/> class.
        /// </summary>
        /// <param name="args">
        /// The arguments.
        /// </param>
        public TestApiAuthorizationAttribute(params object[] args)
        {
            //if (args.Any(p => p is ulong))
            //{
            //    var firstOrDefault = args.FirstOrDefault(p => p is ulong);

            //    if (firstOrDefault != null)
            //    {
            //        _permissions = (ulong)firstOrDefault;
            //    }
            //}
            //else
            //{
            //    _permissions = ulong.MaxValue;
            //}

            //if (args.Any(p => p is SecurableObjectType))
            //{
            //    var firstOrDefault = args.FirstOrDefault(p => p is SecurableObjectType);

            //    if (firstOrDefault != null)
            //    {
            //        _securableObjectType = (SecurableObjectType)firstOrDefault;
            //    }
            //}

            //if (args.Any(p => p is string))
            //{
            //    var firstOrDefault = args.FirstOrDefault(p => p is string);

            //    if (firstOrDefault != null)
            //    {
            //        _securableObjectName = (string)firstOrDefault;
            //    }
            //}
        }

        /// <summary>
        /// Check that the user is authorized to use the controller or method.
        /// </summary>
        /// <param name="actionContext">
        /// The action context for the request.
        /// </param>
        /// <returns>
        /// <c>true</c> if the user has appropriate permissions, else <c>false</c>.
        /// </returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //var categoryId = HttpContext.Current.Request.QueryString[_securableObjectName];
            //var authenticatedUser = (AuthenticatedUser)HttpContext.Current.Items["AuthenticatedUser"];

            //if (_permissions == uint.MaxValue)
            //{
            //    return true;
            //}

            //var securableObjectPermission = authenticatedUser.Permissions.FirstOrDefault(
            //    p => p.SecurableObjectType.Equals(_securableObjectType));

            //if (securableObjectPermission == null)
            //{
            //    return false;
            //}

            //if (securableObjectPermission.SecurableObjectType != SecurableObjectType.Classification
            //    || categoryId == null)
            //{
            //    var retValue = (securableObjectPermission.Permissions & _permissions) != 0;
            //    return retValue;
            //}

            //var securableObjectId = securableObjectPermission.ObjectId.Equals(Guid.Parse(categoryId));

            //if (securableObjectId)
            //{
            //    return (securableObjectPermission.Permissions & _permissions) != 0;
            //}

            //return IsClassificationsBySegment(Guid.Parse(categoryId), authenticatedUser.SId);

            return false;

        }

        private static bool IsClassificationsBySegment(Guid parentClassificationId, Guid sid)
        {
            //var tableService =
            //    DataRepository.Create<ClassificationBySequenceName>();
            //var dataContext = tableService.GetDataContext();

            //if (dataContext == null)
            //{
            //    throw new NullReferenceException("dataContext");
            //}

            //var classficationCount = dataContext.ClassificationBySequenceName
            //    .Where(
            //        a =>
            //            a.PartitionKey ==
            //            TableServiceUtility.CombineToKey(parentClassificationId.ToString(), sid.ToString()))
            //    .AsTableServiceQuery(dataContext).ToList().Count;

            //return classficationCount > 0;

            return false;
        }
    }
}