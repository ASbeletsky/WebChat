namespace WebChat.Services.Managment
{
    #region Using

    using Ninject.Modules;
    using Infrastructure.Services.Interfaces;
    using WebChat.Services.Interfaces;
    using Ninject;
    using Infrastructure.Data.Storage.Managers;

    #endregion

    public class BusinessServicesBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ICustomerService>().To<CustomerService>()
                  .WithConstructorArgument("storageUserManager", AppUserManager.Create())
                  .WithConstructorArgument("entityConverter", ctx => ctx.Kernel.Get<IEntityConverter>());
        }
    }
}
