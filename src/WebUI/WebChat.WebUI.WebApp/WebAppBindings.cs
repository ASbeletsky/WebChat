namespace WebChat.WebUI.WebApp
{

    #region Using

    using Microsoft.Owin.Security;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using RealTimeWebService;
    using System.Web;
    using Microsoft.AspNet.SignalR;
    using Data.Storage.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Data.Storage;
    using Ninject;
    using Data.Storage.Managers;
    #endregion

    public class WebAppBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUserStore<UserModel, long>>().To<UserStore<UserModel, UserRoleModel, long, UserLoginModel, UsersInRolesModel, UserClaimModel>>().InRequestScope();
            Kernel.Bind<AppUserManager>().ToMethod((context => AppUserManager.Create(new WebChatDbContext()))).InRequestScope();
            Kernel.Bind<UserManager<UserModel, long>>().To<AppUserManager>().InRequestScope();
            Kernel.Bind<IRoleStore<IdentityRole, string>>().To<RoleStore<IdentityRole, string, IdentityUserRole>>().WithConstructorArgument("context", context => Kernel.Get<WebChatDbContext>());
            Kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            Kernel.Bind<IUserIdProvider>().To<SignalrUserIdProvider>().InRequestScope();
        }
    }
}
