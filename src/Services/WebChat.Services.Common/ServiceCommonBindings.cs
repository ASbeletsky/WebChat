namespace WebChat.Services.Common
{
    #region Using

    using Interfaces;
    using Ninject.Modules;
    using Settings;
    using WebChat.Services.Interfaces.Settings;

    #endregion

    public class ServiceCommonBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IApplicationSettings>().To<ApplicationSettings>().InSingletonScope();
            Kernel.Bind<IDependencyContainer>().To<NinjectDependencyContainer>();
            Kernel.Bind<IEntityConverter>().To<EntityConverter>();
        }
    }
}
