namespace WebChat.Data.Storage.Managers
{
    #region Using

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using WebChat.Data.Storage;
    using WebChat.Data.Storage.Identity;

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
                RequiredLength = 4,
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

        public async Task<IdentityResult> AddPhotoAsync(long userId, string photoUrl)
        {
            IdentityResult addPhoroResult = null;           
            
            if (ImageUrlIsValid(photoUrl))
            {
                var photo = new Claim(AppClaimTypes.PhotoSource, photoUrl);
                addPhoroResult = await this.AddClaimAsync(userId, photo);
            }
            else
            {
                addPhoroResult = new IdentityResult("Photo source is not valid");
            }

            return addPhoroResult;
        }

        private bool ImageUrlIsValid(string photoUrl)
        {
            bool isValid = !string.IsNullOrEmpty(photoUrl);
            if(isValid)
            {
                var getPhotoRequst = System.Net.WebRequest.CreateHttp(photoUrl);
                getPhotoRequst.Method = "HEAD";
                var getPhotoResponce = getPhotoRequst.GetResponse();
                if (!getPhotoResponce.ContentType.Contains("image"))
                {
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
