using InfoWebTicketSystem.BRL.Interface;
using InfoWebTicketSystem.BRL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfoWebTicketSystem.MVC
{
    public class UnityDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        IUnityContainer container;
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }
        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
        public void Dispose()
        {
            container.Dispose();
        }
        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            throw new NotImplementedException();
        }
    }

    public class UnityRegisterTypesConfig
    {
        IUnityContainer container;
        public UnityRegisterTypesConfig(IUnityContainer container)
        {
            this.container = container;
            
        }
        public void Register()
        {
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<ITicketService, TicketService>();
        }
    }

}