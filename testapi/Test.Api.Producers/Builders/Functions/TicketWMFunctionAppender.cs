// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightboxDetailFunctionAppender.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Class implementation for LightboxDetailFunctionAppender.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Test.Api.Business;
using Test.Api.HyperMedia;
using Test.Api.WebModels;
using System.Linq;
using Test.Api.Core;

namespace Test.Api.Producers.Builders.Functions
{
    /// <summary>
    /// Class implementation for LightboxDetailFunctionAppender.cs.
    /// </summary>
    public class TicketWMFunctionAppender : IFunctionAppender
    {
        private static readonly Type CreateLinksForType = typeof (TicketWM);

        private static readonly Dictionary<string, FunctionRequirements> Functions =
            new Dictionary<string, FunctionRequirements>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"ticket", new FunctionRequirements{Permissions = ulong.MaxValue, ResourceEndPoints = FunctionRequirements.AllEndPoints }}
                //{"lightboxAssets", new FunctionRequirements{Permissions = ulong.MaxValue, ResourceEndPoints = FunctionRequirements.AllEndPoints }},
                //{"shareLightboxes", new FunctionRequirements{Permissions = ulong.MaxValue, ResourceEndPoints = FunctionRequirements.AllEndPoints }},
            };

        /// <summary>
        /// Gets the creates links for.
        /// </summary>
        public Type CreatesLinksFor
        {
            get { return CreateLinksForType; }
        }

        /// <summary>
        /// Appends the links to.
        /// </summary>
        /// <param name="webLinkable">The <see cref="IWebLinkable" /> object.</param>
        /// <param name="endPoint"></param>
        /// <param name="context">Context containing information about the request.</param>
        //public void AppendFunctionsTo(IWebLinkable webLinkable, ApiResourceEndPoint endPoint, ITestApiContext context)
        //{
        //    var webLinkableType = webLinkable.GetType();
        //    if (webLinkableType != CreatesLinksFor)
        //    {
        //        context.AddWarning(
        //            string.Format(
        //                @"Trying to add functions to invalid type {0} in LightboxDetailFunctionAppender.  Operation Cancelled",
        //                webLinkableType));
        //        return;
        //    }

        //    var securableObjectPermission =
        //        context.AuthenticatedUser.Permissions.FirstOrDefault(
        //            p => p.SecurableObjectType.Equals(SecurableObjectType.OrgUnit));

        //    webLinkable.Links.Self = "lightbox";

        //    if (securableObjectPermission == null)
        //    {
        //        AddFunctionsToWebLinkable(
        //            f => f.Value.ResourceEndPoints.Contains(endPoint) && f.Value.Permissions == ulong.MaxValue,
        //            webLinkable.Links);

        //        return;
        //    }

        //    AddFunctionsToWebLinkable(
        //        f => f.Value.ResourceEndPoints.Contains(endPoint) && (f.Value.Permissions & securableObjectPermission.Permissions) > 0 || f.Value.Permissions == ulong.MaxValue,
        //        webLinkable.Links);
        //}


        public void AppendFunctionsTo(IWebLinkable webLinkable, ApiResourceEndPoint endPoint, ITestApiContext context)
        {
            var webLinkableType = webLinkable.GetType();
            if (webLinkableType != CreatesLinksFor)
            {
                context.AddWarning(
                    string.Format(
                        @"Trying to add functions to invalid type {0} in LightboxDetailFunctionAppender.  Operation Cancelled",
                        webLinkableType));
                return;
            }
                   
            webLinkable.Links.Self = "tickets";

                AddFunctionsToWebLinkable(
                    f => f.Value.ResourceEndPoints.Contains(endPoint) && f.Value.Permissions == ulong.MaxValue,
                    webLinkable.Links);

        }
        private static void AddFunctionsToWebLinkable(Func<KeyValuePair<string, FunctionRequirements>, bool> func,
            IWebLinks webLinkable)
        {
            Functions.Where(func).ForEach(function => webLinkable.AddFunction(function.Key));
        }
    }
}