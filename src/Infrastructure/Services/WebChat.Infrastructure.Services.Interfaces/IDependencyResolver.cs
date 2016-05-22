namespace WebChat.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// Represents interface for dependency injectors
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// Gets an instance of specified service
        /// </summary>
        /// <typeparam name="T">service type</typeparam>
        T GetService<T>();
    }
}
