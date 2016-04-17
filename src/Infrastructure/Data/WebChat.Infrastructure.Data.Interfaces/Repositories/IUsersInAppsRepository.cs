namespace WebChat.Infrastructure.Data.Interfaces.Repositories
{
    #region Using

    using WebChat.Infrastructure.Data.Models.Application;

    #endregion

    public interface IUsersInAppsRepository : IRepository<UsersInAppsModel, IComplexKey<long, int>>
    {
        void DeleteUserFromAllApps(long userId);
    }
}
