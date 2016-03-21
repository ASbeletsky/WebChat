namespace WebChat.Domain.Interfaces.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using WebChat.Domain.Core.Application;
    using WebChat.Domain.Interfaces;

    #endregion

    public interface IUsersInAppsRepository : IRepository<UsersInAppsModel, IComplexKey<long, int>>
    {
        void DeleteUserFromAllApps(long userId);
    }
}
