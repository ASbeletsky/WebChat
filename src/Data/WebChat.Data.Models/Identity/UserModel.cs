namespace WebChat.Data.Storage.Identity
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Chat;
    using Models.Application;
    using System.Linq;
    #endregion

    public class UserModel : IdentityUser<long, UserLoginModel, UsersInRolesModel, UserClaimModel>
    {
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<MessageModel> Messages { get; set; }
        public ICollection<UsersInDialogsModel> DialogsShortInfo { get; set; }
        public virtual ICollection<UsersInAppsModel> ApplicationsShortInfo { get; set; }
        public virtual ICollection<ApplicationModel> myOwnApplications { get; set; } //для владельцев

        public IEnumerable<DialogModel> Dialogs
        {
            get
            {
                return this.DialogsShortInfo.Select(userInDialog => userInDialog.Dialog);
            }
        }

        public IEnumerable<ApplicationModel> Applications
        {
            get
            {
                return this.ApplicationsShortInfo.Select(appInfo => appInfo.App);
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserModel, long> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public string PhotoSource
        {
            get
            {
                string photoSrc = this.Claims.FirstOrDefault(c => c.ClaimType == "PhotoUrl").ClaimValue;
                if (photoSrc == null)
                    photoSrc = "~/Content/Images/default-user-image.png";
                return photoSrc;
            }
        }
    }
}
