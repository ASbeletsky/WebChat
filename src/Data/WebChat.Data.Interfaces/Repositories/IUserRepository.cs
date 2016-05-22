namespace WebChat.Domain.Interfaces.Repositories
{

    #region Using

    using Data.Storage.Identity;
    using System.Collections.Generic;

    #endregion

    public interface IUserRepository : IRepository<UserModel, long>
    {
        IEnumerable<UserModel> GetUsersInRole(string roleName);
    }
}
