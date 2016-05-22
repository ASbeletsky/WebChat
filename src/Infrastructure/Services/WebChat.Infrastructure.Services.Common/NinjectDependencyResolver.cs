namespace WebChat.Infrastructure.Services.Common
{

    #region Using

    using Ninject;
    using System;
    using System.Collections.Generic;

    using Interfaces;
    using System.Reflection;
    using System.IO;
    #endregion

    public class NinjectDependencyResolver : System.Web.Mvc.IDependencyResolver, IDependencyResolver
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
            string folder =  Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin\\");
            var assemblies = new List<Assembly>
            {
                 Assembly.LoadFile(folder +  "WebChat.Services.Common.dll"),
                 Assembly.LoadFile(folder +  "WebChat.Business.Services.dll")
            };

            kernel.Load(assemblies);
        }
    }
}
