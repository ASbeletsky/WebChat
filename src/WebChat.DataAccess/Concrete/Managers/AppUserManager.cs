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

namespace WebChat.DataAccess.Managers
{
    public class AppUserManager : UserManager<AppUser, long>
    {
        public AppUserManager(IUserStore<AppUser, long> store) 
            : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager( new UserStore<AppUser, AppRole, long, AppUserLogin, AppUserRole, AppUserClaim>
                                                (
                                                    context.Get<WebChatDbContext>()
                                                )
                                            );
            // Настройка логики проверки имен пользователей
            manager.UserValidator = new UserValidator<AppUser, long>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,                
                RequireUniqueEmail = false
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
