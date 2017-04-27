// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonHomeDocumentBuilder.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The <see cref="JsonHomeDocument" /> builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.HyperMedia;

    /// <summary>
    ///     The <see cref="JsonHomeDocument" /> builder.
    /// </summary>
    public class JsonHomeDocumentBuilder : IJsonHomeDocumentBuilder
    {
        private static readonly IDictionary<string, IEnumerable<KeyValuePair<HttpVerb, ulong>>> VerbMapping =
            new Dictionary<string, IEnumerable<KeyValuePair<HttpVerb, ulong>>>()
                {
                    {
                        "user",
                        new[]
                            {
                                new KeyValuePair
                                    <HttpVerb, ulong>(
                                    HttpVerb.GET,
                                    ulong.MaxValue),
                                       new KeyValuePair
                                    <HttpVerb, ulong>(
                                    HttpVerb.POST,
                                    ulong.MaxValue),
                                new KeyValuePair
                                    <HttpVerb, ulong>(
                                    HttpVerb.PUT,
                                    ulong.MaxValue),
                                new KeyValuePair
                                    <HttpVerb, ulong>(
                                    HttpVerb.PATCH,
                                    ulong.MaxValue),
                                new KeyValuePair
                                    <HttpVerb, ulong>(
                                    HttpVerb.DELETE,
                                    ulong.MaxValue)
                            }
                    },
                     {
                "ticket", new[]
                {
                    new KeyValuePair<HttpVerb, ulong>(HttpVerb.GET, ulong.MaxValue),
                    new KeyValuePair<HttpVerb, ulong>(HttpVerb.PUT,
                        ulong.MaxValue),
                    new KeyValuePair<HttpVerb, ulong>(HttpVerb.PATCH,ulong.MaxValue),
                    new KeyValuePair<HttpVerb, ulong>(HttpVerb.DELETE,ulong.MaxValue)
                }
            },
                     {
                        "tickets",
                        new[]
                            {
                                new KeyValuePair
                                    <HttpVerb, ulong>(
                                    HttpVerb.GET,
                                    ulong.MaxValue),
                                       new KeyValuePair
                                    <HttpVerb, ulong>(
                                    HttpVerb.POST,
                                    ulong.MaxValue),
                                          new KeyValuePair<HttpVerb, ulong>(HttpVerb.PUT,
                        ulong.MaxValue),
                                new KeyValuePair
                                    <HttpVerb, ulong>(
                                    HttpVerb.DELETE,
                                    ulong.MaxValue)
                            }
                    },
                };

        private static readonly IDictionary<string, WebFunction> Functions = new Dictionary<string, WebFunction>()
                    {
                        {
                            "user",
                            new WebFunction
                            (
                            "/users/:id",
                            "https://developer.Test.net/rels/users#user_details",
                            true,
                            null,
                            ulong
                            .MaxValue)
                        },
                        {
                    "ticket",
                    new WebFunction(
                        "/tickets/:id",
                        "https://developer.Test.net/rels/assets#asset_details",
                        true,
                        null,
                        ulong.MaxValue)
                },
                        {
                            "tickets",
                            new WebFunction
                            (
                            "/tickets",
                            "https://developer.Test.net/rels/tickets#ticket_details",
                            true,
                            null,
                            ulong
                            .MaxValue)
                        }
                    };

        /// <summary>
        ///     Builds the resources for.
        /// </summary>
        /// <param name="context">Context containing information about the request.</param>
        /// <returns>
        ///     <see cref="JsonHomeDocument" /> populated according to current user's permissions.
        /// </returns>
        public JsonHomeDocument BuildResourcesFor(ITestApiContext context)
        {
            var jsonHomeDocument = new JsonHomeDocument();

            foreach (var function in Functions)
            {
                var functionName = function.Key;
                var functionValue = function.Value;
                var webFunction = new WebFunction(
                    functionValue.Href,
                    functionValue.Relation,
                    functionValue.Templated,
                    CreateHints(functionName, "Active", DetermineAvailableVerbs(functionName, context), null),
                    ulong.MaxValue);

                var listOfHints =
                    (IEnumerable<HttpVerb>)webFunction.Hints.FirstOrDefault(x => x.Key.Contains("allow")).Value;

                if (webFunction.RequiredPermissions > 0 && listOfHints.Any())
                {
                    jsonHomeDocument.AddFunction(functionName, webFunction);
                }
            }

            return jsonHomeDocument;
        }

        private static IDictionary<string, object> CreateHints(
            string command,
            string status,
            IEnumerable<HttpVerb> allow,
            string[] realm)
        {
            IDictionary<string, object> hintCol = new Dictionary<string, object>();
            hintCol.Add("allow", allow);
            hintCol.Add("documentation", string.Format(@"https://developer.Test.net/docs/{0}", command));
            hintCol.Add("formats", new[] { "application/json", "application/xml" });
            hintCol.Add("status", status);
            hintCol.Add(
                "authRequests",
                new AuthHint
                {
                    Scheme = "bearer",
                    Realms = realm
                });
            return hintCol;
        }

        private static IEnumerable<HttpVerb> DetermineAvailableVerbs(string function, ITestApiContext context)
        {
            if (!VerbMapping.ContainsKey(function))
            {
                return Enumerable.Empty<HttpVerb>();
            }

            var securableObjectPermission =
                context.AuthenticatedUser.Permissions.FirstOrDefault(
                    p => p.SecurableObjectType.Equals(SecurableObjectType.Organization));

            if (securableObjectPermission == null)
            {
                return Enumerable.Empty<HttpVerb>();
            }

            return
                VerbMapping[function].Where(
                    mapping =>
                        mapping.Value == ulong.MaxValue || ((mapping.Value & securableObjectPermission.Permissions) > 0))
                    .Select(mapping => mapping.Key);
        }
    }
}
