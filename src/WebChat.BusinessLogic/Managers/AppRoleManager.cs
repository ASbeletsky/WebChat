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
    public class AppRoleManager : RoleManager<AppRole, long>
    {
        public AppRoleManager(RoleStore<AppRole, long, AppUserRole> store)
                    : base(store) 
        { }
        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            return new AppRoleManager(new RoleStore<AppRole, long, AppUserRole>
                (
                    context.Get<WebChatDbContext>()
                ));
        }

        public bool IsUserInRole(AppUser user, string roleName)
        {
            return user.Roles.Any(role =>
            {
                return role.UserId == user.Id &&
                       role.RoleId == this.FindByName(roleName).Id;
            });                                  
        }

        public bool IsUserInRole(int userId, string roleName)
        {
            var userRoles = WebChatDbContext.GetInstance().Users.Find(userId).Roles;
            return userRoles.Any(role =>
            {
                return role.UserId == userId &&
                       role.RoleId == this.FindByName(roleName).Id;
            });
        }
    }
}
