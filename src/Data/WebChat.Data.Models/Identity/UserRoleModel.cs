namespace WebChat.Data.Storage.Identity
{
    #region Using

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public class UserRoleModel : IdentityRole<long, UsersInRolesModel>
    {
    }
}
