// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserStatusFlags.cs" company="">
//   
// </copyright>
// <summary>
//   The user status flags.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Business
{
    using System;

    /// <summary>
    /// The user status flags.
    /// </summary>
    [Flags]
    public enum UserStatusFlags
    {
        /// <summary>
        /// The anonymous.
        /// </summary>
        Anonymous, 

        /// <summary>
        /// The approved.
        /// </summary>
        Approved, 

        /// <summary>
        /// The company admin.
        /// </summary>
        CompanyAdmin, 

        /// <summary>
        /// The system admin.
        /// </summary>
        SystemAdmin, 

        /// <summary>
        /// The locked out.
        /// </summary>
        LockedOut, 

        /// <summary>
        /// The recieve alerts.
        /// </summary>
        RecieveAlerts, 

        /// <summary>
        /// The active.
        /// </summary>
        Active, 

        /// <summary>
        /// The expired.
        /// </summary>
        Expired, 

        /// <summary>
        /// The not approved.
        /// </summary>
        NotApproved
    }
}