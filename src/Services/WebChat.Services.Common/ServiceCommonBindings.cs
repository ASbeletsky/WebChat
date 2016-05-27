namespace WebChat.Services.Common
{
    #region Using

    using Interfaces;
    using Ninject.Modules;
    using WebChat.Services.Interfaces.Settings;

    #endregion

    class ServiceCommonBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IAuthSettings>().To<AuthServices>().InSingletonScope();
            Kernel.Bind<IDependencyContainer>().To<NinjectDependencyContainer>();
            Kernel.Bind<IEntityConverter>().To<EntityConverter>();
        }
    }
}
