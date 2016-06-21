
using WebChat.Data.Storage.Identity;

namespace WebChat.Business.Chat.Entities
{
    public class ChatClient : ChatMember
    {
        public ChatClient(long userId, int appId)
            : base(userId, appId)
        {
        }

        public int DialogId { get; set; }
    }
}
