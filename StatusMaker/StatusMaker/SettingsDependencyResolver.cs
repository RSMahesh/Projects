// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettingsStringConvention.cs" company="">
//   
// </copyright>
// <summary>
//   The app settings string convention.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;

using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;

using StatusMaker.UI.Properties;

namespace StatusMaker.UI
{
    /// <summary>
    /// Allows strings from App or Web .config files to be injected.
    /// </summary>
    public class SettingsDependencyResolver : ISubDependencyResolver
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
            return dependency.TargetType == typeof(string) && this.SettingExist(dependency);
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
            var s = Settings.Default[dependency.DependencyKey];
            return Convert.ChangeType(s, dependency.TargetType);
        }


        private bool SettingExist(DependencyModel dependency)
        {
            try
            {
                var settingValue = Settings.Default[dependency.DependencyKey];
            }
            catch (SettingsPropertyNotFoundException)
            {

                return false;
            }
            return true;
        }
    }
}