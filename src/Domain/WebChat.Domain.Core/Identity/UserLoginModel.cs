namespace WebChat.Domain.Core.Identity
{
    #region Using

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public class UserLoginModel : IdentityUserLogin<long>
    {
    }
}
