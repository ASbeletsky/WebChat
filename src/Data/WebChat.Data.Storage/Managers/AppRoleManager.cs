namespace WebChat.Data.Data.Managers
{
    #region Using

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WebChat.Data.Storage;
    using WebChat.Data.Storage.Identity;

    #endregion
    public class AppRoleManager : RoleManager<UserRoleModel, long>
    {
        public AppRoleManager()
                    : base(new RoleStore<UserRoleModel, long, UsersInRolesModel>(WebChatDbContext.GetInstance()))
        {
        }
        
    }
}
