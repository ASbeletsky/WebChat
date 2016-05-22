namespace WebChat.Infrastructure.Data.Models.Identity
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Chat;
    using Application;
    using System.Linq;
    #endregion

    public class UserModel : IdentityUser<long, UserLoginModel, UsersInRolesModel, UserClaimModel>
    {
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<MessageModel> Messages { get; set; }
        public virtual ICollection<DialogModel> Dialogs { get; set; }
        public virtual ICollection<ApplicationModel> RelatedApplications { get; private set; }
        public virtual ICollection<ApplicationModel> myOwnApplications { get; set; } //для владельцев

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserModel, long> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public bool IsInRole(Roles role)
        {
            return this.Roles.Any(userRole => userRole.RoleId == (int) role);
        }
    }
}
