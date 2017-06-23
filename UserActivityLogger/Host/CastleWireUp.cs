using ActivityLogger;
using BigFun.Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Core;
using FileSystem;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Host
{
    public class CastleWireUp
    {
        public static void WireUp(IWindsorContainer container, Dictionary<string, string> defaultSettings)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.Kernel.Resolver.AddSubResolver(new AppSettingsDependencyResolver(defaultSettings));
            container.Register(
               Classes
                    .FromAssemblyNamed("ActivityLogger")
                    .Pick().WithServiceAllInterfaces()
                    .LifestyleSingleton(),
                 Classes
                    .FromAssemblyNamed("Core")
                    .Pick().WithServiceAllInterfaces()
                    .LifestyleSingleton(),
                 Classes
                    .FromAssemblyNamed("EventPublisher")
                    .Pick().WithServiceAllInterfaces()
                    .LifestyleSingleton(),
                   Classes
                    .FromAssemblyNamed("FileSystem")
                    .Pick().WithServiceAllInterfaces()
                    .LifestyleSingleton(),
                   Classes
                    .FromAssemblyNamed("KeyBoardEventsListener")
                    .Pick().WithServiceAllInterfaces()
                    .LifestyleSingleton()
           );
        }
    }
}