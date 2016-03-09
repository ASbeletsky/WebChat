namespace WebChat.Infrastructure
{
    /// <summary>
    /// Represents interface for dependency injectors
    /// </summary>
    public interface IDependencyInjector
    {
        /// <summary>
        /// Registers all bindings in currect dependecy injector
        /// </summary>
        void RegisterBindings();

        /// <summary>
        /// Gets an instance of specified service
        /// </summary>
        /// <typeparam name="T">service type</typeparam>
        T Get<T>();
    }
}
