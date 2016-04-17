namespace WebChat.Infrastructure.Data.Storage.Managers
{
    #region Using

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Linq;
    using System.Security.Claims;
    using System;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using Models.Identity;

    #endregion

    public class AppUserManager : UserManager<UserModel, long>
    {
        public AppUserManager(IUserStore<UserModel, long> store) 
            :base(store)
        {
              
        }



        public static AppUserManager Create()
        {
            var context = WebChatDbContext.GetInstance();
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

        public async Task<IdentityResult> SaveClaimAsync(long userId, Claim claim)
        {
            try
            {
                var context = WebChatDbContext.GetInstance();
                UserClaimModel currentClaim = context.Set<UserClaimModel>().FirstOrDefault(c => c.ClaimType == claim.Type);
                if(currentClaim != null)
                {
                    currentClaim.ClaimValue = claim.Value;
                    context.Entry(currentClaim).State = EntityState.Modified;
                    int updatesCount = await context.SaveChangesAsync();

                    if (updatesCount > 0)
                        return new IdentityResult();
                    else
                        return new IdentityResult(string.Format("Cant update {0} claim", claim.Type));
                }

                return await this.AddClaimAsync(userId, claim);
            }
            catch(Exception ex)
            {
                return new IdentityResult("Erroe." + ex.Message);
            }
        }
    }
}
