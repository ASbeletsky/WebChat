namespace WebChat.Data.Interfaces.Repositories
{
    #region Using

    using WebChat.Data.Models.Chat;
    using WebChat.Data.Interfaces;

    #endregion

    public interface IUsersInDialogsRepository : IRepository<UsersInDialogsModel, IComplexKey<long, int>>
    {

    }
}
