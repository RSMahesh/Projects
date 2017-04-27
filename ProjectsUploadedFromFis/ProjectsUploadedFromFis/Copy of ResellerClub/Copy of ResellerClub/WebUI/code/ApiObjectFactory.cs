using System;
using System.Data;
using System.Configuration;

using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ResellerClub.Interface;
using ResellerClub.Common; 



namespace ResellerClub.WebUI
{
    public static class ApiObjectFactory
    {
        private static IUnityContainer myContainer = null;
        public static  T GetObject<T>() where T: class          
        {
            myContainer =(IUnityContainer) HttpContext.Current.Application["unityContainer"];
            T obj = null;
          
            if (typeof(T) == typeof(ICustomer))
                 obj =(T) myContainer.Resolve<ICustomer>();
            if (typeof(T) == typeof(IDomain))
                obj = (T)myContainer.Resolve<IDomain>();
            if (typeof(T) == typeof(IOrder))
                obj = (T)myContainer.Resolve<IOrder>();
            if (typeof(T) == typeof(ISessionLogger))
                obj = (T)myContainer.Resolve<ISessionLogger>();
            if (typeof(T) == typeof(IPaymentProcessor))
                obj = (T)myContainer.Resolve<IPaymentProcessor>();
            if (typeof(T) == typeof(IPlan))
                obj = (T)myContainer.Resolve<IPlan>();
            if (typeof(T) == typeof(IState))
                obj = (T)myContainer.Resolve<IState>();
            if (typeof(T) == typeof(IExceptionLogger))
               obj = (T)myContainer.Resolve<IExceptionLogger>();

            SessionManager SM = new SessionManager();
            ((IBaseInterface)obj).SessionID = SM.SessionId();
            ((IBaseInterface)obj).UserIP = Helper.GetIPAddress();
            ((IBaseInterface)obj).UserURL = Helper.GetCurrentUrl();
/*
            switch (type)
            {
                case typeof(ICustomer):
                   obj = (T)myContainer.Resolve<ICustomer>();
                   break;
                case typeof(IDomain):
                   obj = (T)myContainer.Resolve<IDomain>();
                   break;
                case typeof(IOrder):
                   obj = (T)myContainer.Resolve<IOrder>();
                   break;
                case typeof(ISessionLogger):
                   obj = (T)myContainer.Resolve<ISessionLogger>();
                   break;
                case typeof(ICartLogger) :
                   obj = (T)myContainer.Resolve<ICartLogger>();
                   break;
                case typeof(IPayPalTranscationLogger):
                   obj = (T)myContainer.Resolve<IPayPalTranscationLogger>();
                   break;
            }
*/
            return obj  ;
            
        }

        public static void fun()
        {
          ICustomer obj =  ApiObjectFactory.GetObject<ICustomer>();
        }
    }
}