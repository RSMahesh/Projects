// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfiguration.cs" company="">
//   
// </copyright>
// <summary>
//   The web api configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using System.Web.Http.Batch;
    using System.Web.Http.Controllers;

    using Castle.Core;
    using Castle.DynamicProxy;
    using Castle.Facilities.Logging;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.Windsor;

    using Newtonsoft.Json.Converters;

    using Repository;

    using Test.Api.Core;
    using Test.Api.Plumbing;
    using Test.Api.Plumbing;
    using Test.Api.Security;

    using WebApiContrib.IoC.CastleWindsor;

    /// <summary>
    /// Configure the Web.API application to run properly.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class WebApiConfiguration
    {
        /// <summary>
        /// Sets all the values needed to run Test.API.
        /// </summary>
        /// <param name="config">
        /// Http configuration values.
        /// </param>
        public static void Configure(HttpConfiguration config)
        {
            HttpBatchHandler batchHandler = new DefaultHttpBatchHandler(GlobalConfiguration.DefaultServer)
                                                {
                                                    ExecutionOrder
                                                        =
                                                        BatchExecutionOrder
                                                        .NonSequential
                                                };

            config.Routes.MapHttpBatchRoute("Bulk", "bulk", batchHandler);
            config.Routes.MapHttpRoute("ApiWithFormat", "{controller}/{id}.{ext}", new { ext = "json" });
            config.Routes.MapHttpRoute("ApiBasicWithFormat", "{controller}.{ext}", new { ext = "json" });
            config.Routes.MapHttpRoute("ApiBasic", "{controller}/{id}", new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute("RootInformation", string.Empty, new { Controller = "Home", Action = "Get" });
            config.Routes.MapHttpRoute("ApiPublic", "public/{controller}/{id}", new { id = RouteParameter.Optional });

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

             config.MessageHandlers.Add(new BearerTokenAuthenticationHandler());
            // config.MessageHandlers.Add(new CacheControlHandler());
            // config.MessageHandlers.Add(new ETagHandler()); 
            // config.MessageHandlers.Insert(0, new CompressionHandler());
            ConfigureFormatters(config);
            ConfigureCastlerWindsor(config);
        }

        /// <summary>
        /// The configure formatters.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        private static void ConfigureFormatters(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
            config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "application/xml");

            // config.Formatters.Add(new JsonPatchFormatter());
        }

        /// <summary>
        /// The configure castler windsor.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", 
            Justification = "This is a stupid rule.")]
        private static void ConfigureCastlerWindsor(HttpConfiguration config)
        {
            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.Kernel.Resolver.AddSubResolver(new AppSettingsStringConvention());
            container.Kernel.Resolver.AddSubResolver(new AppSettingsUriConvention());
            container.Kernel.Resolver.AddSubResolver(new AppSettingsPasswordFormatConvention());

            container.AddFacility<LoggingFacility>(f => f.ConfiguredExternally());

            container.Register(
                Component.For<IInterceptor>().ImplementedBy<LogInterceptor>().Named("LogInterceptor"), 
                Classes.FromAssemblyNamed("Test.Api.Business")
                    .Pick()
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton()
                    .Configure(r => { var x = r.Interceptors(InterceptorReference.ForKey("LogInterceptor")).Anywhere; }), 
                Classes.FromAssemblyNamed("Test.Api.Core")
                    .Pick()
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton()
                    .Configure(r => { var x = r.Interceptors(InterceptorReference.ForKey("LogInterceptor")).Anywhere; }), 
                Classes.FromAssemblyNamed("Test.Api.Producers")
                    .Pick()
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton()
                    .Configure(r => { var x = r.Interceptors(InterceptorReference.ForKey("LogInterceptor")).Anywhere; }), 
                Classes.FromAssemblyNamed("Test.Api")
                    .BasedOn<IHttpController>()
                    .WithServiceSelf()
                    .LifestyleTransient()
                    .Configure(r => { var x = r.Interceptors(InterceptorReference.ForKey("LogInterceptor")).Anywhere; }), 
                Classes.FromAssemblyNamed("Test.Api.Handler")
                    .Pick()
                    .WithServiceAllInterfaces()
                    .LifestyleTransient()
                    .Configure(r => { var x = r.Interceptors(InterceptorReference.ForKey("LogInterceptor")).Anywhere; }), 
                Classes.FromAssemblyNamed("Test.Api.HyperMedia")
                    .Pick()
                    .WithServiceAllInterfaces()
                    .LifestyleTransient()
                    .Configure(r => { var x = r.Interceptors(InterceptorReference.ForKey("LogInterceptor")).Anywhere; }), 
                Classes.FromAssemblyNamed("Test.Api.Data")
                    .Pick()
                    .WithServiceAllInterfaces()
                    .LifestyleTransient()
                    .Configure(r => { var x = r.Interceptors(InterceptorReference.ForKey("LogInterceptor")).Anywhere; }),
                Classes.FromAssemblyNamed("Test.Api.Services")
                    .Pick()
                    .WithServiceAllInterfaces()
                    .LifestyleTransient()
                    .Configure(r => { var x = r.Interceptors(InterceptorReference.ForKey("LogInterceptor")).Anywhere; }), 
              Classes.FromThisAssembly()
                    .BasedOn<IHtmlResponseBuilder>()
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton()
                    .Configure(r => { var x = r.Interceptors(InterceptorReference.ForKey("LogInterceptor")).Anywhere; }));


            config.DependencyResolver = new WindsorResolver(container);
        }
    }
}