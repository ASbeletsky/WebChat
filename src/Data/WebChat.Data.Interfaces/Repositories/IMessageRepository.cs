namespace WebChat.Data.Interfaces.Repositories
{

    #region Using

    using Data.Models.Chat;
    using WebChat.Data.Interfaces;

    #endregion

    public interface IMessageRepository : IRepository<MessageModel, long>
    {
    }
}
