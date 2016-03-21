namespace WebChat.Services.Common
{
    #region Using

    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    #endregion

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
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
