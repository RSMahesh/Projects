// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileTypeDetailBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The file type detail builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Producers.Builders.Objects
{
    using System;

    using Test.Api.Business;
    using Test.Api.Core;
    using Test.Api.Producers.Translators;
    using Test.Api.WebModels;

    public class UserWmBuilder : IObjectBuilder
    {
        /// <summary>
        /// The builds for type.
        /// </summary>
        private static readonly Type BuildsForType = typeof(UserWM);

        /// <summary>
        /// Gets the builds object for.
        /// </summary>
        public Type BuildsObjectFor
        {
            get
            {
                return BuildsForType;
            }
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
            var userType = args[0] as User;

            Ensure.That<InvalidConversionException>(userType != null, args[0].GetType(), BuildsForType);

            var fileTypeDetail = Translate.From(userType).To<UserWM>();

            return (T)(object)fileTypeDetail;
        }
    }
}