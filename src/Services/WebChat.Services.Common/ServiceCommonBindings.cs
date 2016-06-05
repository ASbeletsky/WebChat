namespace WebChat.Services.Common
{
    #region Using

    using Interfaces;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Settings;

    #endregion

    public class ServiceCommonBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IApplicationSettings>().To<ApplicationSettings>().InSingletonScope();
            Kernel.Bind<IDependencyContainer>().To<NinjectDependencyContainer>().InRequestScope();
            Kernel.Bind<IEntityConverter>().To<EntityConverter>().InRequestScope();
        }
    }
}
