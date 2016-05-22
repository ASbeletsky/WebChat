namespace WebChat.Domain.Interfaces.Repositories
{

    #region Using

    using Data.Models.Chat;
    using WebChat.Domain.Interfaces;

    #endregion

    public interface IMessageRepository : IRepository<MessageModel, long>
    {
    }
}
