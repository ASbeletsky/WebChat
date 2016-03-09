namespace WebChat.ApplicationBootstrap
{
    #region Using

    using DependencyInjection;
    using WebChat.Infrastructure;

    #endregion

    public static class ApplicationBootstraper
    {
        /// <summary>
        /// Contatains currecnt dependency injector
        /// </summary>
        private static IDependencyInjector _dependencyInjector;

        /// <summary>
        /// Contatains currecnt dependency injector
        /// </summary>
        private static IRealTimeWebService _realTimeWebService;

        /// <summary>
        /// Initializes application modules
        /// </summary>
        static ApplicationBootstraper()
        {
            _dependencyInjector = new NinjectDependencyInjector();
            _realTimeWebService = _dependencyInjector.Get<IRealTimeWebService>();
        }

        /// <summary>
        /// Gets currecnt dependency injector
        /// </summary>
        public static IDependencyInjector DependencyInjector
        {
            get
            {
                return _dependencyInjector;
            }
        }

        public static IRealTimeWebService RealTimeWebService
        {
            get
            {
                return _realTimeWebService;
            }
        }
    }
}
