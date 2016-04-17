namespace WebChat.Infrastructure.Data.Models.Identity
{
    #region Using

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public class UserLoginModel : IdentityUserLogin<long>
    {
    }
}
