namespace WebChat.Domain.Data.Managers
{
    #region Using

    using Microsoft.AspNet.Identity;
    using Domain.Core.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WebChat.Domain.Data;
    using Ninject;
    using System;

    #endregion
    public class AppUserManager : UserManager<UserModel, long>
    {
        public AppUserManager(IUserStore<UserModel, long> store) 
            :base(store)
        {
              
        }

        public static AppUserManager Create(WebChatDbContext context)
        {
            var manager = new AppUserManager(new UserStore<UserModel, UserRoleModel, long, UserLoginModel, UsersInRolesModel, UserClaimModel>(context));
            // Настройка логики проверки имен пользователей
            manager.UserValidator = new UserValidator<UserModel, long>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Настройка логики проверки паролей
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = false,
            };

            // Настройка параметров блокировки по умолчанию
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }
    }
}
