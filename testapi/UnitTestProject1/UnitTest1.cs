using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Test.Api.Business;
using Test.Api.Core;
using Test.Api.Data;
using Test.Api.Producers;
using Test.Api.Producers.Builders.Objects;
using Test.Api.Services;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int MillisecondsToSleepMessageWhenResourceNotFound = 30000;
            double retryTime = 0;
            for (var i = 1; i <= 21; i++)
            {
                var dequeueCount = i;
                retryTime += dequeueCount * (dequeueCount * MillisecondsToSleepMessageWhenResourceNotFound * .5);
            }
            var tt =TimeSpan.FromMilliseconds(retryTime);


            IUnitOfWork unitOfWork = new UnitOfWork();

            TicketService ticketService = new TicketService(new ObjectFactory(new IObjectBuilder[] { new TicketBuilder(), new TicketEntityBuilder() }), unitOfWork);

            UserService userService = new UserService(new ObjectFactory(new[] { new UserBuilder() }), unitOfWork);

            var user = userService.GetAllUser().FirstOrDefault();
           
            var tic =  ticketService.GetAllTickets(user.Id.ToGuid());
        }
    }
}
