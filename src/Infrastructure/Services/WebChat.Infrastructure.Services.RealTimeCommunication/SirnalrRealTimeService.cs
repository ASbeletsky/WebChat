namespace WebChat.Infrastructure.Services.RealTimeCommunication
{
    #region Using

    using System.Configuration;
    using Owin;
    using Microsoft.AspNet.SignalR;
    using Interfaces;

    #endregion

    /// <summary>
    /// Represents implementaion of <see cref="IRealTimeWebService"/> by signalR
    /// </summary>
    public class SirnalrRealTimeService : IRealTimeWebService
    {
        private readonly string hubPath;
        private readonly HubConfiguration hubConfig;

        /// <summary>
        /// Initialize new instance of <see cref="SirnalrRealTimeService"/> using <see cref="SignalrConfigSection"/> in web config
        /// </summary>
        public SirnalrRealTimeService()
        {
            SignalrConfigSection signalrConfig = (SignalrConfigSection) ConfigurationManager.GetSection(SignalrConfigSection.SectionName);
            hubPath = signalrConfig.HubPath;
            hubConfig = new HubConfiguration
            {
                EnableDetailedErrors = signalrConfig.HubConfiguration.EnableDetailedErrors,
                EnableJSONP = signalrConfig.HubConfiguration.EnableJSONP
            };
        }

        /// <summary>
        /// Register real time web communication service in current web application
        /// </summary>
        public void RegisterService(IAppBuilder application)
        {
            var idProvider = new SignalrUserIdProvider();
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);
            application.MapSignalR(hubPath, hubConfig);
        }
    }
}
