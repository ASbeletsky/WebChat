namespace WebChat.Services.Interfaces
{
    #region Using

    using Owin;

    #endregion

    /// <summary>
    /// Represents interface for real time web communication service
    /// </summary>
    public interface IRealTimeWebService
    {
        /// <summary>
        /// Register real time web communication service in current web application
        /// </summary>
        void RegisterService(IAppBuilder application);
    }
}
