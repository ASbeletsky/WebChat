namespace WebChat.WebUI.WebApp.AppStart
{
    #region Using

    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Domain.Data.Managers;
    using Data.Storage.Identity;

    #endregion

    // Настройка диспетчера входа для приложения.
    public class ApplicationSignInManager : SignInManager<UserModel, long>
    {
        public ApplicationSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }
    }
}
