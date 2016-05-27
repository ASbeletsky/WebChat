namespace WebChat.Services.Common
{
    
    #region Using

    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Interfaces;

    #endregion

    public class NinjectDependencyContainer : IDependencyResolver, IDependencyContainer
    {
        private IKernel kernel;
        public NinjectDependencyContainer(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public T GetService<T>()
        {
            return kernel.TryGet<T>();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            kernel.Load(assemblies);
        }
    }
}
