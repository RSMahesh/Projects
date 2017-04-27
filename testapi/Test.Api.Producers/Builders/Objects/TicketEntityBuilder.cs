// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserBuilder.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   Class implementation for UserBuilder.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Entities;

namespace Test.Api.Producers.Builders.Objects
{
    using System;
    using System.Linq;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Data.Entities;
    using Test.Api.Producers.Translators;
    using Test.Api.WebModels;

    /// <summary>
    /// Class implementation for UserBuilder.cs.
    /// </summary>
    public class TicketEntityBuilder : IObjectBuilder
    {
        private static readonly Type[] ValidTypes =
            {
                typeof (Ticket)
            };

        private static readonly Type BuildsForType = typeof(TicketEntity);

        /// <summary>
        /// Gets the builds object for.
        /// </summary>
        public Type BuildsObjectFor
        {
            get { return BuildsForType; }
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Build<T>(params object[] args)
        {
            Ensure.That<InvalidConversionException>(args.Any(), null, BuildsForType);

            var argType = args[0].GetType();

            Ensure.That<InvalidConversionException>(
                ValidTypes.Contains(argType),
                args[0].GetType(),
                BuildsForType);

            var user = TranslateToTicketEntity((dynamic)args[0]);

            return (T)(object)user;
        }

        private static TicketEntity TranslateToTicketEntity(Ticket ticket)
        {
            var ticketEntity = Translate.From(ticket).To<TicketEntity>();
            return ticketEntity;
        }

    }
}