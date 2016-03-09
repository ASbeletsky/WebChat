namespace WebChat.Models.Entities.Identity
{
    #region Using

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public class AppRole : IdentityRole<long, AppUserRole>
    {
    }
}
