namespace WebChat.Infrastructure.Services.Interfaces
{
    public class DependencyResolver
    {
        private static object mutex;
        private static IDependencyResolver currentDependencyResolver;

        public static void SetResolver(IDependencyResolver dependencyResolver)
        {
            currentDependencyResolver = dependencyResolver;
        }

        public static IDependencyResolver Current
        {
            get
            {
                lock(mutex)
                {
                    if(currentDependencyResolver == null)
                    {
                        throw new System.Exception("Dependency resolver was not set");
                    }

                    return currentDependencyResolver;
                }                
            }
        }
    }
}
