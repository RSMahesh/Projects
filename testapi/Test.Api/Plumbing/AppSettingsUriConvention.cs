﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettingsUriConvention.cs" company="">
//   
// </copyright>
// <summary>
//   Allows Uris translated from App or Web .config settings to be injected.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Plumbing
{
    using System;
    using System.Configuration;

    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;

    /// <summary>
    /// Allows Uris translated from App or Web .config settings to be injected.
    /// </summary>
    public class AppSettingsUriConvention : ISubDependencyResolver
    {
        /// <summary>
        /// Returns true if the resolver is able to satisfy this dependency.
        /// </summary>
        /// <param name="context">
        /// Creation context, which is a resolver itself.
        /// </param>
        /// <param name="contextHandlerResolver">
        /// Parent resolver - normally the IHandler implementation.
        /// </param>
        /// <param name="model">
        /// Model of the component that is requesting the dependency.
        /// </param>
        /// <param name="dependency">
        /// The dependency model.
        /// </param>
        /// <returns>
        /// <c>true</c> if the dependency can be satisfied.
        /// </returns>
        public bool CanResolve(
            CreationContext context, 
            ISubDependencyResolver contextHandlerResolver, 
            ComponentModel model, 
            DependencyModel dependency)
        {
            return dependency.TargetType == typeof(Uri);
        }

        /// <summary>
        /// Should return an instance of a service or property values as
        ///     specified by the dependency model instance.
        ///     It is also the responsibility of <see cref="T:Castle.MicroKernel.IDependencyResolver"/>
        ///     to throw an exception in the case a non-optional dependency
        ///     could not be resolved.
        /// </summary>
        /// <param name="context">
        /// Creation context, which is a resolver itself.
        /// </param>
        /// <param name="contextHandlerResolver">
        /// Parent resolver - normally the IHandler implementation.
        /// </param>
        /// <param name="model">
        /// Model of the component that is requesting the dependency.
        /// </param>
        /// <param name="dependency">
        /// The dependency model.
        /// </param>
        /// <returns>
        /// The dependency resolved value or null.
        /// </returns>
        public object Resolve(
            CreationContext context, 
            ISubDependencyResolver contextHandlerResolver, 
            ComponentModel model, 
            DependencyModel dependency)
        {
            var appSettingsKey = dependency.DependencyKey;
            var s = ConfigurationManager.AppSettings[appSettingsKey];
            return new Uri(s);
        }
    }
}