namespace WebChat.Domain.Interfaces.Repositories
{
    #region Using

    using WebChat.Domain.Core.Chat;
    using Core.Identity;
    using System.Collections.Generic;

    #endregion

    public interface IUserRepository : IRepository<UserModel, long>
    {
        IEnumerable<UserModel> GetUsersInRole(string roleName);
    }
}
