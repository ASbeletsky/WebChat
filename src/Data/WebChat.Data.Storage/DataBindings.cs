namespace WebChat.Data.Storage
{
    #region Using

    using Domain.Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using System.Data.Entity;
    using WebChat.Data.Storage.Identity;
    using WebChat.Domain.Data.Managers;

    #endregion

    public class DataBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<DbContext>().ToConstructor(context => new WebChatDbContext()).InRequestScope();
            Kernel.Bind<IUserStore<UserModel, long>>().To<UserStore<UserModel, UserRoleModel, long, UserLoginModel, UsersInRolesModel, UserClaimModel>>().InRequestScope();
            Kernel.Bind<UserManager<UserModel, long>>().ToMethod(context => AppUserManager.Create(new WebChatDbContext())).InRequestScope();
            Kernel.Bind<IDataStorage>().To<EfDataStorage>().InRequestScope();
        }
    }
}
