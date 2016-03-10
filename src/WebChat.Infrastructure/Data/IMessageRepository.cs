using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.DataAccess.Concrete.DTO;
using WebChat.DataAccess.Concrete.Entities.Chat;

namespace WebChat.Infrastructure.Data

{
    public interface IMessageRepository : IRepository<Message>
    {
        IEnumerable<StoredMessageDTO> GetAllMessagesInDialog(int dialogId);
        double AverageMessageCountInMinuteForDialog(long userId, int dialogId);
    }
}
