namespace Test.Api.Business
{
    using System;

    class UserRole
    {
        public Guid ID { get; set; }

        public string GrantedPermissionsList { get; set; }

        public Guid OrgUnitId { get; set; }
    }
}
