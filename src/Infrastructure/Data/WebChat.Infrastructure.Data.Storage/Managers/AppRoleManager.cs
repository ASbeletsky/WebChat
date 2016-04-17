namespace WebChat.Infrastructure.Data.Storage.Managers
{
    #region Using

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Identity;

    #endregion
    public class AppRoleManager : RoleManager<UserRoleModel, long>
    {
        public AppRoleManager()
                    : base(new RoleStore<UserRoleModel, long, UsersInRolesModel>(WebChatDbContext.GetInstance()))
        {
        }
        
    }
}
