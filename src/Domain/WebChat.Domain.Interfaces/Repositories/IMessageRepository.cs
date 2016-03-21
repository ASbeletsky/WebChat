namespace WebChat.Domain.Interfaces.Repositories
{
    #region Using

    using System.Collections.Generic;
    using WebChat.Domain.Core.Chat;
    using WebChat.Domain.Interfaces;

    #endregion

    public interface IMessageRepository : IRepository<MessageModel, long>
    {
    }
}
