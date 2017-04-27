// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FunctionRequirements.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Class implmentation for FunctionRequirements.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using Test.Api.Core;

namespace Test.Api.Producers.Builders.Functions
{
    internal class FunctionRequirements
    {
        public static ApiResourceEndPoint[] AllEndPoints =
        {
            ApiResourceEndPoint.Tickets,
            ApiResourceEndPoint.Users
        };

        public ulong Permissions { get; set; }
        public ApiResourceEndPoint[] ResourceEndPoints { get; set; }
    }
}