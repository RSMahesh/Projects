using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Services
{
    using System.Runtime.InteropServices;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Data;
    using Test.Api.Data.Entities;
    using Test.Api.Producers.Translators;

    public class UserService : IUserService
    {
        /// <summary>
        /// The _object factory.
        /// </summary>
        private readonly IObjectFactory _objectFactory;

        /// <summary>
        /// The unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        private IRepository<UserEntity> _userRepository;

        public UserService(IObjectFactory objectFactory, IUnitOfWork unitOfWork)
        {
            Ensure.Argument.IsNotNull(objectFactory, "objectFactory");
            Ensure.Argument.IsNotNull(unitOfWork, "unitOfWork");

            _objectFactory = objectFactory;
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<UserEntity>();
        }

        public User GetUser(Guid userId)
        {
            var userEntity = _userRepository.GetById(userId);

            return _objectFactory.Create<User>(userEntity);
        }

        public User GetUser(string userName, Guid orgUnitId)
        {
            var results = _userRepository.Find(u=> u.UserName == userName && u.OrgUnitId == orgUnitId);

            var users = results.Select(userEntity => _objectFactory.Create<User>(userEntity));

            return users.FirstOrDefault();
        }

        public User UpdateUser(User user)
        {
            var userEntity = _objectFactory.Create<UserEntity>(user);
            _userRepository.Update(userEntity);

            return GetUser(user.Id.ToGuid());
        }

        public IEnumerable<User> GetAllUser()
        {
            var results = _userRepository.All();

            var users = results.Select(userEntity => _objectFactory.Create<User>(userEntity));

            return users;
        }

        public bool ValidateUser(string userName, string password, Guid orgUnitId)
        {
            var user = _userRepository.Find(u => u.Password == password && u.UserName == userName);

            return user.Any();
        }

        public List<ACL> GetUserPermissions(User user)
        {
            var aclRepository = _unitOfWork.GetRepository<ACLEntity>();
            var results = aclRepository.Find(acl => acl.SourceID == user.RoleId);
            //var acls = results.Select(acl => _objectFactory.Create<ACL>(acl));

            var acls = results.Select(
                acl => Translate.From(acl).To<ACL>());

         
            return acls.ToList();
        }

    }
}
