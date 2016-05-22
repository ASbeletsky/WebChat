namespace WebChat.Infrastructure.Data.Interfaces.Repositories
{
    using Domain.Core.Identity;
    #region Using

    using System.Collections.Generic;

    #endregion

    public interface IUserRepository : IRepository<User, long>
    {
    }
}
