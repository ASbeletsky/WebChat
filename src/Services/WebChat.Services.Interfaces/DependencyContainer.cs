namespace WebChat.Services.Interfaces
{
    public class DependencyContainer
    {
        private static IDependencyContainer container;
        public static IDependencyContainer Current
        {
            get { return DependencyContainer.container; }
        }

        public static void SetContainer(IDependencyContainer container)
        {
            DependencyContainer.container = container;
        }
    }
}
