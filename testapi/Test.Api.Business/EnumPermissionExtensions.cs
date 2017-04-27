// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumPermissionExtensions.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The EnumPermissionExtensions class illustrates the extension methods of EnumPermission.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Business
{
    /// <summary>
    /// Extension methods for <see cref="Permission" />.
    /// </summary>
    public static class EnumPermissionExtensions
    {
        /// <summary>
        /// Shifts the specified <see cref="Permission" />.
        /// </summary>
        /// <param name="permission">
        /// The <see cref="Permission" /> permission.
        /// </param>
        /// <returns>
        /// The bitwise representation of the permission.
        /// </returns>
        public static ulong Shift(this Permission permission)
        {
            return permission == Permission.All ? ulong.MaxValue : 1UL << (int)permission;
        }
    }
}