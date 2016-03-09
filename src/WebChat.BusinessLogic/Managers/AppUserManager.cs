using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.DataAccess.Concrete.DataBase;
using WebChat.DataAccess.Concrete.Entities.Identity;

namespace WebChat.BusinessLogic.Managers
{
    public class AppUserManager : UserManager<AppUser, int>
    {
        public AppUserManager(IUserStore<AppUser, int> store) 
            : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager( new UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
                                                (
                                                    context.Get<WebChatDbContext>()
                                                )
                                            );
            // Настройка логики проверки имен пользователей
            manager.UserValidator = new UserValidator<AppUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Настройка логики проверки паролей
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Настройка параметров блокировки по умолчанию
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
           
            return manager;
        }

        public override Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            var result = base.CreateAsync(user, password);
            return result;
        }

    }
}
