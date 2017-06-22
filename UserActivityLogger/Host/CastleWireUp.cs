using ActivityLogger;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Core;
using FileSystem;
using System.IO;
using System.Reflection;

namespace Host
{
    public class CastleWireUp
    {
        public static void WireUp(IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.Kernel.Resolver.AddSubResolver(new AppSettingsDependencyResolver());
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