namespace WebChat.Services.Interfaces
{
    #region Using    

    using System;

    #endregion Using

    /// <summary>
    /// Represents object provides access to current operation context.
    /// </summary>
    public interface IRequestContext : IDisposable
    {
        /// <summary>
        /// Gets an instance of model factory.
        /// </summary>
        IDependencyContainer Factory
        {
            get;
        }
    }
}
