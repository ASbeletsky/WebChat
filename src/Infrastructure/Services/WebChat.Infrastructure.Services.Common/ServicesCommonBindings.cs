namespace WebChat.Infrastructure.Services.Common
{
    #region Using

    using Ninject.Modules;
    using Interfaces;

    #endregion
    public class ServicesCommonBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IEntityConverter>().To<EntityConverter>();                 
        }
    }
}
