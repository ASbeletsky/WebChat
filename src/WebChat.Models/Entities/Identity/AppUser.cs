namespace WebChat.Models.Entities.Identity
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WebChat.Models.Entities.Chat;
    using WebChat.Models.Entities.CustomerApps;

    #endregion

    public class AppUser : IdentityUser<long, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public string Name { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<CustomerApplication> RelatedApplications { get; set; }
        public virtual ICollection<CustomerApplication> myOwnApplications { get; set; } //для владельцев


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser, long> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
