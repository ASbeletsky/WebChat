namespace WebChat.Services.Common
{
    #region Using

    using Ninject.Modules;
    using WebChat.Services.Interfaces;

    #endregion
    public class ServicesCommonBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IEntityConverter>().To<EntityConverter>();                 
        }
    }
}
