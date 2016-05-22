namespace WebChat.Domain.Interfaces.Repositories
{
    #region Using

    using Data.Models.Application;
    using WebChat.Domain.Interfaces;

    #endregion

    public interface IUsersInAppsRepository : IRepository<UsersInAppsModel, IComplexKey<long, int>>
    {
        void DeleteUserFromAllApps(long userId);
    }
}
