namespace WebChat.WebUI.WebApp.App_Start
{
    #region Using

    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using WebChat.Domain.Core.Identity;
    using Domain.Data.Managers;

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
