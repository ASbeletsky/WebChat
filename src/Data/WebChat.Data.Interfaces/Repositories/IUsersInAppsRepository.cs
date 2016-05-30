namespace WebChat.Data.Interfaces.Repositories
{
    #region Using

    using Data.Models.Application;
    using System.Collections.Generic;
    using WebChat.Data.Interfaces;

    #endregion

    public interface IUsersInAppsRepository : IRepository<UsersInAppsModel, IComplexKey<long, int>>
    {
        void DeleteUserFromAllApps(long userId);
    }
}
