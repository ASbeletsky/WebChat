namespace WebChat.Infrastructure.Data.Models.Identity
{
    #region Using

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public class UserRoleModel : IdentityRole<long, UsersInRolesModel>
    {
    }
}
