using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Abstract;
using WebChat.DataAccess.Concrete.Entities.Chat;

namespace WebChat.BusinessLogic.Chat.Entities
{
    public class ChatMessage
    {
        public int Id;
        public ChatDialog RelatedDialog { get; set; }
        public ChatMember SentBy { get; set; }
        public DateTime SentAt { get; set; }
        public Message AsStoredMessage { get; set; }
    }
}
