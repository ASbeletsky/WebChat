using System;
using WebChat.Infrastructure.Services.Interfaces;

namespace WebChat.Infrastructure.Services.Common
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
                        throw new Exception("Dependency resolver was not set");
                    }

                    return currentDependencyResolver;
                }                
            }
        }
    }
}
