namespace WebChat.Business.Management
{
    #region Using

    using Ninject.Modules;
    using Services;
    using Interfaces;
    using Domain.Data.Managers;
    using WebChat.Services.Interfaces;
    using Ninject;
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
