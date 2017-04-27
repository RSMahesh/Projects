using System;
using System.Collections.Generic;

using Test.Api.Core;
using Test.Api.Data.Entities;
using Test.Api.Data.Queries;

namespace Test.Api.Data
{
    public class UserDataStore : IApiDataStore<UserEntity>
    {

        public IApiSearchResult<UserEntity> Insert(UserEntity item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(UserEntity item)
        {
            throw new System.NotImplementedException();
        }

        public IApiSearchResult<UserEntity> Update(UserEntity item)
        {
            throw new System.NotImplementedException();
        }

        public IApiSearchResult<System.Collections.Generic.IEnumerable<UserEntity>> Read(Core.IQuery query)
        {
            throw new System.NotImplementedException();
        }

        public System.Type ObjectType
        {
            get { return typeof(UserEntity); }
        }

        private static IApiSearchResult<IEnumerable<UserEntity>> ExecuteSelect(
        SelectUserByUserAndOrgUnitID query)
        {
            //string userName = query.SearchValue.ToString();
            //var appUser = new AppUser(userName);

            //if (appUser.UserId.CompareTo(Guid.Empty) != 0)
            //{
            //    return CreateSearchResult(new[] { appUser }, context);
            //}

            //context.AddError(string.Format("User not found: username: {0}", userName));
            //throw new ResourceNotFoundException(userName);

            return null;
        }
    }
}
