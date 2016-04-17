namespace WebChat.Infrastructure.Data.Interfaces.Repositories
{
    using Models.Identity;
    #region Using

    using System.Collections.Generic;

    #endregion

    public interface IUserRepository : IRepository<UserModel, long>
    {
        IEnumerable<UserModel> GetUsersInRole(string roleName);
    }
}
