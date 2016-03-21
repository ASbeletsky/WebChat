namespace WebChat.Services.Interfaces
{
    /// <summary>
    /// Represents interface for dependency injectors
    /// </summary>
    public interface IDependencyContainer
    {
        /// <summary>
        /// Gets an instance of specified service
        /// </summary>
        /// <typeparam name="T">service type</typeparam>
        T Get<T>();
    }
}
