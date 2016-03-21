namespace WebChat.Domain.Core.Identity
{
    #region Using

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public class UserRoleModel : IdentityRole<long, UsersInRolesModel>
    {
    }
}
