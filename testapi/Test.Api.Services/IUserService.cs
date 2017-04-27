namespace Test.Api.Services
{
    using System;
    using System.Collections.Generic;
    using Test.Api.Business;

    public interface IUserService
    {
        User GetUser(Guid userId);

        User GetUser(string userName, Guid orgUnitId);


        User UpdateUser(User user);

        IEnumerable<User> GetAllUser();

        bool ValidateUser(string userName, string password, Guid orgUnitId);


        List<ACL> GetUserPermissions(User user);

    }
}