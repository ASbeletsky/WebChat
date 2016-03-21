namespace WebChat.Domain.Data.Managers
{
    #region Using

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WebChat.Domain.Core.Identity;
    using WebChat.Domain.Data;

    #endregion
    public class AppRoleManager : RoleManager<UserRoleModel, long>
    {
        public AppRoleManager()
                    : base(new RoleStore<UserRoleModel, long, UsersInRolesModel>(WebChatDbContext.GetInstance()))
        {
        }
        
    }
}
