using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;

namespace StatusMaker.UI
{
    public static class CastleWireUp
    {
        public static void WireUp(IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.Kernel.Resolver.AddSubResolver(new SettingsDependencyResolver());
            container.Kernel.Resolver.AddSubResolver(new AppSettingsDependencyResolver());
            container.Register(
               Classes
                    .FromAssemblyNamed("StatusMaker.Data")
                    .Pick().WithServiceAllInterfaces()
                    .LifestyleSingleton(),
                 Classes
                    .FromAssemblyNamed("StatusMaker.Business")
                    .Pick().WithServiceAllInterfaces()
                    .LifestyleTransient(),
                 Classes
                    .FromAssemblyNamed("StatusMaker")
                    .Pick().WithServiceAllInterfaces()
                    .LifestyleSingleton(),
                 Classes
                    .FromAssemblyNamed("StatusMaker.Data")
                    .Pick().WithServiceAllInterfaces()
                    .LifestyleSingleton()
           );
        }
    }
}