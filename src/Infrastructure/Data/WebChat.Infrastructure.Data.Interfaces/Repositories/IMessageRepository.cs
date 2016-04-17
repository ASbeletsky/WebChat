namespace WebChat.Infrastructure.Data.Interfaces.Repositories
{
    #region Using

    using WebChat.Infrastructure.Data.Models.Chat;

    #endregion

    public interface IMessageRepository : IRepository<MessageModel, long>
    {
    }
}
