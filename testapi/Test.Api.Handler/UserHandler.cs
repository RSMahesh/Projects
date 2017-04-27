// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserHandler.cs" company="">
//   
// </copyright>
// <summary>
//   Handles the logic for the User resource end-point.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Test.Api.Services;

namespace Test.Api.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Data;
    using Test.Api.Data.Entities;
    using Test.Api.Producers;
    using Test.Api.WebModels;


    /// <summary>
    /// Handles the logic for the User resource end-point.
    /// </summary>
    public class UserHandler : IUserHandler
    {
        /// <summary>
        /// The _response builder.
        /// </summary>
        private readonly IResponseBuilder _responseBuilder;

        /// <summary>
        /// The _object factory.
        /// </summary>
        private readonly IObjectFactory _objectFactory;

        /// <summary>
        /// The unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        private IUserService _userService;

        public UserHandler(IResponseBuilder responseBuilder, IUserService userService, IObjectFactory objectFactory)
        {
            Ensure.Argument.IsNotNull(responseBuilder, "responseBuilder");
            Ensure.Argument.IsNotNull(objectFactory, "objectFactory");
            Ensure.Argument.IsNotNull(userService, "userService");

            _responseBuilder = responseBuilder;
            _objectFactory = objectFactory;
            _userService = userService;
        }


        /// <summary>
        /// The handle get.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="ResponseEnvelope"/>.
        /// </returns>
        public ResponseEnvelope<IEnumerable<UserWM>> HandleGet(ITestApiContext context)
        {
            
            var results = _unitOfWork.GetRepository<UserEntity>().All();

            var users = results.Select(userEntity => _objectFactory.Create<User>(userEntity));

            var usersWM = users.Select(user => _objectFactory.Create<UserWM>(user));

            var recordCounts = CreateRecordCounts(usersWM.Count());

            var searchResults = new ApiSearchResult<IEnumerable<UserWM>>(recordCounts, usersWM);


            var response = _responseBuilder.CreateResponse(context, searchResults);

            return response;
        }

        public ResponseEnvelope<UserWM> HandleCurrentUserGet(ITestApiContext context)
        {

            var user = _userService.GetUser(context.AuthenticatedUser.Id.ToGuid());

            var usersWM = _objectFactory.Create<UserWM>(user);

            var recordCounts = CreateRecordCounts(1);

            var searchResults = new ApiSearchResult<UserWM>(recordCounts, usersWM);

            var response = _responseBuilder.CreateResponse(context, searchResults);

            return response;
        }


        private static RecordCounts CreateRecordCounts(int count)
        {
            return new RecordCounts
            {
                StartingRecord = 0,
                RecordsReturned = count,
                TotalRecordsFound = count
            };
        }

        /// <summary>
        /// The get user.
        /// </summary>
        private void GetUser()
        {



            // var list = new List<Entities.Ticket>();
            // foreach (var ticket in ticketList)
            // {
            // list.Add(ticket);
            // }

            // return list;
        }
    }
}