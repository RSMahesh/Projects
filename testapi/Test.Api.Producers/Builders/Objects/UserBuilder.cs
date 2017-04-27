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
    public class UserBuilder : IObjectBuilder
    {
        private static readonly DateTime MinSupportedDateTime = DateTime.Parse("1753-01-01T00:00:00.0000000Z ").ToUniversalTime();
        private static readonly Type[] ValidTypes =
            {
                typeof (UserEntity),
                typeof (UserWM),
            };

        private static readonly Type BuildsForType = typeof(User);

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

            var user = TranslateToUser((dynamic)args[0]);

            return (T)(object)user;
        }

        private static User TranslateToUser(UserEntity userEntity)
        {
            //var user = new User(EntityId.Empty, new EmailAddress(userEntity.UserName));
            //return Translate.From(userEntity).MappProperties<User>(user);

            var expiresAt = userEntity.ExpiresAt < MinSupportedDateTime
                ? MinSupportedDateTime
                : userEntity.ExpiresAt;
            var user = new User(userEntity.Id.ToEntityId(), new EmailAddress(userEntity.UserName))
            {
                AdditionalMessage =
                    !string.IsNullOrEmpty(userEntity.AdditionalMessage)
                        ? userEntity.AdditionalMessage.ToSafeString()
                        : SafeString.Empty,
                Address =
                    !string.IsNullOrEmpty(userEntity.Address)
                        ? new StreetAddress(userEntity.Address)
                        : StreetAddress.Empty,
                AdminNotes =
                    !string.IsNullOrEmpty(userEntity.AdminNotes)
                        ? userEntity.AdminNotes.ToSafeString()
                        : SafeString.Empty,
                AlertsEnabled = userEntity.AlertsEnabled,
                CellularNumber =
                    !string.IsNullOrEmpty(userEntity.CellularNumber)
                        ? new PhoneNumber(userEntity.CellularNumber)
                        : PhoneNumber.Empty,
                Comment =
                    !string.IsNullOrEmpty(userEntity.Comment)
                        ? userEntity.Comment.ToSafeString()
                        : SafeString.Empty,
                CreatedAt = DateTime.MinValue,
                Department =
                 !string.IsNullOrEmpty(userEntity.Department)
                     ? userEntity.Department.ToSafeString()
                     : SafeString.Empty,
                EmailAddress =
                    !string.IsNullOrEmpty(userEntity.EmailAddress)
                        ? new EmailAddress(userEntity.EmailAddress)
                        : EmailAddress.Empty,
                ExpiresAt = userEntity.ExpiresAt ?? DateTime.MaxValue,
                FaxNumber =
                    !string.IsNullOrEmpty(userEntity.FaxNumber)
                        ? new PhoneNumber(userEntity.FaxNumber)
                        : PhoneNumber.Empty,
                FirstName =
                    !string.IsNullOrEmpty(userEntity.FirstName)
                        ? userEntity.FirstName.ToSafeString()
                        : SafeString.Empty,
                LastActivityDate = DateTime.MinValue,
                LastLockoutDate = DateTime.MinValue,
                LastLoginDate = DateTime.MinValue,
                LastName =
                    !string.IsNullOrEmpty(userEntity.LastName)
                        ? userEntity.LastName.ToSafeString()
                        : SafeString.Empty,
                // NewUserNotification = userEntity.NewUserNotification,
                OfficeNumber =
                    !string.IsNullOrEmpty(userEntity.OfficeNumber)
                        ? new PhoneNumber(userEntity.OfficeNumber)
                        : PhoneNumber.Empty,
                // OrganizationName = userEntity.OrganizationName.ToSafeString(),
                PasswordInformation = new PasswordInformation
                {
                    Password = new Password(userEntity.Password),
                    RecoveryQuestion = userEntity.PasswordQuestion.ToSafeString(),
                    RecoveryAnswer = userEntity.PasswordAnswer.ToSafeString()
                },
                Position =
                    !string.IsNullOrEmpty(userEntity.Position)
                        ? userEntity.Position.ToSafeString()
                        : SafeString.Empty,
                Title =
                    !string.IsNullOrEmpty(userEntity.Title) ? userEntity.Title.ToSafeString() : SafeString.Empty,
                Website =
                    !string.IsNullOrEmpty(userEntity.Website)
                        ? userEntity.Website.ToSafeString()
                        : SafeString.Empty,
                RoleId = userEntity.RoleId
            };

            return user;
        }

    }
}