namespace WebChat.ApplicationBootstrap.DependencyInjection
{
    using System;
    #region Using

    using Ninject;
    using WebChat.Infrastructure;
    using WebChat.RealTimeWebService;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using Ninject.Modules;

    #endregion

    /// <summary>
    /// Represents <see cref="IDependencyInjector"/> implementation by Ninject
    /// </summary>
    public class NinjectDependencyInjector : IDependencyInjector
    {
        private readonly IKernel kernel;
        public NinjectDependencyInjector()
        {
            kernel = new StandardKernel();
        }

        /// <summary>
        /// Gets an instance of specified service
        /// </summary>
        public T Get<T>()
        {
            return kernel.Get<T>();
        }

        /// <summary>
        /// Registers all bindings by Ninject
        /// </summary>
        public void RegisterBindings()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            kernel.Load(assemblies);
        }
    }
}
