// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorCompositionRoot.cs" company="">
//   
// </copyright>
// <summary>
//   Retrieves the controller needed for a call to a specific end-point.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Plumbing
{
    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;

    using Castle.Windsor;

    /// <summary>
    /// Retrieves the controller needed for a call to a specific end-point.
    /// </summary>
    public class WindsorCompositionRoot : IHttpControllerActivator
    {
        /// <summary>
        /// Castle Windsor container.
        /// </summary>
        private readonly IWindsorContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorCompositionRoot"/> class.
        /// </summary>
        /// <param name="container">
        /// Prebuilt Castle Windsor container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The container is not specified.
        /// </exception>
        public WindsorCompositionRoot(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._container = container;
        }

        /// <summary>
        /// Resolve the required controller instance.
        /// </summary>
        /// <param name="request">
        /// The Http request the controller is needed for.
        /// </param>
        /// <param name="controllerDescriptor">
        /// Information that describes the controller needed.
        /// </param>
        /// <param name="controllerType">
        /// <see cref="Type"/> of controller required.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpController"/> needed for the <see cref="HttpRequestMessage"/>.
        /// </returns>
        public IHttpController Create(
            HttpRequestMessage request, 
            HttpControllerDescriptor controllerDescriptor, 
            Type controllerType)
        {
            var controller = (IHttpController)this._container.Resolve(controllerType);

            request.RegisterForDispose(new Release(() => this._container.Release(controller)));

            return controller;
        }

        /// <summary>
        /// Class allow the controller to be disposed.
        /// </summary>
        private class Release : IDisposable
        {
            /// <summary>
            /// Action to dispose the controller.
            /// </summary>
            private readonly Action _release;

            /// <summary>
            /// Initializes a new instance of the <see cref="Release"/> class.
            /// </summary>
            /// <param name="release">
            /// Action to dispose the controller.
            /// </param>
            public Release(Action release)
            {
                if (release == null)
                {
                    throw new ArgumentNullException("release");
                }

                this._release = release;
            }

            /// <summary>
            /// Dispose of this class.
            /// </summary>
            public void Dispose()
            {
                this._release();
            }
        }
    }
}